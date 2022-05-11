import { DayLessons } from "../day-lessons";
import { Lesson } from "../lesson";
import { IdName } from "../shared";

export interface ScheduleRoomEntry extends IdName {
    floor: number;
    lessons?: DayLessons<Lesson>[];
}