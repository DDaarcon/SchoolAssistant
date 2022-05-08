import ListEntry from "../../lists/interfaces/list-entry";

export default interface ClassListEntry extends ListEntry {
    name: string;
    specialization: string;
    amountOfStudents: number;
}