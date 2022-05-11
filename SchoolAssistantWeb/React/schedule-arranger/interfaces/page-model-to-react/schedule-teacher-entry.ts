import { DayLessons } from "../day-lessons";
import { Lesson } from "../lesson";
import { IdName } from "../shared";

export interface ScheduleTeacherEntry extends IdName {
    shortName: string;
    mainSubjectIds: number[];
    additionalSubjectIds: number[];
    lessons?: DayLessons<Lesson>[];
}