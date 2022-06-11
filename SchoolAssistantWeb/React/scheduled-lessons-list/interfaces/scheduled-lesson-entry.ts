import HeldClasses from "./held-classes";

export default interface ScheduledLessonEntry {
    startTimeTk: number;
    duration: number;
    className: string;
    subjectName: string;
    heldClasses?: HeldClasses;
}