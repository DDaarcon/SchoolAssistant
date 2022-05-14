import { DayOfWeek } from "../../../enums/day-of-week";
import { Time } from "../../../interfaces/shared";

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