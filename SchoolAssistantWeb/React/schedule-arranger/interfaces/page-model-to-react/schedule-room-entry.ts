import DayLessons from "../../../schedule-shared/interfaces/day-lessons";
import IdName from "../../../schedule-shared/interfaces/shared/id-name";
import Lesson from "../lesson";

export default interface ScheduleRoomEntry extends IdName {
    floor: number;
    lessons?: DayLessons<Lesson>[];
}