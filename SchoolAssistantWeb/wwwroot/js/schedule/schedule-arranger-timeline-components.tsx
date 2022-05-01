type ScheduleDayColumnProps = {
    dayIndicator: DayOfWeek;
    lessons: PeriodicLessonTimetableEntry[];

    dropped: (dayIndicator: DayOfWeek, cellIndex: number, data: DataTransfer) => void;
}
type ScheduleDayColumnState = {

}
class ScheduleDayColumn extends React.Component<ScheduleDayColumnProps, ScheduleDayColumnState> {
    private _cells: JSX.Element[];

    constructor(props) {
        super(props);

        this.instantiateCells();
    }

    instantiateCells = () => {
        const cellsPerHour = 60 / scheduleArrangerConfig.cellDuration;
        const count = (scheduleArrangerConfig.endHour - scheduleArrangerConfig.startHour) * cellsPerHour;

        this._cells = [];
        for (let i = 0; i < count; i++)
            this._cells.push(
                <ScheduleCell
                    key={i}
                    dayIndicator={this.props.dayIndicator}
                    cellIndex={i}
                    dropped={this.props.dropped}
                    entered={this.onEntered}
                    wholeHour={i % cellsPerHour == 0}
                />);
    }

    onEntered = (dayIndicator: DayOfWeek, cellIndex: number) => {

    }

    render() {

        return (
            <div className="sa-schedule-day-column">
                {this._cells}
            </div>
        )
    }
}





type ScheduleCellProps = {
    dayIndicator: DayOfWeek;
    cellIndex: number;
    wholeHour: boolean;

    dropped: (dayIndicator: DayOfWeek, cellIndex: number, data: DataTransfer) => void;
    entered: (dayIndicator: DayOfWeek, cellIndex: number) => void;
}
type ScheduleCellState = {

}
class ScheduleCell extends React.Component<ScheduleCellProps, ScheduleCellState> {
    onDrop: React.DragEventHandler<HTMLDivElement> = (event) => {
        this.props.dropped(
            this.props.dayIndicator,
            this.props.cellIndex,
            event.dataTransfer
        );
    }

    onDragOver: React.DragEventHandler<HTMLDivElement> = (event) => {
        event.preventDefault();
    }

    onDragEnter: React.DragEventHandler<HTMLDivElement> = (_) => {
        this.props.entered(
            this.props.dayIndicator,
            this.props.cellIndex
        );
    }
    render() {
        let style: React.CSSProperties = {
            height: scheduleArrangerConfig.cellHeight
        }

        return (
            <div className={"sa-schedule-cell" + (this.props.wholeHour ? " sa-schedule-cell-whole-hour" : "")}
                style={style}
                onDrop={this.onDrop}
                onDragOver={this.onDragOver}
                onDragEnter={this.onDragEnter}
            ></div>
        )
    }
}





type LessonsByDayProps = {
    lessons: PeriodicLessonTimetableEntry[];

}
type LessonsByDayState = {

}
class LessonsByDay extends React.Component<LessonsByDayProps, LessonsByDayState> {


}





type GenericLessonTileProps = {
    lesson: PeriodicLessonTimetableEntry;

}
type GenericLessonTileState = {

}
class GenericLessonTile extends React.Component<GenericLessonTileProps, GenericLessonTileState> {

    private calcTopOffset() {
        const minutes = (this.props.lesson.hour - scheduleArrangerConfig.startHour) * 60 + this.props.lesson.minutes;
        const cells = minutes / scheduleArrangerConfig.cellDuration;
        return cells * scheduleArrangerConfig.cellHeight;
    }

    private calcHeight() {
        const duration = this.props.lesson.customDuration ?? scheduleArrangerConfig.defaultLessonDuration;
        const cells = duration / scheduleArrangerConfig.cellDuration;
        return cells * scheduleArrangerConfig.cellHeight;
    }

    render() {
        let style: React.CSSProperties = {
            top: this.calcTopOffset(),
            height: this.calcHeight()
        }

        return (
            <div className="sa-lesson-tile"
                style={style}
            >
                {this.props.lesson.subjectName}
            </div>
        )
    }
}