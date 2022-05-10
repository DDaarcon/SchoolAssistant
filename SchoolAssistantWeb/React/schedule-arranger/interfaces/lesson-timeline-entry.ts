import { IdName, Time } from "./shared";

export interface LessonTimelineEntry {
    id?: number;

    time: Time;
    customDuration?: number;

    subject: IdName;
    lecturer: IdName;
    room?: IdName;
}