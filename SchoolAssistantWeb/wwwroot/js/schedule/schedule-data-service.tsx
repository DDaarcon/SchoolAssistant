class ScheduleArrangerDataService {
    prefabs: ScheduleLessonPrefab[] = [];

    subjects?: ScheduleSubjectEntry[];
    teachers?: ScheduleTeacherEntry[];
    rooms?: ScheduleRoomEntry[];

    addPrefab(prefab: ScheduleLessonPrefab) {
        this.prefabs.push(prefab);

        dispatchEvent(new CustomEvent('newPrefab', {
            detail: prefab
        }));
    }

    getSubjectName = (id: number) => this.subjects.find(x => x.id == id).name;
    getTeacherName = (id: number) => this.teachers.find(x => x.id == id).shortName;
    getRoomName = (id: number) => this.rooms.find(x => x.id == id).name;



    getTeacherAndRoomLessons = async (teacherId: number, roomId: number, apply: (teacher?: ScheduleDayLessons[], room?: ScheduleDayLessons[]) => void) => {
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

        var response = await scheduleServer.getAsync<ScheduleOtherLessons>("OtherLessons", {
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