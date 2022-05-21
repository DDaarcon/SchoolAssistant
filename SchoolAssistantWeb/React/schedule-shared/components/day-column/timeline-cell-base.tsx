import React from "react";
import DayOfWeek from "../../enums/day-of-week";
import ScheduleTimelineConfig from "../../interfaces/props-models/schedule-timeline-config";
import Time from "../../interfaces/shared/time";
import './timeline-cell-base.css';

export type TimelineCellBaseProps = {
    dayIndicator: DayOfWeek;
    cellIndex: number;
    time: Time;
    config: ScheduleTimelineConfig;
}
export type TimelineCellBaseState = {

}
export default abstract class TimelineCellBase
    <TProps extends TimelineCellBaseProps,
    TState extends TimelineCellBaseState> extends React.Component<TProps, TState> {
    private get _wholeHour() { return this.props.time.minutes == 0; }

    protected getContainerProps?(): React.HTMLAttributes<HTMLDivElement>;

    render() {
        let style: React.CSSProperties = {
            height: this.props.config.cellHeight
        }

        return (
            <div className={"sched-timeline-cell" + (this._wholeHour ? " sched-timeline-cell-whole-hour" : "")}
                style={style}
                {...this.getContainerProps?.()}
            >
                {this._wholeHour
                    ? <div className="sched-timeline-whole-hour-line"></div>
                    : undefined}
            </div>
        )
    }
}