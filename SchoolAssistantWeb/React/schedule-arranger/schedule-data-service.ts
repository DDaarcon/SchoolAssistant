import { DayLessons } from "./interfaces/day-lessons";
import { LessonPrefab } from "./interfaces/lesson-prefab";
import { OtherLessons } from "./interfaces/other-lessons";
import { ScheduleRoomEntry } from "./interfaces/page-model-to-react/schedule-room-entry";
import { ScheduleSubjectEntry } from "./interfaces/page-model-to-react/schedule-subject-entry";
import { ScheduleTeacherEntry } from "./interfaces/page-model-to-react/schedule-teacher-entry";
import { scheduleArrangerConfig, server } from "./main";

class ScheduleArrangerDataService {
    prefabs: LessonPrefab[] = [];

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

    getTeacherAndRoomLessons = async (teacherId: number, roomId: number, apply: (teacher?: DayLessons[], room?: DayLessons[]) => void) => {
        // find teacher and room in storage
        const teacher = this.teachers.find(x => x.id == teacherId);
        const room = this.rooms.find(x => x.id == roomId);
        if (!teacher || !room)
            return;

        // get their lessons and send back to display
        const teacherLessons = teacher.lessons;
        const roomLessons = room.lessons;
        apply(teacherLessons, roomLessons);

        if (teacherLessons && roomLessons) return;

        // if any of them are missing lessons, fetch from server
        if (teacherLessons) teacherId = undefined;
        if (roomLessons) roomId = undefined;

        var response = await server.getAsync<OtherLessons>("OtherLessons", {
            classId: scheduleArrangerConfig.classId,
            teacherId,
            roomId
        });

        if (response) {
            teacher.lessons ?? (teacher.lessons = response.teacher);
            room.lessons ?? (room.lessons = response.room);
            apply(teacher.lessons, room.lessons);
        }
    }
}
const scheduleDataService = new ScheduleArrangerDataService;
export default scheduleDataService;