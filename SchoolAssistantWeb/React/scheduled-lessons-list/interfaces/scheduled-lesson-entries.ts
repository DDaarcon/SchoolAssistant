import ScheduledLessonListEntry from "./scheduled-lesson-entry";

export default interface ScheduledLessonListEntries {
    entries: ScheduledLessonListEntry[];
    incomingAtTk?: number;
}