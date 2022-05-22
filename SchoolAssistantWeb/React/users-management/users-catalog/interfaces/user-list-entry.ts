import ListEntry from "../../../shared/lists/interfaces/list-entry";

export default interface UserListEntry extends ListEntry {
    firstName: string;
    lastName: string;
    userName: string;
    email: string;
}