import DayLessons from "../../../schedule-shared/interfaces/day-lessons";
import IdName from "../../../schedule-shared/interfaces/shared/id-name";
import Lesson from "../lesson";

export default interface ScheduleTeacherEntry extends IdName {
    shortName: string;
    mainSubjectIds: number[];
    additionalSubjectIds: number[];
    lessons?: DayLessons<Lesson>[];
}