import React from "react";
import TimeColumn from "../../schedule-shared/components/time-column";
import DayOfWeek from "../../schedule-shared/enums/day-of-week";
import TimeColumnVariant from "../../schedule-shared/enums/time-column-variant";
import DayLessons from "../../schedule-shared/interfaces/day-lessons";
import Lesson from "../../schedule-shared/interfaces/lesson";
import ScheduleTimeline from "../../schedule-shared/schedule-timeline";
import ScheduleConfig from "../interfaces/schedule-config";
import DayColumn from "./day-column";
import './schedule-display-timeline.css';

type ScheduleDisplayTimelineProps = {
    config: ScheduleConfig;
    data: DayLessons<Lesson>[];
}
type ScheduleDisplayTimelineState = {
    scheduleHeight: number;
}

export default class ScheduleDisplayTimeline extends React.Component<ScheduleDisplayTimelineProps, ScheduleDisplayTimelineState> {
    constructor(props) {
        super(props);

        this.state = {
            scheduleHeight: 200
        }
    }

    render() {
        return (
            <div className={this._fullClassName}
                ref={ref => this._containerRef = ref}
            >
                <ScheduleTimeline
                    config={this.props.config}
                    dayColumnFactory={this.dayColumnFactory}
                    timeColumn={
                        <TimeColumn
                            {...this.props.config}
                            scheduleHeight={this.state.scheduleHeight}
                            variant={TimeColumnVariant.WholeHoursByHeight}
                        />
                    }
                    getReferenceOnMount={ref => this._scheduleRef = ref}
                />
            </div>
        )
    }


    private dayColumnFactory = (day: DayOfWeek): JSX.Element => {
        const lessons = this.props.data.find(x => x.dayIndicator == day)?.lessons ?? [];
        return (
            <DayColumn
                key={day}
                scheduleHeight={this.state.scheduleHeight}
                config={this.props.config}
                dayIndicator={day}
                lessons={lessons}
            />
        )
    }

    private _scheduleRef: HTMLDivElement;
    private _containerRef: HTMLDivElement;

    private readonly HIDDEN_CLASS_NAME = "schedule-hidden";

    private get _fullClassName() {
        let className = `schedule-display-timeline-container ${this.HIDDEN_CLASS_NAME}`;
        return className;
    }

    componentDidMount() {
        this._containerRef.classList.remove(this.HIDDEN_CLASS_NAME);

        this.setState({ scheduleHeight: this._scheduleRef.clientHeight });
    }
}