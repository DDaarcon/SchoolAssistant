import DayOfWeek from "../../schedule-shared/enums/day-of-week";
import ScheduleTimelineConfig from "../../schedule-shared/interfaces/props-models/schedule-timeline-config";
import ScheduleViewerType from "../enums/schedule-viewer-type";

export default interface ScheduleConfig extends ScheduleTimelineConfig {
    locale?: string;
    hiddenDays: DayOfWeek[];
    for: ScheduleViewerType;
}