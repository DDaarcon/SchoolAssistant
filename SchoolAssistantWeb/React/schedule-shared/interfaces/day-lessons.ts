import DayOfWeek from "../enums/day-of-week";
import LessonTimelineEntry from "./lesson-timeline-entry";

export default interface DayLessons<TLesson extends LessonTimelineEntry = LessonTimelineEntry> {
    dayIndicator: DayOfWeek;
    lessons: TLesson[];
}