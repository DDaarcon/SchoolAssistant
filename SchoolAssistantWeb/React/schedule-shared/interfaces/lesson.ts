import LessonTimelineEntry from "./lesson-timeline-entry";
import IdName from "./shared/id-name";

export default interface Lesson extends LessonTimelineEntry {
    orgClass?: IdName;
    subjClass?: IdName;
}