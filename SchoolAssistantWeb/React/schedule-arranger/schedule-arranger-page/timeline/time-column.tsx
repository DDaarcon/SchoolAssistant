﻿import React from "react";
import { displayTime } from "../../../schedule-shared/help/time-functions";
import Time from "../../../schedule-shared/interfaces/shared/time";
import { scheduleArrangerConfig } from "../../main";

type TimeColumnProps = {

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
            length: scheduleArrangerConfig.endHour - scheduleArrangerConfig.startHour
        }, (_, i) => scheduleArrangerConfig.startHour + i);
        let offset = 0;

        for (const hour of hours) {
            this._timeLables.push(this.timeLabel({ hour, minutes: 0 }, offset));
            offset += (60 / scheduleArrangerConfig.cellDuration) * scheduleArrangerConfig.cellHeight;
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