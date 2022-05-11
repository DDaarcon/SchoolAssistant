import { Time } from "../../../interfaces/shared";

export default interface LessonEditModel {
    id: number;
    time: Time;
    customDuration?: number;
    subjectId: number;
    lecturerId: number;
    roomId: number;
}