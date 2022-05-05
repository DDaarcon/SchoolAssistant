enum DayOfWeek {
    Sunday,
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday
}

/**
 *  This file contains:
 *  
 *  ScheduleArrangerPage - container for timetable and prefab selector components
 *  
 *  ScheduleArrangerTimeline - component in which included are columns for displaying each day of week.
 *      This component is also responsible for sending to server data about newly placed lessons on schedule.
 * 
 *  ScheduleArrangerSelector - holder for prefabs, creates them from already placed lessons when applicatioin is first time loaded.
 *      Opens modal for creating new prefabs. Prefabs are stored in DataService
 * 
 * */






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
    teacherBusyLessons?: ScheduleDayLessons[];
    roomBusyLessons?: ScheduleDayLessons[];
}
class ScheduleArrangerTimeline extends React.Component<ScheduleArrangerTimelineProps, ScheduleArrangerTimelineState> {
    private _days: ScheduleDayLessons[];

    constructor(props) {
        super(props);

        this.state = {};

        this._assignDaysFromProps();

        addEventListener('dragBegan', (event: CustomEvent) => this.initiateShowingOtherLessonsShadows(event));
        addEventListener('hideLessonShadow', this.hideOtherLessonsShadows);
    }

    private _assignDaysFromProps() {
        this._days = [];
        for (const dayOfWeekIt in DayOfWeek) {
            if (isNaN(dayOfWeekIt as unknown as number)) continue;

            const dayOfWeek = dayOfWeekIt as unknown as DayOfWeek;
            this._days.push(
                this.props.data.find(x => x.dayIndicator == dayOfWeek)
                ?? { dayIndicator: dayOfWeek, lessons: [] }
            );
        }
    }

    onDropped = (dayIndicator: DayOfWeek, cellIndex: number, time: Time, data: DataTransfer) => {
        this.hideOtherLessonsShadows();

        const prefab: ScheduleLessonPrefab | undefined = JSON.parse(data.getData("prefab"));

        const lessons = this._days[dayIndicator];
        if (!lessons) return;

        for (const lesson of lessons.lessons ?? []) {
            const overlaps = areTimesOverlappingByDuration(
                lesson.time,
                lesson.customDuration ?? scheduleArrangerConfig.defaultLessonDuration,
                time,
                scheduleArrangerConfig.defaultLessonDuration
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
            console.log(result);
            if (result.success) {
                this._days[dayIndicator].lessons.push(result.lesson);
                this.forceUpdate();
            }
        })
    }


    initiateShowingOtherLessonsShadows = async (event: CustomEvent) => {
        const data: ScheduleLessonPrefab = event.detail;

        await scheduleDataService.getTeacherAndRoomLessons(data.lecturer.id, data.room.id, this.displayOtherLessonsShadows);
    }
    displayOtherLessonsShadows = (teacher?: ScheduleDayLessons[], room?: ScheduleDayLessons[]) => {
        if (!teacher && !room) return;

        this.setState(prevState => {
            let { teacherBusyLessons, roomBusyLessons } = prevState;

            teacherBusyLessons ?? (teacherBusyLessons = teacher);
            roomBusyLessons ?? (roomBusyLessons = room);

            return { teacherBusyLessons, roomBusyLessons };
        });
    }
    hideOtherLessonsShadows = () => {
        this.setState({
            teacherBusyLessons: undefined,
            roomBusyLessons: undefined
        })
    }



    private getDaysOfWeekIterable = (with6th: boolean = false, with0th: boolean = false) =>
        Object.values(DayOfWeek).map(x => parseInt(x as unknown as string)).filter(x =>
            !isNaN(x) && (with0th || x != 0) && (with6th || x != 6));

    render() {

        console.log(this.getDaysOfWeekIterable());
        return (
            <div className="schedule-arranger-timeline">

                <ScheduleTimeColumn/>

                {this.getDaysOfWeekIterable().map(x => (
                    <ScheduleDayColumn
                        key={x}
                        dayIndicator={x as DayOfWeek.Monday}
                        lessons={this._days[x]?.lessons ?? []}
                        teacherBusyLessons={this.state.teacherBusyLessons?.[x]?.lessons}
                        roomBusyLessons={this.state.roomBusyLessons?.[x]?.lessons}
                        dropped={this.onDropped}
                    />
                ))}

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

        this.preparePrefabs();

        addEventListener('newPrefab', (_) => this.forceUpdate());
    }

    private preparePrefabs() {
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


const areTimesOverlappingByDuration = (timeAStart: Time, durationA: number, timeBStart: Time, durationB: number) => {
    const aStart = toMinutes(timeAStart);
    const aEnd = aStart + durationA;
    const bStart = toMinutes(timeBStart);
    const bEnd = bStart + durationB;

    const left = aStart > bStart && aStart < bEnd;
    const right = aEnd > bStart && aEnd < bEnd;
    const over = aStart <= bStart && aEnd >= bEnd;

    return left || right || over;
}

const toMinutes = (time: Time) => time.hour * 60 + time.minutes;