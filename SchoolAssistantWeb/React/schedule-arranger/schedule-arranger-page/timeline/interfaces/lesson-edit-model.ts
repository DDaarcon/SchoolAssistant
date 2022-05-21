import DayOfWeek from "../../../../schedule-shared/enums/day-of-week";
import Time from "../../../../schedule-shared/interfaces/shared/time";

export default interface LessonEditModel {
    id: number;
    time: Time;
    day: DayOfWeek;
    customDuration?: number;
    subjectId: number;
    lecturerId?: number;
    roomId: number;
    classId: number;
}