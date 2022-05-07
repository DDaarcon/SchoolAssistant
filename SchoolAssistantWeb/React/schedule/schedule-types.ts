import { ResponseJson } from "../shared/server-connection";
import { DayOfWeek } from "./schedule-arranger-page";


export interface ScheduleClassLessons {
    data: ScheduleDayLessons[];
}

export interface ScheduleDayLessons {
    dayIndicator: DayOfWeek;
    lessons: PeriodicLessonTimetableEntry[];
}

export interface Time {
    hour: number;
    minutes: number;
}

export interface PeriodicLessonTimetableEntry {
    id?: number;

    time: Time;
    customDuration?: number;

    subject: IdName;
    lecturer: IdName;
    room?: IdName;
}

export interface IdName {
    id: number;
    name: string;
}



export interface AddLessonResponse extends ResponseJson {
    lesson?: PeriodicLessonTimetableEntry;
}

export interface ScheduleArrangerConfig {
    defaultLessonDuration: number;
    startHour: number;
    endHour: number;

    cellDuration: number;
    cellHeight: number;

    classId?: number;
}

export interface ScheduleClassSelectorEntry {
    id: number;
    name: string;
    specialization?: string;
}





export interface ScheduleOtherLessons {
    teacher?: ScheduleDayLessons[];
    room?: ScheduleDayLessons[];
}


export interface ScheduleLessonPrefab {
    subject: IdName;
    lecturer: IdName;
    room?: IdName;
}

export interface ScheduleSubjectEntry extends IdName { }
export interface ScheduleTeacherEntry extends IdName {
    shortName: string;
    mainSubjectIds: number[];
    additionalSubjectIds: number[];
    lessons?: ScheduleDayLessons[];
}
export interface ScheduleRoomEntry extends IdName {
    floor: number;
    lessons?: ScheduleDayLessons[];
}






/** Shared by timeline and selector */

export interface ScheduleLessonModificationData {
    subjectId?: number;
    teacherId?: number;
    roomId?: number;
}