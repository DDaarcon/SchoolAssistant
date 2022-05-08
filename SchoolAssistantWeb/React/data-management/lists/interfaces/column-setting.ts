import ListEntry from "./list-entry";

type ColumnSetting<TData extends ListEntry> = {
    header: string;
    prop: keyof TData;
    style?: React.CSSProperties;
}
export default ColumnSetting;