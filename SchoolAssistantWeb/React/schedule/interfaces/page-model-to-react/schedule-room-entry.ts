import { DayLessons } from "../day-lessons";
import { IdName } from "../shared";

export interface ScheduleRoomEntry extends IdName {
    floor: number;
    lessons?: DayLessons[];
}