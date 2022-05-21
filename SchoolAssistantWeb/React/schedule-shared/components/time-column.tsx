import React from "react";
import TimeColumnVariant from "../enums/time-column-variant";
import { displayTime } from "../help/time-functions";
import ScheduleTimelineConfig from "../interfaces/props-models/schedule-timeline-config";
import Time from "../interfaces/shared/time";
import './time-column.css';

type TimeColumnProps = {
    config: ScheduleTimelineConfig;
    variant?: TimeColumnVariant;

    scheduleHeight?: number;
}
type TimeColumnState = {

}

export default class TimeColumn extends React.Component<TimeColumnProps, TimeColumnState> {



    render() {
        this._timeLables = [];

        this.addTimeLabelsByVariant();

        return (
            <div className="sched-time-column">
                {this._timeLables}
            </div>
        )
    }

    private _timeLables: JSX.Element[];

    private addTimeLabelsByVariant() {
        switch (this.props.variant ?? TimeColumnVariant.WholeHoursByConfig) {
            case TimeColumnVariant.WholeHoursByConfig:
                this.addWholeHoursByConfig();
                break;
            case TimeColumnVariant.WholeHoursByHeight:
                this.addWholeHoursByHeight();
                break;
        }
    }

    private addWholeHoursByConfig() {
        const hours = this._wholeHoursToDisplay;

        const offsetIncrement = (60 / this.props.config.cellDuration) * this.props.config.cellHeight;
        let offset = 0;

        for (const hour of hours) {
            this._timeLables.push(this.timeLabel({ hour, minutes: 0 }, offset));
            offset += offsetIncrement;
        }
    }

    private addWholeHoursByHeight() {
        const hours = this._wholeHoursToDisplay;
        if (!hours.length) return;

        const offsetIncrement = (this.props.scheduleHeight ?? 0) / hours.length;
        let offset = 0;

        for (const hour of hours) {
            this._timeLables.push(this.timeLabel({ hour, minutes: 0 }, offset));
            offset += offsetIncrement;
        }
    }

    private get _wholeHoursToDisplay() {
        return Array.from({
            length: this.props.config.endHour - this.props.config.startHour
        }, (_, i) => this.props.config.startHour + i);
    }

    private timeLabel = (time: Time, top: number) => (
        <div className="sched-time-label"
            key={`${time.hour}${time.minutes}`}
            style={{ top }}
        >
            {displayTime(time)}
        </div>
    )
}