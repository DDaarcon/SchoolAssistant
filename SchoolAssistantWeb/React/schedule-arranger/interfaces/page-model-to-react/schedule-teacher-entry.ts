import DayLessons from "../../../schedule-shared/interfaces/day-lessons";
import Lesson from "../../../schedule-shared/interfaces/lesson";
import IdName from "../../../schedule-shared/interfaces/shared/id-name";

export default interface ScheduleTeacherEntry extends IdName {
    shortName: string;
    mainSubjectIds: number[];
    additionalSubjectIds: number[];
    lessons?: DayLessons<Lesson>[];
}