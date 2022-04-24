interface TableData {
    [index: string]: string | number;
    id?: number;
}

interface GroupedTableData<TData extends TableData> {
    id: string | number;
    name?: string;
    entries: TData[];
}

type ModificationComponentProps = {
    recordId?: number;
    reloadAsync: () => Promise<void>;
    onMadeAnyChange: () => void;
}

type GroupedModificationComponentProps = ModificationComponentProps & {
    groupId: string | number;
}

type ColumnSetting<TData extends TableData> = {
    header: string;
    prop: keyof TData;
    style?: React.CSSProperties;
}






type TableProps<TData extends TableData> = SharedTableProps<TData, ModificationComponentProps, TData>;
type TableState<TData extends TableData> = SharedTableState<TData, TData> & {
    editedRecordId?: number;
    addingNew: boolean;
}

class Table<TData extends TableData> extends SharedTable<TData, ModificationComponentProps, TData, TableProps<TData>, TableState<TData>> {
    constructor(props) {
        super(props);

        this.state = {
            addingNew: false,
            loading: true
        }
    }

    openOrCloseModification = (id: number) => {
        if (id == this.state.editedRecordId)
            this.setState({ editedRecordId: undefined, addingNew: false });
        else
            this.setState({ editedRecordId: id, addingNew: false });
    }

    openOrCloseAddingNew = () => {
        if (this._madeAnyChange) {
            const confirmation = confirmClosing();
            if (!confirmation) return;
        }

        this._madeAnyChange = false;
        this.setState({ editedRecordId: undefined, addingNew: !this.state.addingNew });
    }

    render() {
        const displayProperties = this.props.columnsSetting.map(x => x.prop);

        return (
            <this.TableFundation
                tbody={
                    <>
                        {this.state.data?.map((data, i) => {
                            const recordId = data.id;
                            return (
                                <this.TableRecordToUse key={i}
                                    recordId={recordId}
                                    recordData={data}
                                    modificationComponent={this.ModificationComponentToUse}
                                    modifying={recordId == this.state?.editedRecordId}
                                    displayProperties={displayProperties}
                                    onOpenEdit={this.openOrCloseModification}
                                    reloadAsync={this.loadAsync}
                                    isEven={i % 2 == 1}
                                />
                            )
                        })}
                        <this.RecordRowsTouse
                            isEven={this.state.data?.length % 2 == 1}
                            openedModification={this.state.addingNew}
                            columnsCount={this.props.columnsSetting.length + 1}
                            dataRow={
                                <td colSpan={this.props.columnsSetting.length + 1}>
                                    <a onClick={this.openOrCloseAddingNew} href="#">
                                        Dodaj
                                    </a>
                                </td>
                            }
                            modificationComponent={
                                <this.ModificationComponentToUse
                                    recordId={undefined}
                                    reloadAsync={this.loadAsync}
                                    onMadeAnyChange={this.onMadeAnyChange}
                                />
                            }
                        />
                    </>
                }
            />
        )
    }
}





type GroupedTableProps<TData extends TableData> = SharedTableProps<TData, GroupedModificationComponentProps, GroupedTableData<TData>>;
type GroupedTableState<TData extends TableData> = SharedTableState<TData, GroupedTableData<TData>> & {
    editedRecord?: {
        groupId: string | number;
        id: number;
    };
    addingNewOfGroup?: string | number;
}
class GroupedTable<TData extends TableData> extends SharedTable<TData, GroupedModificationComponentProps, GroupedTableData<TData>, GroupedTableProps<TData>, GroupedTableState<TData>> {
    constructor(props) {
        super(props);

        this.state = {
            loading: true
        }
    }

    openOrCloseModification = (id: number, groupId?: string | number) => {
        if (id == this.state.editedRecord?.id
            && groupId == this.state.editedRecord?.groupId)
            this.setState({ editedRecord: undefined, addingNewOfGroup: undefined });
        else
            this.setState({ editedRecord: { id, groupId }, addingNewOfGroup: undefined });
    }

    createOpenOrCloseAddingNewHandler = (groupId: string | number) => {
        return () => {
            if (this._madeAnyChange) {
                const confirmation = confirmClosing();
                if (!confirmation) return;
            }

            this._madeAnyChange = false;
            if (groupId == this.state.addingNewOfGroup)
                this.setState({ editedRecord: undefined, addingNewOfGroup: undefined });
            else
                this.setState({ editedRecord: undefined, addingNewOfGroup: groupId });
        }
    }

    render() {
        const displayProperties = this.props.columnsSetting.map(x => x.prop);

        return (
            <this.TableFundation
                tbody={
                    <>
                        {this.state.data?.map((group, i) =>
                            <React.Fragment key={i}>
                                {group.name != undefined
                                    ?
                                    <>
                                        <tr className="group-header-row">
                                            <th colSpan={this.props.columnsSetting.length + 1}>
                                                {group.name}
                                            </th>
                                        </tr>
                                        <tr className="separation-row">
                                            <td colSpan={this.props.columnsSetting.length + 1}></td>
                                        </tr>
                                    </>
                                    : <></>}

                                {group.entries?.map((entry, entryIndex) =>
                                    <this.TableRecordToUse
                                        key={entry.id}
                                        recordId={entry.id}
                                        groupId={group.id}
                                        recordData={entry}
                                        modificationComponent={this.ModificationComponentToUse}
                                        modifying={entry.id == this.state?.editedRecord?.id && group.id == this.state?.editedRecord?.groupId}
                                        displayProperties={displayProperties}
                                        onOpenEdit={this.openOrCloseModification}
                                        reloadAsync={this.loadAsync}
                                        isEven={entryIndex % 2 == 1}
                                    />
                                )}
                                <this.RecordRowsTouse
                                    isEven={group.entries?.length % 2 == 1}
                                    openedModification={this.state.addingNewOfGroup == group.id}
                                    columnsCount={this.props.columnsSetting.length + 1}
                                    dataRow={
                                        <td colSpan={this.props.columnsSetting.length + 1}>
                                            <a onClick={this.createOpenOrCloseAddingNewHandler(group.id)} href="#">
                                                Dodaj
                                            </a>
                                        </td>
                                    }
                                    modificationComponent={
                                        <this.ModificationComponentToUse
                                            recordId={undefined}
                                            reloadAsync={this.loadAsync}
                                            onMadeAnyChange={this.onMadeAnyChange}
                                            groupId={group.id}
                                        />
                                    }
                                />
                            </React.Fragment>
                        )}
                    </>
                }
            />
        );
    }
}






type TableRecordProps<
    TData extends TableData,
    TModificationComponentProps extends ModificationComponentProps | GroupedModificationComponentProps
> = {
    recordId?: number;
    recordData: TData;
    onOpenEdit?: (id: number, groupId?: string | number) => void;
    displayProperties: (keyof TData)[];
    modificationComponent: new (props: TModificationComponentProps) => React.Component<TModificationComponentProps>;
    customRecordRowsComponent?: new (props: RecordRowsProps) => React.Component<RecordRowsProps>;
    modifying: boolean;
    reloadAsync: () => Promise<void>;

    isEven: boolean;
    groupId?: string | number;
}
type TableRecordState = {

}

class TableRecord<
    TData extends TableData,
    TModificationComponentProps extends ModificationComponentProps | GroupedModificationComponentProps
    >
extends React.Component<TableRecordProps<TData, TModificationComponentProps>, TableRecordState>
{
    protected readonly _defaultRecordRowsComponent = RecordRows;

    protected get RecordRowsTouse() { return this.props.customRecordRowsComponent ?? this._defaultRecordRowsComponent; }
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
        const keys = this.props.displayProperties;

        return (
            <this.RecordRowsTouse
                isEven={this.props.isEven}
                openedModification={this.props.modifying}
                columnsCount={this.props.displayProperties.length + 1}
                dataRow={
                    <>
                        {keys.map((key, index) => <td key={index}>{this.props.recordData[key]}</td>)}
                        <td className="dm-edit-btn-cell">
                            <a onClick={this.onClickedEditBtn} href="#">
                                Edytuj
                            </a>
                        </td>
                    </>
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


function confirmClosing() {
    return confirm("Zakończyć edycję? Wprowadzone zmiany zostaną utracone");
}