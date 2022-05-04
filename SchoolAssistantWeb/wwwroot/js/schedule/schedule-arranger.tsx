﻿interface ScheduleClassLessons {
    data: ScheduleDayLessons[];
}

interface ScheduleDayLessons {
    dayIndicator: DayOfWeek;
    lessons: PeriodicLessonTimetableEntry[];
}

interface Time {
    hour: number;
    minutes: number;
}

interface PeriodicLessonTimetableEntry {
    id?: number;

    time: Time;
    customDuration?: number;

    subject: IdName;
    lecturer: IdName;
    room?: IdName;
}

interface IdName {
    id: number;
    name: string;
}

enum DayOfWeek {
    Sunday,
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday
}


interface AddLessonResponse extends ResponseJson {
    lesson?: PeriodicLessonTimetableEntry;
}





type ScheduleArrangerPageProps = {
    classData: ScheduleClassLessons;
}
type ScheduleArrangerPageState = {

}
class ScheduleArrangerPage extends React.Component<ScheduleArrangerPageProps, ScheduleArrangerPageState> {


    render() {
        return (
            <div className="schedule-arranger-page">
                <ScheduleArrangerSelector
                    data={this.props.classData.data}
                />

                <ScheduleArrangerTimeline
                    data={this.props.classData.data}
                />
            </div>
        )
    }
}





type ScheduleArrangerTimelineProps = {
    data: ScheduleDayLessons[];

}
type ScheduleArrangerTimelineState = {

}
class ScheduleArrangerTimeline extends React.Component<ScheduleArrangerTimelineProps, ScheduleArrangerTimelineState> {
    private _days: ScheduleDayLessons[];

    constructor(props) {
        super(props);

        this._assignDaysFromProps();
    }

    private _assignDaysFromProps() {
        this._days = [];
        for (let dayData of this.props.data) {
            this._days[dayData.dayIndicator] = dayData;
        }
    }

    onDropped = (dayIndicator: DayOfWeek, cellIndex: number, time: Time, data: DataTransfer) => {
        const prefab: ScheduleLessonPrefab | undefined = JSON.parse(data.getData("prefab"));

        const lessons = this._days[dayIndicator];
        if (!lessons) return;

        for (const lesson of lessons.lessons ?? []) {
            const overlaps = areTimesOverlapping(
                lesson.time,
                sumTimes(lesson.time, { hour: 0, minutes: lesson.customDuration ?? scheduleArrangerConfig.defaultLessonDuration }),
                time,
                { hour: 0, minutes: scheduleArrangerConfig.defaultLessonDuration }
            );

            if (overlaps) return;
        }

        scheduleServer.postAsync<AddLessonResponse>("Lesson", {}, {
            classId: scheduleArrangerConfig.classId,
            day: dayIndicator,
            time: time,
            customDuration: undefined,
            subjectId: prefab.subject.id,
            lecturerId: prefab.lecturer.id,
            roomId: prefab.room.id
        }).then(result => {
            if (result.success) {
                this._days[dayIndicator].lessons.push(result.lesson);
                this.forceUpdate();
            }
        })
    }

    render() {
        return (
            <div className="schedule-arranger-timeline">

                <ScheduleTimeColumn/>

                <ScheduleDayColumn
                    dayIndicator={DayOfWeek.Monday}
                    lessons={this._days[DayOfWeek.Monday]?.lessons ?? []}
                    dropped={this.onDropped}
                />
                <ScheduleDayColumn
                    dayIndicator={DayOfWeek.Tuesday}
                    lessons={this._days[DayOfWeek.Tuesday]?.lessons ?? []}
                    dropped={this.onDropped}
                />
                <ScheduleDayColumn
                    dayIndicator={DayOfWeek.Wednesday}
                    lessons={this._days[DayOfWeek.Wednesday]?.lessons ?? []}
                    dropped={this.onDropped}
                />
                <ScheduleDayColumn
                    dayIndicator={DayOfWeek.Thursday}
                    lessons={this._days[DayOfWeek.Thursday]?.lessons ?? []}
                    dropped={this.onDropped}
                />
                <ScheduleDayColumn
                    dayIndicator={DayOfWeek.Friday}
                    lessons={this._days[DayOfWeek.Friday]?.lessons ?? []}
                    dropped={this.onDropped}
                />

            </div>
        )
    }
}





type ScheduleArrangerSelectorProps = {
    data: ScheduleDayLessons[];
}
type ScheduleArrangerSelectorState = {
}
class ScheduleArrangerSelector extends React.Component<ScheduleArrangerSelectorProps, ScheduleArrangerSelectorState> {
    private _addPrefabModalId?: number;

    constructor(props) {
        super(props);

        const prefabs = this.props.data.flatMap(dayLessons => dayLessons.lessons).map(this.lessonToPrefab);
        const validPrefabs: ScheduleLessonPrefab[] = [];

        for (let prefab of prefabs) {
            if (validPrefabs.some(x =>
                x.subject.id == prefab.subject.id
                && x.lecturer.id == prefab.lecturer.id
                && x.room.id == prefab.room.id)) continue;

            validPrefabs.push(prefab);
        }

        scheduleDataService.prefabs = validPrefabs;

        addEventListener('addPrefab', () => this.forceUpdate());
    }

    private lessonToPrefab = (lesson: PeriodicLessonTimetableEntry): ScheduleLessonPrefab => ({
        subject: lesson.subject,
        lecturer: lesson.lecturer,
        room: lesson.room
    })

    openAddPrefabModal = () => {
        this._addPrefabModalId = modalController.add({
            children: (
                <ScheduleLessonModificationComponent
                    submit={this.addPrefab}
                />
            )
        })
    }

    private addPrefab = (info: ScheduleLessonModificationData) => {
        modalController.closeById(this._addPrefabModalId);

        scheduleDataService.addPrefab({
            subject: { id: info.subjectId, name: scheduleDataService.getSubjectName(info.subjectId) },
            lecturer: { id: info.teacherId, name: scheduleDataService.getTeacherName(info.teacherId) },
            room: { id: info.roomId, name: scheduleDataService.getRoomName(info.roomId) }
        });
    }

    render() {
        return (
            <div className="schedule-arranger-selector">
                {scheduleDataService.prefabs.map((prefab, index) => (
                    <ScheduleLessonPrefabTile
                        key={index}
                        data={prefab}
                    />
                ))}
                <ScheduleAddPrefabTile
                    onClick={this.openAddPrefabModal}
                />
            </div>
        )
    }
}



const areTimesOverlapping = (timeAStart: Time, timeAEnd: Time, timeBStart: Time, timeBEnd: Time): boolean =>
    toMinutes(timeAStart) > toMinutes(timeBStart) && toMinutes(timeAStart) < toMinutes(timeBEnd)
    || toMinutes(timeAEnd) > toMinutes(timeBStart) && toMinutes(timeAEnd) < toMinutes(timeBEnd)
    || toMinutes(timeAStart) <= toMinutes(timeBStart) && toMinutes(timeAEnd) >= toMinutes(timeBEnd);

const toMinutes = (time: Time) => time.hour * 60 + time.minutes;