import React from "react";
import DayOfWeek from "../enums/day-of-week";
import LessonTimelineEntry from "../interfaces/lesson-timeline-entry";
import ScheduleTimelineConfig from "../interfaces/props-models/schedule-timeline-config";
import Time from "../interfaces/shared/time";
import DayLabel from "./day-column/day-label";
import './day-column-base.css';

export type DayColumnBaseProps = {
    dayIndicator: DayOfWeek;
    config: ScheduleTimelineConfig;
    lessons: LessonTimelineEntry[];
}
export type DayColumnBaseState = {}

export default abstract class DayColumnBase
    <TProps extends DayColumnBaseProps,
     TState extends DayColumnBaseState> extends React.Component<TProps, TState> {

    private _cells: JSX.Element[];

    constructor(props) {
        super(props);

        this.state = this.getInitialState();
    }


    protected instantiateCells() {
        if (!this.getTimelineCellComponent) throw new Error("Overriding method `getTimelineCellComponent` is required for calling `instantiateCells`");

        const cellsPerHour = 60 / this.props.config.cellDuration;
        const count = (this.props.config.endHour - this.props.config.startHour) * cellsPerHour;

        const cellTimes = Array.from({ length: count }, (_, i): Time => {
            const minutesFromMidnight = (this.props.config.startHour * 60) + this.props.config.cellDuration * i;
            return {
                hour: Math.floor(minutesFromMidnight / 60),
                minutes: minutesFromMidnight % 60
            };
        })

        this._cells = cellTimes.map((cellTime, i) => this.getTimelineCellComponent(cellTime, i));
    }

    protected getInitialState(): TState {
        return {} as TState;
    }

    protected getContainerProps?(): React.HTMLAttributes<HTMLDivElement> | undefined;

    protected getLessonsDisplayComponent?(): JSX.Element;
    protected getTimelineCellComponent?(time: Time, index: number): JSX.Element;
    protected getAdditionalComponents?(): JSX.Element[] | JSX.Element | undefined;

    render() {
        return (
            <div className="sched-timeline-day-column"
                {...this.getContainerProps?.()}
            >

                <DayLabel day={this.props.dayIndicator} />

                {this.getAdditionalComponents?.()}

                {this.getLessonsDisplayComponent?.()}

                {this._cells}

            </div>
        )
    }
}