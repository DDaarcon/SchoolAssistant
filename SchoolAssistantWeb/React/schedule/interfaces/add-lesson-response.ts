import { ResponseJson } from "../../shared/server-connection";
import { LessonTimelineEntry } from "./lesson-timeline-entry";

export interface AddLessonResponse extends ResponseJson {
    lesson?: LessonTimelineEntry;
}