import React from "react";
import { DayOfWeek } from "../../enums/day-of-week";
import { nameForDayOfWeek } from "../../help-functions";

type DayLabelProps = {
    day: DayOfWeek;
}
const DayLabel = (props: DayLabelProps) => {
    return (
        <div className="sa-day-label">
            {nameForDayOfWeek(props.day)}
        </div>
    )
}
export default DayLabel;