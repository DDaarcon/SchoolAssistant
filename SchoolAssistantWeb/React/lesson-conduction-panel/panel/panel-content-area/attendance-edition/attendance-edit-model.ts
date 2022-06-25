import StudentPresenceEditModel from "./student-presence-edit-model";

export default interface AttendanceEditModel {
    id: number;
    students: StudentPresenceEditModel[];
}