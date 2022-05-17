import DayOfWeek from "../enums/day-of-week";
import ScheduleViewerType from "../enums/schedule-viewer-type";

export default interface ScheduleConfig {
    locale?: string;
    hiddenDays: DayOfWeek[];
    startHour: number;
    endHour: number;
    for: ScheduleViewerType;
}