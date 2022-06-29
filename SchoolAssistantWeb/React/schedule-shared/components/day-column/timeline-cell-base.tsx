import React from "react";
import DayOfWeek from "../../enums/day-of-week";
import Time from "../../interfaces/shared/time";
import './timeline-cell-base.css';

export type TimelineCellBaseProps = {
    dayIndicator: DayOfWeek;
    cellIndex: number;
    time: Time;
    height: number;
    containerProps?: React.HTMLAttributes<HTMLDivElement>;
    className?: string;
}
export type TimelineCellBaseState = {

}

/*
 * Small piece of whole DayColumn. SchedArr uses those elements for detecting time, when lesson should be inserted
 * 
 */

export default class TimelineCellBase
    <TProps extends TimelineCellBaseProps,
    TState extends TimelineCellBaseState> extends React.Component<TProps, TState> {

    render() {
        let style: React.CSSProperties = {
            height: this.props.height
        }

        const customContainerProps = this.props.containerProps ?? this.getContainerProps?.();

        let className = "sched-timeline-cell";
        if (this._isWholeHour)
            className += " sched-timeline-cell-whole-hour";
        if (this.props.className)
            className += " " + this.props.className;

        return (
            <div className={className}
                style={style}
                {...customContainerProps ?? {}}
            >
                {this._isWholeHour
                    ? <div className="sched-timeline-whole-hour-line"></div>
                    : undefined}
            </div>
        )
    }

    private get _isWholeHour() { return this.props.time.minutes == 0; }

    protected getContainerProps?(): React.HTMLAttributes<HTMLDivElement>;
}