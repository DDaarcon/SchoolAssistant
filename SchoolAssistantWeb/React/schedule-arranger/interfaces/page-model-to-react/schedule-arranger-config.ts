import ScheduleTimelineConfig from "../../../schedule-shared/interfaces/props-models/schedule-timeline-config";

export default interface ScheduleArrangerConfig extends ScheduleTimelineConfig {
    cellDuration: number;
    cellHeight: number;

    classId?: number;
}