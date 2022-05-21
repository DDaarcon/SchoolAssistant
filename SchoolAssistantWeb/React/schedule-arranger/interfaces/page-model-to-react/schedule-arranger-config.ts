import ScheduleTimelineConfig from "../../../schedule-shared/interfaces/props-models/schedule-timeline-config";

export default interface ScheduleArrangerConfig extends ScheduleTimelineConfig {
    defaultLessonDuration: number;
    classId?: number;
}