import ListEntry from "./list-entry";

export default interface CustomRowButton<TEntry extends ListEntry> {
    label: string;
    cellClassName?: string;
    buttonClassName?: string;
    action: (entry: TEntry) => void;
    columnStyle?: React.CSSProperties;
}