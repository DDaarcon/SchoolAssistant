import React from "react";
import Loader, { LoaderSize, LoaderType } from "../shared/loader";

export interface TableData {
    [index: string]: string | number;
    id?: number;
}

export type ModificationComponentProps = {
    recordId?: number;
    reloadAsync: () => Promise<void>;
    onMadeAnyChange: () => void;
}

export type GroupedModificationComponentProps = ModificationComponentProps & {
    groupId: string | number;
}

export interface GroupedTableData<TData extends TableData> {
    id: string | number;
    name?: string;
    entries: TData[];
}

export type ColumnSetting<TData extends TableData> = {
    header: string;
    prop: keyof TData;
    style?: React.CSSProperties;
}


export type SharedTableProps<
    TData extends TableData,
    TModificationComponentProps extends ModificationComponentProps | GroupedModificationComponentProps,
    TStoredData extends TData | GroupedTableData<TData>
    > = {
        modificationComponent: new (props: TModificationComponentProps) => React.Component<TModificationComponentProps>;
        customTableRecordComponent?: new (props: TableRecordProps<TData, TModificationComponentProps>) => React.Component<TableRecordProps<TData, TModificationComponentProps>>;
        customRecordRowsComponent?: new (props: RecordRowsProps) => React.Component<RecordRowsProps>;
        customInformationRowComponent?: (props: InformationRowProps<TData>) => JSX.Element;
        columnsSetting: ColumnSetting<TData>[];
        loadDataAsync: () => Promise<TStoredData[]>;
    }
export type SharedTableState<
    TData extends TableData,
    TStoredData extends TData | GroupedTableData<TData>
    > = {
        data?: TStoredData[];
        loading: boolean;
    }

export default abstract class SharedTable<
    TData extends TableData,
    TModificationComponentProps extends ModificationComponentProps | GroupedModificationComponentProps,
    TStoredData extends TData | GroupedTableData<TData>,
    TProps extends SharedTableProps<TData, TModificationComponentProps, TStoredData>,
    TState extends SharedTableState<TData, TStoredData>
    >
    extends React.Component<TProps, TState> {

    protected _madeAnyChange: boolean = false;

    protected get TableRecordToUse() { return this.props.customTableRecordComponent ?? TableRecord; }
    protected get RecordRowsToUse() { return this.props.customRecordRowsComponent ?? RecordRows; }
    protected get InformationRowToUse() { return this.props.customInformationRowComponent ?? InformationRow; }
    protected get ModificationComponentToUse() { return this.props.modificationComponent; }



    async componentDidMount() {
        await this.loadAsync();
    }

    onMadeAnyChange = () => {
        this._madeAnyChange = true;
    }

    loadAsync = async () => {
        this.setState({ loading: true });
        const newData = await this.props.loadDataAsync();
        this.setState({
            data: newData,
            loading: false
        });
    }

    renderColumnSetting = (setting: ColumnSetting<TData>, index: number) => {
        if (setting.style)
            return (
                <col key={index} style={setting.style} />
            )
        return <col key={index} />
    }

    LoaderComponent = () =>
        <Loader
            enable={this.state?.loading}
            size={LoaderSize.Medium}
            type={LoaderType.Absolute}
        />;


    TableFundation = (props: {
        tbody: JSX.Element;
    }) =>
        <>
            <this.LoaderComponent />
            <table className="dm-table">
                <colgroup>
                    {this.props.columnsSetting.map(this.renderColumnSetting)}
                </colgroup>
                <thead>
                    <tr>
                        {this.props.columnsSetting.map((setting, i) => <th key={i}>{setting.header}</th>)}
                    </tr>
                </thead>
                <tbody>
                    {props.tbody}
                </tbody>
            </table>
        </>;

    public abstract render(): JSX.Element;
}






export type TableRecordProps<
    TData extends TableData,
    TModificationComponentProps extends ModificationComponentProps | GroupedModificationComponentProps
    > = {
        recordId?: number;
        recordData: TData;
        onOpenEdit?: (id: number, groupId?: string | number) => void;
        displayProperties: (keyof TData)[];
        modificationComponent: new (props: TModificationComponentProps) => React.Component<TModificationComponentProps>;
        recordRowsComponent: new (props: RecordRowsProps) => React.Component<RecordRowsProps>;
        informationRowComponent: (props: InformationRowProps<TData>) => JSX.Element;
        modifying: boolean;
        reloadAsync: () => Promise<void>;

        isEven: boolean;
        groupId?: string | number;
    }
type TableRecordState = {

}

export class TableRecord<
    TData extends TableData,
    TModificationComponentProps extends ModificationComponentProps | GroupedModificationComponentProps
    >
    extends React.Component<TableRecordProps<TData, TModificationComponentProps>, TableRecordState>
{
    protected get InformationRowToUse() { return this.props.informationRowComponent; }
    protected get RecordRowsToUse() { return this.props.recordRowsComponent; }
    protected get ModificationComponentToUse() { return this.props.modificationComponent; }

    private _madeAnyChange: boolean = false;

    onClickedEditBtn = () => {
        if (this._madeAnyChange) {
            const confirmation = confirmClosing();
            if (!confirmation) return;
        }

        this._madeAnyChange = false;
        this.props.onOpenEdit?.(this.props.recordId, this.props.groupId);
    }

    onMadeAnyChange = () => {
        this._madeAnyChange = true;
    }

    render() {
        return (
            <this.RecordRowsToUse
                isEven={this.props.isEven}
                openedModification={this.props.modifying}
                columnsCount={this.props.displayProperties.length + 1}
                dataRow={
                    <this.InformationRowToUse
                        recordData={this.props.recordData}
                        recordDataKeys={this.props.displayProperties}
                        onClickedEditBtn={this.onClickedEditBtn}
                    />
                }
                modificationComponent={
                    <this.ModificationComponentToUse
                        recordId={this.props.recordId}
                        reloadAsync={this.props.reloadAsync}
                        onMadeAnyChange={this.onMadeAnyChange}
                        //@ts-ignore
                        groupId={this.props.groupId}
                    />
                }
            />
        )
    }
}





export type InformationRowProps<TData extends TableData> = {
    recordDataKeys: (keyof TData)[];
    recordData: TData;
    onClickedEditBtn: React.MouseEventHandler<HTMLAnchorElement>;
}
const InformationRow = <TData extends TableData>(props: InformationRowProps<TData>) => {
    return (
        <>
            {props.recordDataKeys.map((key, index) => <td key={index}>{props.recordData[key]}</td>)}
            <td className="dm-edit-btn-cell">
                <a onClick={props.onClickedEditBtn} href="#">
                    Edytuj
                </a>
            </td>
        </>
    )
}





type RecordRowsProps = {
    isEven: boolean;
    openedModification: boolean;
    columnsCount: number;
    dataRow: JSX.Element;
    modificationComponent: JSX.Element;
}
class RecordRows extends React.Component<RecordRowsProps> {
    render() {

        const modificationRow = this.props.openedModification
            ? (
                <td className="dm-modification-component-container"
                    colSpan={this.props.columnsCount}
                >
                    {this.props.modificationComponent}
                </td>
            ) : <></>;

        return (
            <>
                <tr className={
                    (this.props.isEven ? "even-row" : "") +
                    " data-row" +
                    (this.props.openedModification ? "" : " single-standing-row")
                }>
                    {this.props.dataRow}
                </tr>
                <tr className={this.props.isEven ? "even-row" : ""}>
                    {modificationRow}
                </tr>
                <tr className="separation-row">
                    <td colSpan={this.props.columnsCount}> </td>
                </tr>
            </>
        );
    }
}


export function confirmClosing() {
    return confirm("Zakończyć edycję? Wprowadzone zmiany zostaną utracone");
}