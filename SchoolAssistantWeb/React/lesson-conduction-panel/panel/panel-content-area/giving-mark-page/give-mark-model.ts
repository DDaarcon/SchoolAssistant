import MarkModel from "../../../marks/mark-model";

export default interface GiveMarkModel {
    id: number;
    mark?: MarkModel;
    description: string;
    weight?: number;
    studentId?: number;
}