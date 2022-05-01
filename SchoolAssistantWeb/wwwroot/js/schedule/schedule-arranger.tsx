interface ScheduleClassLessons {
    data: ScheduleDayLessons[];
}

interface ScheduleDayLessons {
    dayIndicator: DayOfWeek;
    lessons: PeriodicLessonTimetableEntry[];
}

interface PeriodicLessonTimetableEntry {
    id?: number;

    hour: number;
    minutes: number;
    customDuration?: number;

    subject: IdName;
    lecturer: IdName;
    room?: IdName;
}

interface ScheduleLessonPrefab {
    subject: IdName;
    lecturer: IdName;
    room?: IdName;
}

interface IdName {
    id: number;
    name: string;
}

enum DayOfWeek {
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Sunday
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
    private _monday: ScheduleDayLessons;
    private _tuesday: ScheduleDayLessons;
    private _wednesday: ScheduleDayLessons;
    private _thursday: ScheduleDayLessons;
    private _friday: ScheduleDayLessons;

    private assignDays() {
        for (let dayData of this.props.data) {
            switch (dayData.dayIndicator) {
                case DayOfWeek.Monday: this._monday = dayData;
                    break;
                case DayOfWeek.Tuesday: this._tuesday = dayData;
                    break;
                case DayOfWeek.Wednesday: this._wednesday = dayData;
                    break;
                case DayOfWeek.Thursday: this._thursday = dayData;
                    break;
                case DayOfWeek.Friday: this._friday = dayData;
                    break;
                default: break;
            }
        }
    }

    onDropped = (dayIndicator: DayOfWeek, cellIndex: number, time: Time, data: DataTransfer) => {

    }

    render() {
        this.assignDays();

        return (
            <div className="schedule-arranger-timeline">

                <ScheduleTimeColumn/>

                <ScheduleDayColumn
                    dayIndicator={DayOfWeek.Monday}
                    lessons={this._monday?.lessons ?? []}
                    dropped={this.onDropped}
                />
                <ScheduleDayColumn
                    dayIndicator={DayOfWeek.Tuesday}
                    lessons={this._tuesday?.lessons ?? []}
                    dropped={this.onDropped}
                />
                <ScheduleDayColumn
                    dayIndicator={DayOfWeek.Wednesday}
                    lessons={this._wednesday?.lessons ?? []}
                    dropped={this.onDropped}
                />
                <ScheduleDayColumn
                    dayIndicator={DayOfWeek.Thursday}
                    lessons={this._thursday?.lessons ?? []}
                    dropped={this.onDropped}
                />
                <ScheduleDayColumn
                    dayIndicator={DayOfWeek.Friday}
                    lessons={this._friday?.lessons ?? []}
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
    prefabs: ScheduleLessonPrefab[];
}
class ScheduleArrangerSelector extends React.Component<ScheduleArrangerSelectorProps, ScheduleArrangerSelectorState> {
    constructor(props) {
        super(props);

        this.state = {
            prefabs: []
        }

        let prefabs = this.props.data.flatMap(dayLessons => dayLessons.lessons).map(this.lessonToPrefab);

        for (let prefab of prefabs) {
            if (this.state.prefabs.some(x =>
                x.subject.id == prefab.subject.id
                && x.lecturer.id == prefab.lecturer.id
                && x.room.id == prefab.room.id)) continue;

            this.state.prefabs.push(prefab);
        }
    }

    private lessonToPrefab = (lesson: PeriodicLessonTimetableEntry): ScheduleLessonPrefab => ({
        subject: lesson.subject,
        lecturer: lesson.lecturer,
        room: lesson.room
    })

    render() {
        return (
            <div className="schedule-arranger-selector">
                {this.state.prefabs.map((prefab, index) => (
                    <ScheduleLessonPrefabTile
                        key={index}
                        data={prefab}
                    />
                ))}
            </div>
        )
    }
}