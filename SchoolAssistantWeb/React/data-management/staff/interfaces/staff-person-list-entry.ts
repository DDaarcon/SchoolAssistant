import ListEntry from "../../../shared/lists/interfaces/list-entry";

export default interface StaffPersonListEntry extends ListEntry {
    name: string;
    specialization?: string;
}