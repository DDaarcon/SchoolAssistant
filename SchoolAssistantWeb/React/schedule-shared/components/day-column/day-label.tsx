import React from "react";
import DayOfWeek from "../../enums/day-of-week";
import { nameForDayOfWeek } from "../../help/weekdays-function";
import './day-label.css';

type DayLabelProps = {
    day: DayOfWeek;
}
const DayLabel = (props: DayLabelProps) => {
    return (
        <div className="sched-timeline-day-label my-raised-bar">
            {nameForDayOfWeek(props.day)}
        </div>
    )
}
export default DayLabel;