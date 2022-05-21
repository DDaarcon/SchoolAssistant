import LessonTimelineEntry from "../../schedule-shared/interfaces/lesson-timeline-entry";
import IdName from "../../schedule-shared/interfaces/shared/id-name";

export default interface Lesson extends LessonTimelineEntry {
    orgClass?: IdName;
    subjClass?: IdName;
}