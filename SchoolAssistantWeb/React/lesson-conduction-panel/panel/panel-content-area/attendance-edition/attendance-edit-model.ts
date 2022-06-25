import StudentPresenceModel from "./student-presence-model";

export default interface AttendanceEditModel {
    id: number;
    students: StudentPresenceModel[];
}