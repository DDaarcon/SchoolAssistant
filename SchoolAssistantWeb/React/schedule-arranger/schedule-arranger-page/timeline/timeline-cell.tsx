import React from "react";
import TimelineCellBase, { TimelineCellBaseProps, TimelineCellBaseState } from "../../../schedule-shared/components/day-column/timeline-cell-base";
import DayOfWeek from "../../../schedule-shared/enums/day-of-week";
import Time from "../../../schedule-shared/interfaces/shared/time";
import PlacingAssistantService from "../services/placing-assistant-service";

type TimelineCellProps = TimelineCellBaseProps & {
    dropped: (dayIndicator: DayOfWeek, time: Time) => void;
    mouseEntered: (dayIndicator: DayOfWeek, time: Time) => void;
    touched: (dayIndicator: DayOfWeek, time: Time) => void;
}
type TimelineCellState = {

}

export default class TimelineCell extends React.Component<TimelineCellProps, TimelineCellState> {

    render() {
        const { dropped, mouseEntered, ...rest } = this.props;

        return (
            <TimelineCellBase
                {...rest}
                containerProps={this.getContainerProps()}
            />
        )
    }
    

    private getContainerProps(): React.HTMLAttributes<HTMLDivElement> {
        return {
            onDrop: this.dropped,
            onDragOver: this.draggedOver,
            onDragEnter: this.dragEntered,
            onMouseEnter: this.mouseEntered,
            onClick: this.clicked,
            onTouchStart: this.fingerIsDown,
            onTouchMove: this.fingerMoved,
            onTouchEnd: this.fingerIsUp
        }
    }

    private dropped: React.DragEventHandler<HTMLDivElement> = (event) => {
        this.props.dropped(
            this.props.dayIndicator,
            this.props.time
        );
    }

    private draggedOver: React.DragEventHandler<HTMLDivElement> = (event) => {
        event.preventDefault();
    }

    private dragEntered: React.DragEventHandler<HTMLDivElement> = (_) => {
        this.props.mouseEntered(
            this.props.dayIndicator,
            this.props.time
        );
    }

    private mouseEntered: React.MouseEventHandler<HTMLDivElement> = (ev) => {
        if (!PlacingAssistantService.isPlacingBySelection)
            return;
        this.props.mouseEntered(
            this.props.dayIndicator,
            this.props.time
        );
    }


    private _preventCLick = false;

    private clicked: React.MouseEventHandler<HTMLDivElement> = () => {
        if (this._preventCLick) {
            this._preventCLick = false;
            return;
        }

        if (!PlacingAssistantService.isPlacingBySelection)
            return;

        this.props.dropped(
            this.props.dayIndicator,
            this.props.time
        );
    }


    private _awaitNextTouchEvent = false;

    private fingerIsDown: React.TouchEventHandler<HTMLDivElement> = (ev) => {
        this._awaitNextTouchEvent = true;
        this._preventCLick = true;
    }

    private fingerMoved: React.TouchEventHandler<HTMLDivElement> = (ev) => {
        this._awaitNextTouchEvent = false;
        this._preventCLick = false;
    }
    private fingerIsUp: React.TouchEventHandler<HTMLDivElement> = (ev) => {
        if (!this._awaitNextTouchEvent)
            return;
        this._awaitNextTouchEvent = false;
        setTimeout(() => this._preventCLick = false, 1000);

        this.props.touched(
            this.props.dayIndicator,
            this.props.time
        );
    }
}