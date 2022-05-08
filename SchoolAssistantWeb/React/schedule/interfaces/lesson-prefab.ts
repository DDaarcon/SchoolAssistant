import { IdName } from "./shared";

export interface LessonPrefab {
    subject: IdName;
    lecturer: IdName;
    room?: IdName;
}