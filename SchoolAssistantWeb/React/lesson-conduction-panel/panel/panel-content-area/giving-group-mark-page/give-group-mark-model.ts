import MarkModel from "../../../marks/mark-model";

export default interface GiveGroupMarkModel {
    lessonId: number;
    description: string;
    weight?: number;
    marks: { [studentId: number]: MarkModel | undefined }
}