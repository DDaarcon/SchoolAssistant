

interface ScheduleClassLessons {
    data: ScheduleDayLessons[];
}

interface ScheduleDayLessons {
    dayIndicator: DayOfWeek;
    lessons: PeriodicLessonTimetableEntry[];
}

interface Time {
    hour: number;
    minutes: number;
}

interface PeriodicLessonTimetableEntry {
    id?: number;

    time: Time;
    customDuration?: number;

    subject: IdName;
    lecturer: IdName;
    room?: IdName;
}

interface IdName {
    id: number;
    name: string;
}



interface AddLessonResponse extends ResponseJson {
    lesson?: PeriodicLessonTimetableEntry;
}

interface ScheduleArrangerConfig {
    defaultLessonDuration: number;
    startHour: number;
    endHour: number;

    cellDuration: number;
    cellHeight: number;

    classId?: number;
}

interface ScheduleClassSelectorEntry {
    id: number;
    name: string;
    specialization?: string;
}





interface ScheduleOtherLessons {
    teacher?: ScheduleDayLessons[];
    room?: ScheduleDayLessons[];
}


interface ScheduleLessonPrefab {
    subject: IdName;
    lecturer: IdName;
    room?: IdName;
}

interface ScheduleSubjectEntry extends IdName { }
interface ScheduleTeacherEntry extends IdName {
    shortName: string;
    mainSubjectIds: number[];
    additionalSubjectIds: number[];
    lessons?: ScheduleDayLessons[];
}
interface ScheduleRoomEntry extends IdName {
    floor: number;
    lessons?: ScheduleDayLessons[];
}






/** Shared by timeline and selector */

interface ScheduleLessonModificationData {
    subjectId?: number;
    teacherId?: number;
    roomId?: number;
}