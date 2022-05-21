import React from "react";
import { displayTime } from "../help/time-functions";
import ScheduleTimelineConfig from "../interfaces/props-models/schedule-timeline-config";
import Time from "../interfaces/shared/time";

type TimeColumnProps = {
    config: ScheduleTimelineConfig;
}
type TimeColumnState = {

}
export default class TimeColumn extends React.Component<TimeColumnProps, TimeColumnState> {
    private _timeLables: JSX.Element[];

    constructor(props) {
        super(props);

        this._timeLables = [];

        this.addWholeHours();
    }

    addWholeHours() {
        const hours = Array.from({
            length: this.props.config.endHour - this.props.config.startHour
        }, (_, i) => this.props.config.startHour + i);
        let offset = 0;

        for (const hour of hours) {
            this._timeLables.push(this.timeLabel({ hour, minutes: 0 }, offset));
            offset += (60 / this.props.config.cellDuration) * this.props.config.cellHeight;
        }
    }


    timeLabel = (time: Time, top: number) => (
        <div className="sa-time-label"
            key={`${time.hour}${time.minutes}`}
            style={{ top }}
        >
            {displayTime(time)}
        </div>
    )

    render() {
        return (
            <div className="sa-time-column">
                {this._timeLables}
            </div>
        )
    }
}