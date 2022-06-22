import MarkModel from "../../../marks/mark-model";

export default interface GiveMarkModel {
    mark?: MarkModel;
    description: string;
    weight?: number;
    studentId?: number;
}