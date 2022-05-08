import ListEntry from "../../lists/interfaces/list-entry";

export default interface StudentListEntry extends ListEntry {
    name: string;
    numberInJournal: number;
}