import React from "react";
import TimelineCellBase, { TimelineCellBaseProps, TimelineCellBaseState } from "../../../schedule-shared/components/day-column/timeline-cell-base";
import DayOfWeek from "../../../schedule-shared/enums/day-of-week";
import Time from "../../../schedule-shared/interfaces/shared/time";
import PlacingAssistantService from "../services/placing-assistant-service";

type TimelineCellProps = TimelineCellBaseProps & {
    dropped: (dayIndicator: DayOfWeek, cellIndex: number, time: Time) => void;
    entered: (dayIndicator: DayOfWeek, cellIndex: number, time: Time) => void;
}
type TimelineCellState = {

}

export default class TimelineCell extends React.Component<TimelineCellProps, TimelineCellState> {

    render() {
        const { dropped, entered, ...rest } = this.props;

        return (
            <TimelineCellBase
                {...rest}
                containerProps={this.getContainerProps()}
            />
        )
    }

    private onDrop: React.DragEventHandler<HTMLDivElement> = (event) => {
        this.props.dropped(
            this.props.dayIndicator,
            this.props.cellIndex,
            this.props.time
        );
    }

    private onDragOver: React.DragEventHandler<HTMLDivElement> = (event) => {
        event.preventDefault();
    }

    private onDragEnter: React.DragEventHandler<HTMLDivElement> = (_) => {
        this.props.entered(
            this.props.dayIndicator,
            this.props.cellIndex,
            this.props.time
        );
    }

    private onMouseEnter: React.MouseEventHandler<HTMLDivElement> = (ev) => {
        if (!PlacingAssistantService.isPlacingBySelection)
            return;
        this.props.entered(
            this.props.dayIndicator,
            this.props.cellIndex,
            this.props.time
        );
    }

    private onClick: React.MouseEventHandler<HTMLDivElement> = (ev) => {
        if (!PlacingAssistantService.isPlacingBySelection)
            return;
        this.props.dropped(
            this.props.dayIndicator,
            this.props.cellIndex,
            this.props.time
        );
    }

    private getContainerProps(): React.HTMLAttributes<HTMLDivElement> {
        return {
            onDrop: this.onDrop,
            onDragOver: this.onDragOver,
            onDragEnter: this.onDragEnter,
            onMouseEnter: this.onMouseEnter,
            onClick: this.onClick
        }
    }
}