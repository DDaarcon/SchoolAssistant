import ParticipatingStudentModel from "./participating-student-model";

export default interface LessonConductionPanelModel {
    lessonId: number;
    subjectName: string;
    className: string;
    startTimeTk: number;
    duration: number;
    topic?: string;
    students: ParticipatingStudentModel[];
}