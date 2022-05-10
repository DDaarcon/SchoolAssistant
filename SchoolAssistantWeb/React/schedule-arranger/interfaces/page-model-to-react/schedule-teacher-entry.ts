import { DayLessons } from "../day-lessons";
import { IdName } from "../shared";

export interface ScheduleTeacherEntry extends IdName {
    shortName: string;
    mainSubjectIds: number[];
    additionalSubjectIds: number[];
    lessons?: DayLessons[];
}