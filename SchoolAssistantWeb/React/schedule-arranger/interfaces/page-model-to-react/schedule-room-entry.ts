import DayLessons from "../../../schedule-shared/interfaces/day-lessons";
import Lesson from "../../../schedule-shared/interfaces/lesson";
import IdName from "../../../schedule-shared/interfaces/shared/id-name";

export default interface ScheduleRoomEntry extends IdName {
    floor: number;
    lessons?: DayLessons<Lesson>[];
}