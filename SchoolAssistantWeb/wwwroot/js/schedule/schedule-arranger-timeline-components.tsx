type ScheduleDayColumnProps = {
    dayIndicator: DayOfWeek;
    lessons: PeriodicLessonTimetableEntry[];

    teacherBusyLessons?: PeriodicLessonTimetableEntry[];
    roomBusyLessons?: PeriodicLessonTimetableEntry[];

    dropped: (dayIndicator: DayOfWeek, cellIndex: number, time: Time, data: DataTransfer) => void;
}
type ScheduleDayColumnState = {
    shadowFor?: Time;
}
class ScheduleDayColumn extends React.Component<ScheduleDayColumnProps, ScheduleDayColumnState> {
    private _cells: JSX.Element[];
    private _iAmCallingHideShadow = false;

    constructor(props) {
        super(props);

        this.state = {};

        addEventListener('hideLessonShadow', () => this.hideLessonShadow());

        this.instantiateCells();
    }


    instantiateCells = () => {
        const cellsPerHour = 60 / scheduleArrangerConfig.cellDuration;
        const count = (scheduleArrangerConfig.endHour - scheduleArrangerConfig.startHour) * cellsPerHour;

        const cellTimes = Array.from({ length: count }, (_, i): Time => {
            const minutesFromMidnight = (scheduleArrangerConfig.startHour * 60) + scheduleArrangerConfig.cellDuration * i;
            return {
                hour: Math.floor(minutesFromMidnight / 60),
                minutes: minutesFromMidnight % 60
            };
        })

        this._cells = cellTimes.map((cellTime, i) =>
            <ScheduleCell
                key={i}
                dayIndicator={this.props.dayIndicator}
                cellIndex={i}
                dropped={this.props.dropped}
                entered={this.onEntered}
                time={cellTime}
            />);
    }



    dropped = (dayIndicator: DayOfWeek, cellIndex: number, time: Time, data: DataTransfer) => {
        this.hideLessonShadow();
        this.props.dropped(dayIndicator, cellIndex, time, data);
    }

    onEntered = (dayIndicator: DayOfWeek, cellIndex: number, time: Time) => {
        this._iAmCallingHideShadow = true;
        dispatchEvent(new Event('hideLessonShadow'));
        this._iAmCallingHideShadow = false;

        this.setState({ shadowFor: time });
    }



    hideLessonShadow = () => {
        if (this.state.shadowFor && !this._iAmCallingHideShadow)
            this.setState({ shadowFor: undefined });
    }



    render() {

        return (
            <div className="sa-schedule-day-column"
                onDragEnd={this.hideLessonShadow}
            >
                <DayLabel day={this.props.dayIndicator} />
                <RoomLessonsShadow lessons={this.props.roomBusyLessons} />
                <TeacherLessonsShadow lessons={this.props.teacherBusyLessons} />
                <LessonPlacingShadow time={this.state.shadowFor} />
                <LessonsByDay lessons={this.props.lessons} />
                {this._cells}
            </div>
        )
    }
}





type ScheduleCellProps = {
    dayIndicator: DayOfWeek;
    cellIndex: number;
    time: Time;

    dropped: (dayIndicator: DayOfWeek, cellIndex: number, time: Time, data: DataTransfer) => void;
    entered: (dayIndicator: DayOfWeek, cellIndex: number, time: Time) => void;
}
type ScheduleCellState = {

}
class ScheduleCell extends React.Component<ScheduleCellProps, ScheduleCellState> {
    private get _wholeHour() { return this.props.time.minutes == 0; }

    onDrop: React.DragEventHandler<HTMLDivElement> = (event) => {
        this.props.dropped(
            this.props.dayIndicator,
            this.props.cellIndex,
            this.props.time,
            event.dataTransfer
        );
    }

    onDragOver: React.DragEventHandler<HTMLDivElement> = (event) => {
        event.preventDefault();
    }

    onDragEnter: React.DragEventHandler<HTMLDivElement> = (_) => {
        this.props.entered(
            this.props.dayIndicator,
            this.props.cellIndex,
            this.props.time
        );
    }
    render() {
        let style: React.CSSProperties = {
            height: scheduleArrangerConfig.cellHeight
        }

        return (
            <div className={"sa-schedule-cell" + (this._wholeHour ? " sa-schedule-cell-whole-hour" : "")}
                style={style}
                onDrop={this.onDrop}
                onDragOver={this.onDragOver}
                onDragEnter={this.onDragEnter}
            >
                {this._wholeHour
                    ? <div className="sa-whole-hour-line"></div>
                    : undefined}
            </div>
        )
    }
}





type LessonsByDayProps = {
    lessons: PeriodicLessonTimetableEntry[];

}
type LessonsByDayState = {

}
class LessonsByDay extends React.Component<LessonsByDayProps, LessonsByDayState> {

    render() {
        return (
            <>
                {this.props.lessons.map(lesson => 
                    <GenericLessonTile className="sa-placed-lesson"
                        key={`${lesson.time.hour}${lesson.time.minutes}`}
                        time={lesson.time}
                        customDuration={lesson.customDuration}
                    >
                        <div className="sa-lesson-time">
                            {displayTime(lesson.time)}
                        </div>
                        <div className="sa-lesson-subject">
                            {lesson.subject.name}
                        </div>
                        <div className="sa-lesson-lecturer">
                            {lesson.lecturer.name}
                        </div>
                        <div className="sa-lesson-room">
                            {lesson.room?.name}
                        </div>
                    </GenericLessonTile>
                )}
            </>
        )
    }
}






type LessonPlacingShadowProps = {
    time?: Time;
}
type LessonPlacingShadowState = {}
class LessonPlacingShadow extends React.Component<LessonPlacingShadowProps, LessonPlacingShadowState> {

    render() {
        if (!this.props.time) return <></>

        return (
            <GenericLessonTile className="sa-lesson-placing-shadow"
                time={this.props.time}
            >
                <h4>{displayTime(this.props.time)} - {displayTime(sumTimes(this.props.time, { hour: 0, minutes: scheduleArrangerConfig.defaultLessonDuration }))}</h4>
            </GenericLessonTile>
        )
    }
}





type TeacherLessonsShadowProps = {
    lessons?: PeriodicLessonTimetableEntry[];
}
const TeacherLessonsShadow = (props: TeacherLessonsShadowProps) => {
    if (!props.lessons) return <></>;
    return (
        <>
            {props.lessons.map(x => (
                <GenericLessonTile className="sa-lessons-teacher-busy"
                    key={x.id}
                    time={x.time}
                >
                    {x.lecturer.name}
                </GenericLessonTile>
            ))}
        </>
    )
}





type RoomLessonsShadowProps = {
    lessons?: PeriodicLessonTimetableEntry[];
}
const RoomLessonsShadow = (props: RoomLessonsShadowProps) => {
    if (!props.lessons) return <></>;
    return (
        <>
            {props.lessons.map(x => (
                <GenericLessonTile className="sa-lessons-room-busy"
                    key={x.id}
                    time={x.time}
                >
                    {x.room.name}
                </GenericLessonTile>
            ))}
        </>
    )
}





type GenericLessonTileProps = {
    time: Time;
    customDuration?: number;
    className?: string;
    children: React.ReactNode;
}
type GenericLessonTileState = {

}
class GenericLessonTile extends React.Component<GenericLessonTileProps, GenericLessonTileState> {

    private calcTopOffset() {
        const minutes = (this.props.time.hour - scheduleArrangerConfig.startHour) * 60 + this.props.time.minutes;
        const cells = minutes / scheduleArrangerConfig.cellDuration;
        return cells * scheduleArrangerConfig.cellHeight;
    }

    private calcHeight() {
        const duration = this.props.customDuration ?? scheduleArrangerConfig.defaultLessonDuration;
        const cells = duration / scheduleArrangerConfig.cellDuration;
        return cells * scheduleArrangerConfig.cellHeight;
    }

    render() {
        let style: React.CSSProperties = {
            top: this.calcTopOffset(),
            height: this.calcHeight()
        }

        return (
            <div className={`sa-lesson-tile ${this.props.className}`}
                style={style}
            >
                {this.props.children}
            </div>
        )
    }
}






type ScheduleTimeColumnProps = {

}
type ScheduleTimeColumnState = {

}
class ScheduleTimeColumn extends React.Component<ScheduleTimeColumnProps, ScheduleTimeColumnState> {
    private _timeLables: JSX.Element[];

    constructor(props) {
        super(props);

        this._timeLables = [];

        this.addWholeHours();
    }

    addWholeHours() {
        const hours = Array.from({
            length: scheduleArrangerConfig.endHour - scheduleArrangerConfig.startHour
        }, (_, i) => scheduleArrangerConfig.startHour + i);
        let offset = 0;

        for (const hour of hours) {
            this._timeLables.push(this.timeLabel({ hour, minutes: 0 }, offset));
            offset += (60 / scheduleArrangerConfig.cellDuration) * scheduleArrangerConfig.cellHeight;
        }
    }


    timeLabel = (time: Time, top: number) => (
        <div className="sa-time-label"
            key={`${time.hour}${time.minutes}`}
            style={{ top }}
        >
            {displayTime(time)}
        </div>
    )

    render() {
        return (
            <div className="sa-time-column">
                {this._timeLables}
            </div>
        )
    }
}





type DayLabelProps = {
    day: DayOfWeek;
}
const DayLabel = (props: DayLabelProps) => {
    return (
        <div className="sa-day-label">
            {nameForDayOfWeek(props.day)}
        </div>
    )
}