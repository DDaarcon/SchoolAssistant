import DayOfWeek from "../../enums/day-of-week";

export default interface ScheduleTimelineConfig {
    startHour: number;
    endHour: number;

    hiddenDays?: DayOfWeek[];
    defaultLessonDuration: number;
}