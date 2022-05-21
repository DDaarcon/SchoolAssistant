import LessonTimelineEntry from "../../schedule-shared/interfaces/lesson-timeline-entry";
import { ResponseJson } from "../../shared/server-connection";

export interface AddLessonResponse extends ResponseJson {
    lesson?: LessonTimelineEntry;
}