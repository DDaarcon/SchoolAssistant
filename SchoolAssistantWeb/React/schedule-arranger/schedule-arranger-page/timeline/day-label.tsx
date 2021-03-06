import React from "react";
import DayOfWeek from "../../../schedule-shared/enums/day-of-week";
import { nameForDayOfWeek } from "../../../schedule-shared/help/weekdays-function";

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