import { LessonTimelineEntry } from "./lesson-timeline-entry";
import { IdName } from "./shared";

export interface Lesson extends LessonTimelineEntry {
    orgClass?: IdName;
    subjClass?: IdName;
}