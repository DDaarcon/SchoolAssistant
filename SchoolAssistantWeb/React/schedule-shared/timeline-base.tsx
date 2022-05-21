import React from 'react';
import TimeColumn from './components/time-column';
import DayOfWeek from './enums/day-of-week';
import DayLessons from './interfaces/day-lessons';
import LessonTimelineEntry from './interfaces/lesson-timeline-entry';
import ScheduleTimelineConfig from './interfaces/props-models/schedule-timeline-config';
import './timeline-base.css';

export type ScheduleTimelineBaseProps<TConfig extends ScheduleTimelineConfig, TLesson extends LessonTimelineEntry> = {
    data: DayLessons<TLesson>[];
    config: TConfig;
}
export type ScheduleTimelineBaseState = {}

export default abstract class ScheduleTimelineBase
    <TProps extends ScheduleTimelineBaseProps<TConfig, TLesson>,
    TState extends ScheduleTimelineBaseState,
    TConfig extends ScheduleTimelineConfig,
    TLesson extends LessonTimelineEntry> extends React.Component<TProps, TState> {

    constructor(props) {
        super(props);

        this.state = this.getInitialState();
    }

    protected getInitialState(): TState {
        return {} as TState;
    }


    protected getDaysOfWeekIterable = (with6th: boolean = false, with0th: boolean = false) =>
        Object.values(DayOfWeek).map(x => parseInt(x as unknown as string)).filter(x =>
            !isNaN(x) && (with0th || x != 0) && (with6th || x != 6)) as DayOfWeek[];

    protected getDayColumnComponent?(day: DayOfWeek): JSX.Element;

    protected getTimeColumnComponent?(): JSX.Element;

    protected className: string;
    protected containerElement: HTMLDivElement;

    render() {
        if (!this.getDayColumnComponent) throw new Error("Overriding `getDayColumnComponent` is mandatory");

        return (
            <div className={"schedule-timeline " + this.className}
                ref={ref => this.containerElement = ref}
            >

                {this.getTimeColumnComponent?.()}

                {this.getDaysOfWeekIterable().map(day => this.getDayColumnComponent!(day))}
            </div>
        )
    }
}