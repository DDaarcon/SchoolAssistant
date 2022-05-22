import ListEntry from "./list-entry";

export default interface GroupListEntry<TData extends ListEntry> {
    id: string | number;
    name?: string;
    entries: TData[];
}