import MarkModel from "../../../marks/mark-model";

export default interface GiveMarkModel {
    lessonId: number;
    mark?: MarkModel;
    description: string;
    weight?: number;
    studentId?: number;
}