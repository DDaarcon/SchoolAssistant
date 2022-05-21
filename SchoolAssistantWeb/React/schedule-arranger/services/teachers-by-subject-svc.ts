import ScheduleTeacherEntry from "../interfaces/page-model-to-react/schedule-teacher-entry";
import TeacherOptionEntry from "../interfaces/teacher-option-entry";

export default class TeachersBySubjectSvc {

    constructor(
        private _teachers: ScheduleTeacherEntry[]
    ) { }

    getFor(subjectId: number): TeacherOptionEntry[] {
        return [
            ...this._teachers
                .filter(x => x.mainSubjectIds.includes(subjectId))
                .map(x => ({ id: x.id, name: x.name, isMainTeacher: true })),
            ...this._teachers
                .filter(x => !x.mainSubjectIds.includes(subjectId) && x.additionalSubjectIds.includes(subjectId))
                .map(x => ({ id: x.id, name: x.name, isMainTeacher: false }))
        ]
    }
}