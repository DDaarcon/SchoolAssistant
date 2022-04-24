interface TableData {
    [index: string]: string | number;
    id?: number;
}

interface GroupedTableData<TData extends TableData> {
    id: string;
    name: string;
    entries: TData[];
}

type ModificationComponentProps = {
    recordId?: number;
    reloadAsync: () => Promise<void>;
    onMadeAnyChange: () => void;
}

type GroupedModificationComponentProps = ModificationComponentProps & {
    groupId: string;
}

type ColumnSetting<TData extends TableData> = {
    header: string;
    prop: keyof TData;
    style?: React.CSSProperties;
}


// TODO: Extract common part from Table and GroupedTable


type TableProps<TData extends TableData> = {
    modificationComponent: new (props: ModificationComponentProps) => React.Component<ModificationComponentProps>;
    columnsSetting: ColumnSetting<TData>[];
    loadDataAsync: () => Promise<TData[]>;
}
type TableState<TData extends TableData> = {
    editedRecordId?: number;
    addingNew: boolean;
    data?: TData[];
    loading: boolean;
}

class Table<TData extends TableData> extends React.Component<TableProps<TData>, TableState<TData>> {
    private madeAnyChange: boolean = false;

    constructor(props) {
        super(props);

        this.state = {
            addingNew: false,
            loading: true
        }
    }

    async componentDidMount() {
        await this.loadAsync();
    }

    openOrCloseModification = (id: number) => {
        if (id == this.state.editedRecordId)
            this.setState({ editedRecordId: undefined, addingNew: false });
        else
            this.setState({ editedRecordId: id, addingNew: false });
    }

    openOrCloseAddingNew = () => {
        if (this.madeAnyChange) {
            const confirmation = confirmClosing();
            if (!confirmation) return;
        }

        this.madeAnyChange = false;
        this.setState({ editedRecordId: undefined, addingNew: !this.state.addingNew });
    }

    loadAsync = async () => {
        this.setState({ loading: true });
        const newData = await this.props.loadDataAsync();
        this.setState({
            data: newData,
            loading: false
        });
    }

    onMadeAnyChange = () => {
        this.madeAnyChange = true;
    }

    renderColumnSetting = (setting: ColumnSetting<TData>, index: number) => {
        if (setting.style)
            return (
                <col key={index} style={setting.style} />
            )
        return <col key={index} />
    }

    render() {
        const displayProperties = this.props.columnsSetting.map(x => x.prop);
        const ModificationComponent = this.props.modificationComponent;

        return (
            <>
                <Loader
                    enable={this.state?.loading}
                    size={LoaderSize.Medium}
                    type={LoaderType.Absolute}
                />
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
                        {this.state.data?.map((data, i) => {
                            const recordId = data.id;
                            return (
                                <TableRecord<TData, ModificationComponentProps> key={i}
                                    recordId={recordId}
                                    recordData={data}
                                    modificationComponent={this.props.modificationComponent}
                                    modifying={recordId == this.state?.editedRecordId}
                                    displayProperties={displayProperties}
                                    onOpenEdit={this.openOrCloseModification}
                                    reloadAsync={this.loadAsync}
                                    isEven={i % 2 == 1}
                                />
                            )
                        })}
                        <RecordRows
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
                                <ModificationComponent
                                    recordId={undefined}
                                    reloadAsync={this.loadAsync}
                                    onMadeAnyChange={this.onMadeAnyChange}
                                />
                            }
                        />
                    </tbody>
                </table>
            </>
        )
    }
}





type GroupedTableProps<TData extends TableData> = {
    modificationComponent: new (props: GroupedModificationComponentProps) => React.Component<GroupedModificationComponentProps>;
    columnsSetting: ColumnSetting<TData>[];
    loadDataAsync: () => Promise<GroupedTableData<TData>[]>;
}
type GroupedTableState<TData extends TableData> = {
    editedRecordId?: number;
    addingNewOfGroup?: string;
    data?: GroupedTableData<TData>[];
    loading: boolean;
}
class GroupedTable<TData extends TableData> extends React.Component<GroupedTableProps<TData>, GroupedTableState<TData>> {
    private madeAnyChange: boolean = false;

    constructor(props) {
        super(props);

        this.state = {
            loading: true
        }
    }

    async componentDidMount() {
        await this.loadAsync();
    }

    openOrCloseModification = (id: number) => {
        if (id == this.state.editedRecordId)
            this.setState({ editedRecordId: undefined, addingNewOfGroup: undefined });
        else
            this.setState({ editedRecordId: id, addingNewOfGroup: undefined });
    }

    createOpenOrCloseAddingNewHandler = (groupId: string) => {
        return () => {
            if (this.madeAnyChange) {
                const confirmation = confirmClosing();
                if (!confirmation) return;
            }

            this.madeAnyChange = false;
            if (groupId == this.state.addingNewOfGroup)
                this.setState({ editedRecordId: undefined, addingNewOfGroup: undefined });
            else
                this.setState({ editedRecordId: undefined, addingNewOfGroup: groupId });
        }
    }

    loadAsync = async () => {
        this.setState({ loading: true });
        const newData = await this.props.loadDataAsync();
        this.setState({
            data: newData,
            loading: false
        });
    }

    onMadeAnyChange = () => {
        this.madeAnyChange = true;
    }

    renderColumnSetting = (setting: ColumnSetting<TData>, index: number) => {
        if (setting.style)
            return (
                <col key={index} style={setting.style} />
            )
        return <col key={index} />
    }

    render() {
        const displayProperties = this.props.columnsSetting.map(x => x.prop);
        const ModificationComponent = this.props.modificationComponent;

        return (
            <>
                <Loader
                    enable={this.state?.loading}
                    size={LoaderSize.Medium}
                    type={LoaderType.Absolute}
                />
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
                        {this.state.data?.map((group, i) => 
                            <React.Fragment key={i}>
                                <tr className="group-header-row">
                                    <th colSpan={this.props.columnsSetting.length + 1}>
                                        {group.name}
                                    </th>
                                </tr>
                                <tr className="separation-row">
                                    <td colSpan={this.props.columnsSetting.length + 1}></td>
                                </tr>
                                {group.entries?.map((entry, entryIndex) =>
                                    <TableRecord<TData, GroupedModificationComponentProps> key={entry.id}
                                        recordId={entry.id}
                                        recordData={entry}
                                        modificationComponent={this.props.modificationComponent}
                                        modifying={entry.id == this.state?.editedRecordId}
                                        displayProperties={displayProperties}
                                        onOpenEdit={this.openOrCloseModification}
                                        reloadAsync={this.loadAsync}
                                        isEven={entryIndex % 2 == 1}
                                        groupId={group.id}
                                    />
                                )}
                                <RecordRows
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
                                        <ModificationComponent
                                            recordId={undefined}
                                            reloadAsync={this.loadAsync}
                                            onMadeAnyChange={this.onMadeAnyChange}
                                            groupId={group.id}
                                        />
                                    }
                                />
                            </React.Fragment>
                        )}
                    </tbody>
                </table>
            </>
        )
    }
}







type TableRecordProps<
    TData extends TableData,
    TModificationComponentProps extends ModificationComponentProps | GroupedModificationComponentProps
> = {
    recordId?: number;
    recordData: TData;
    onOpenEdit?: (id: number) => void;
    displayProperties: (keyof TData)[];
    modificationComponent: new (props: TModificationComponentProps) => React.Component<TModificationComponentProps>;
    modifying: boolean;
    reloadAsync: () => Promise<void>;

    isEven: boolean;
    groupId?: string;
}
type TableRecordState = {

}

class TableRecord<
    TData extends TableData,
    TModificationComponentProps extends ModificationComponentProps | GroupedModificationComponentProps
> extends React.Component<TableRecordProps<TData, TModificationComponentProps>, TableRecordState> {
    private madeAnyChange: boolean = false;

    onClickedEditBtn = () => {
        if (this.madeAnyChange) {
            const confirmation = confirmClosing();
            if (!confirmation) return;
        }

        this.madeAnyChange = false;
        this.props.onOpenEdit?.(this.props.recordId);
    }

    onMadeAnyChange = () => {
        this.madeAnyChange = true;
    }

    render() {
        const keys = this.props.displayProperties;
        const ModificationComponent = this.props.modificationComponent;

        return (
            <RecordRows
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
                    <ModificationComponent
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
const RecordRows = (props: RecordRowsProps) => {
    const modificationRow = props.openedModification
        ? (
            <td className="dm-modification-component-container"
                colSpan={props.columnsCount}
            >
                {props.modificationComponent}
            </td>
        ) : <></>;

    return (
        <>
            <tr className={
                    (props.isEven ? "even-row" : "") +
                    " data-row" +
                    (props.openedModification ? "" : " single-standing-row")
                }
            >
                {props.dataRow}
            </tr>
            <tr className={props.isEven ? "even-row" : ""}>
                {modificationRow}
            </tr>
            <tr className="separation-row">
                <td colSpan={props.columnsCount}> </td>
            </tr>
        </>
    );
}


function confirmClosing() {
    return confirm("Zakończyć edycję? Wprowadzone zmiany zostaną utracone");
}