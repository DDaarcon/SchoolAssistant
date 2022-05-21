import DayOfWeek from "../schedule-shared/enums/day-of-week";
import { areTimesOverlappingByDuration } from "../schedule-shared/help/time-functions";
import DayLessons from "../schedule-shared/interfaces/day-lessons";
import Lesson from "../schedule-shared/interfaces/lesson";
import Time from "../schedule-shared/interfaces/shared/time";
import { modalController } from "../shared/modals";
import { ResponseJson } from "../shared/server-connection";
import LessonPrefab from "./interfaces/lesson-prefab";
import OtherLessons from "./interfaces/other-lessons";
import ScheduleClassSelectorEntry from "./interfaces/page-model-to-react/schedule-class-selector-entry";
import ScheduleRoomEntry from "./interfaces/page-model-to-react/schedule-room-entry";
import ScheduleSubjectEntry from "./interfaces/page-model-to-react/schedule-subject-entry";
import ScheduleTeacherEntry from "./interfaces/page-model-to-react/schedule-teacher-entry";
import { scheduleArrangerConfig, server } from "./main";
import TeachersBySubjectSvc from "./services/teachers-by-subject-svc";

class ScheduleArrangerDataService {
    prefabs: LessonPrefab[] = [];

    classes?: ScheduleClassSelectorEntry[];
    lessons?: DayLessons[];

    subjects?: ScheduleSubjectEntry[];
    teachers?: ScheduleTeacherEntry[];
    rooms?: ScheduleRoomEntry[];

    addPrefab(prefab: LessonPrefab) {
        this.prefabs.push(prefab);

        dispatchEvent(new CustomEvent('newPrefab', {
            detail: prefab
        }));
    }

    getSubjectName = (id: number) => this.subjects.find(x => x.id == id).name;
    getTeacherName = (id: number) => this.teachers.find(x => x.id == id).shortName;
    getRoomName = (id: number) => this.rooms.find(x => x.id == id).name;

    isTileDragged: boolean = false;

    getTeacherAndRoomLessonsAsync = async (teacherId: number, roomId: number, apply: (teacher?: DayLessons<Lesson>[], room?: DayLessons<Lesson>[]) => void) => {
        const teacher = this.teachers.find(x => x.id == teacherId);
        const room = this.rooms.find(x => x.id == roomId);
        if (!teacher || !room)
            return;

        apply(teacher.lessons, room.lessons);

        if (await this.fetchFromServerAsync(teacher, room)) {
            apply(teacher.lessons, room.lessons);
        }
    }


    assignDaysFromProps(days: DayLessons[]) {
        this.lessons = [];
        for (const dayOfWeekIt in DayOfWeek) {
            if (isNaN(dayOfWeekIt as unknown as number)) continue;

            const dayOfWeek = dayOfWeekIt as unknown as DayOfWeek;
            this.lessons.push(
                days.find(x => x.dayIndicator == dayOfWeek)
                ?? { dayIndicator: dayOfWeek, lessons: [] }
            );
        }
    }

    async getOverlappingLessonsAsync(checkFor: {
        day: DayOfWeek;
        time: Time;
        customDuration?: number;
        teacherId?: number;
        roomId?: number;
    }, exceptId?: number)
    {
        const teacher = this.teachers.find(x => x.id == checkFor.teacherId);
        const room = this.rooms.find(x => x.id == checkFor.roomId);

        await this.fetchFromServerAsync(teacher, room);

        const selectedClass = this.classes!.find(x => x.id == scheduleArrangerConfig.classId);
        
        const lessons = this.lessons.find(x => x.dayIndicator == checkFor.day)
            .lessons.map<Lesson>(x => ({ ...x, orgClass: { id: selectedClass.id, name: selectedClass.name } }))

            .concat(teacher?.lessons.find(x => x.dayIndicator == checkFor.day)?.lessons ?? [])
            .concat(room?.lessons.find(x => x.dayIndicator == checkFor.day)?.lessons ?? [])
            .filter(x => x.id != exceptId);

        return lessons.filter(x => areTimesOverlappingByDuration(
            checkFor.time,
            checkFor.customDuration ?? scheduleArrangerConfig.defaultLessonDuration,
            x.time,
            x.customDuration ?? scheduleArrangerConfig.defaultLessonDuration
        ));
    }


    getTeachersBySubject(subjectId: number) {
        const svc = new TeachersBySubjectSvc(this.teachers);
        return svc.getFor(subjectId);
    }

    getLessonById(id: number) {
        for (const lessons of this.lessons) {
            const lesson = lessons.lessons.find(x => x.id == id);
            if (lesson)
                return { day: lessons.dayIndicator, lesson }
        }
        return undefined;
    }

    async removeLessonAndGetResultAsync(id: number) {
        const dayAndLesson = this.getLessonById(id);
        if (!dayAndLesson) return true;

        if (!await new Promise<boolean>(resolve => {
            modalController.addConfirmation({
                header: "Usuwanie zajęć",
                text: `Czy na pewno chcesz usunąć zajęcia z przedmiotu '${dayAndLesson.lesson.subject.name}'?`,
                onConfirm: () => resolve(true),
                onDecline: () => resolve(false)
            });
        }))
            return false;

        const response = await server.postAsync<ResponseJson>("DeleteLesson", {
            id: dayAndLesson.lesson.id
        });

        if (response.success) {
            this.lessons[dayAndLesson.day].lessons.splice(this.lessons[dayAndLesson.day].lessons.indexOf(dayAndLesson.lesson), 1);
            dispatchEvent(new Event("timeline-lessons-rerender"));
        }
        else console.log(response.message)

        return response.success;
    }

    private async fetchFromServerAsync(teacher?: ScheduleTeacherEntry, room?: ScheduleRoomEntry): Promise<boolean> {
        const fetchTeacher = teacher != undefined && teacher?.lessons == undefined,
            fetchRoom = room != undefined && room?.lessons == undefined;

        if (!fetchTeacher && !fetchRoom) return false;

        var response = await server.getAsync<OtherLessons>("OtherLessons", {
            classId: scheduleArrangerConfig.classId,
            teacherId: fetchTeacher ? teacher?.id : undefined,
            roomId: fetchRoom ? room?.id : undefined
        });

        if (response) {
            teacher.lessons ?? (teacher.lessons = response.teacher);
            room.lessons ?? (room.lessons = response.room);
        }
        return true;
    }
}
const dataService = new ScheduleArrangerDataService;
export default dataService;