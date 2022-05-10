import React from "react";
import { DayOfWeek } from "../../enums/day-of-week";
import { Time } from "../../interfaces/shared";
import { scheduleArrangerConfig } from "../../main";

type TimelineCellProps = {
    dayIndicator: DayOfWeek;
    cellIndex: number;
    time: Time;

    dropped: (dayIndicator: DayOfWeek, cellIndex: number, time: Time, data: DataTransfer) => void;
    entered: (dayIndicator: DayOfWeek, cellIndex: number, time: Time) => void;
}
type TimelineCellState = {

}
export default class TimelineCell extends React.Component<TimelineCellProps, TimelineCellState> {
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