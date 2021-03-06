import HeldClasses from "./held-classes";

export default interface ScheduledLessonListEntry {
    startTimeTk: number;
    duration: number;
    className: string;
    subjectName: string;
    heldClasses?: HeldClasses;

    alreadyAdded?: true;
    startTime?: Date;
}