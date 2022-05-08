import { DayOfWeek } from "../enums/day-of-week";
import { LessonTimelineEntry } from "./lesson-timeline-entry";

export interface DayLessons {
    dayIndicator: DayOfWeek;
    lessons: LessonTimelineEntry[];
}