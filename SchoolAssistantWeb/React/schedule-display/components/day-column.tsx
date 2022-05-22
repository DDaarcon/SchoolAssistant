import React from "react";
import DayColumnBase, { DayColumnBaseProps, DayColumnBaseState } from "../../schedule-shared/components/day-column-base";
import Lesson from "../../schedule-shared/interfaces/lesson";
import Time from "../../schedule-shared/interfaces/shared/time";
import ScheduleConfig from "../interfaces/schedule-config";
import SETTINGS from "../settings";
import LessonsByDay from "./lessons-by-day";
import TimelineCell from "./timeline-cell";

type DayColumnProps = DayColumnBaseProps<ScheduleConfig, Lesson> & {
    scheduleHeight: number;
};
type DayColumnState = DayColumnBaseState;

export default class DayColumn extends DayColumnBase<DayColumnProps, DayColumnState, ScheduleConfig, Lesson> {

    private get _cellDuration() { return 60 / SETTINGS.CellsPerHour; }

    private _cellHeight: number;

    constructor(props) {
        super(props);

        this.instantiateCells();
    }

    private instantiateCells() {
        if (!this.getTimelineCellComponent) throw new Error("Overriding method `getTimelineCellComponent` is required for calling `instantiateCells`");

        const count = (this.props.config.endHour - this.props.config.startHour) * SETTINGS.CellsPerHour;
        this._cellHeight = this.props.scheduleHeight / count;

        const cellTimes = Array.from({ length: count }, (_, i): Time => {
            const minutesFromMidnight = (this.props.config.startHour * 60) + this._cellDuration * i;
            return {
                hour: Math.floor(minutesFromMidnight / 60),
                minutes: minutesFromMidnight % 60
            };
        })

        this._cells = cellTimes.map((cellTime, i) => this.getTimelineCellComponent(cellTime, i));
    }

    protected override getLessonsDisplayComponent(): JSX.Element {
        return (
            <LessonsByDay
                lessons={this.props.lessons}
                day={this.props.dayIndicator}
                config={this.props.config}
                cellHeight={this._cellHeight}
            />
        )
    }

    protected getTimelineCellComponent(time: Time, index: number): JSX.Element {
        return (
            <TimelineCell
                key={index}
                height={this._cellHeight}
                dayIndicator={this.props.dayIndicator}
                cellIndex={index}
                time={time}
            />
        )
    }

    render() {
        this.instantiateCells();

        return super.render();
    }
}