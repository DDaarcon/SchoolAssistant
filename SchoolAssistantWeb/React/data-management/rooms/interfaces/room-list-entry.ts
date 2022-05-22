import ListEntry from "../../../shared/lists/interfaces/list-entry";

export default interface RoomListEntry extends ListEntry {
    name: string;
    floor: number;
}