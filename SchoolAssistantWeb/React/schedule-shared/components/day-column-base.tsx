import React from "react";
import DayOfWeek from "../enums/day-of-week";
import LessonTimelineEntry from "../interfaces/lesson-timeline-entry";
import ScheduleTimelineConfig from "../interfaces/props-models/schedule-timeline-config";
import Time from "../interfaces/shared/time";
import DayLabel from "./day-column/day-label";
import './day-column-base.css';

export type DayColumnBaseProps<TConfig extends ScheduleTimelineConfig, TLesson extends LessonTimelineEntry> = {
    dayIndicator: DayOfWeek;
    config: TConfig;
    lessons: TLesson[];
}
export type DayColumnBaseState = {}

export default abstract class DayColumnBase
    <TProps extends DayColumnBaseProps<TConfig, TLesson>,
    TState extends DayColumnBaseState,
    TConfig extends ScheduleTimelineConfig,
    TLesson extends LessonTimelineEntry> extends React.Component<TProps, TState> {

    protected _cells: JSX.Element[];

    constructor(props) {
        super(props);

        this.state = this.getInitialState();
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