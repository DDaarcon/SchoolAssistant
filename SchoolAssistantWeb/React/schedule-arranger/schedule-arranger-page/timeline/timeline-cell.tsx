import React from "react";
import TimelineCellBase, { TimelineCellBaseProps, TimelineCellBaseState } from "../../../schedule-shared/components/day-column/timeline-cell-base";
import DayOfWeek from "../../../schedule-shared/enums/day-of-week";
import Time from "../../../schedule-shared/interfaces/shared/time";

type TimelineCellProps = TimelineCellBaseProps & {
    dropped: (dayIndicator: DayOfWeek, cellIndex: number, time: Time, data: DataTransfer) => void;
    entered: (dayIndicator: DayOfWeek, cellIndex: number, time: Time) => void;
}
type TimelineCellState = TimelineCellBaseState & {

}
export default class TimelineCell extends TimelineCellBase<TimelineCellProps, TimelineCellState> {
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

    protected override getContainerProps(): React.HTMLAttributes<HTMLDivElement> {
        return {
            onDrop: this.onDrop,
            onDragOver: this.onDragOver,
            onDragEnter: this.onDragEnter
        }
    }
}