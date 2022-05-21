import IdName from "./shared/id-name";
import Time from "./shared/time";

export default interface LessonTimelineEntry {
    id?: number;

    time: Time;
    customDuration?: number;

    subject: IdName;
    lecturer: IdName;
    room?: IdName;
}