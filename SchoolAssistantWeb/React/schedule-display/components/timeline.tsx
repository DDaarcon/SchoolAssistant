import React from "react";
import TimeColumn from "../../schedule-shared/components/time-column";
import DayOfWeek from "../../schedule-shared/enums/day-of-week";
import TimeColumnVariant from "../../schedule-shared/enums/time-column-variant";
import Lesson from "../../schedule-shared/interfaces/lesson";
import ScheduleTimelineBase, { ScheduleTimelineBaseProps, ScheduleTimelineBaseState } from "../../schedule-shared/timeline-base";
import ScheduleConfig from "../interfaces/schedule-config";
import DayColumn from "./day-column";
import './timeline.css';

type ScheduleDisplayTimelineProps = ScheduleTimelineBaseProps<ScheduleConfig, Lesson> & { }
type ScheduleDisplayTimelineState = ScheduleTimelineBaseState & {
    scheduleHeight: number;
}

export default class ScheduleDisplayTimeline extends ScheduleTimelineBase<ScheduleDisplayTimelineProps, ScheduleDisplayTimelineState, ScheduleConfig, Lesson> {
    constructor(props) {
        super(props);

        this.className = "schedule-display-timeline";
    }

    protected override getInitialState(): ScheduleDisplayTimelineState {
        return { scheduleHeight: 200 };
    }

    protected override getDayColumnComponent(day: DayOfWeek): JSX.Element {
        return (
            <DayColumn
                key={day}
                scheduleHeight={this.state.scheduleHeight}
                config={this.props.config}
                dayIndicator={day}
                lessons={this.props.data.find(x => x.dayIndicator == day)?.lessons ?? []}
            />
        )
    }

    protected override getTimeColumnComponent(): JSX.Element {
        return (
            <TimeColumn
                { ...this.props.config }
                scheduleHeight={this.state.scheduleHeight}
                variant={TimeColumnVariant.WholeHoursByHeight}
            />
        )
    }

    componentDidMount() {
        this.setState({ scheduleHeight: this.containerElement.clientHeight });
    }
}