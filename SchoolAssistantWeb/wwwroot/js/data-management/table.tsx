﻿interface TableData {
    [index: string]: string | number;
    id?: number;
}

type ModificationComponentProps = {
    recordId?: number;
    reloadAsync: () => Promise<void>;
    onMadeAnyChange: () => void;
}



type ColumnSetting<TData extends TableData> = {
    header: string;
    prop: keyof TData;
    style?: React.CSSProperties;
}


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
        //this.loadAsync();
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
                                <TableRecord<TData> key={i}
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
                        <RowGroup
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






type TableRecordProps<TData extends TableData> = {
    recordId?: number;
    recordData: TData;
    onOpenEdit?: (id: number) => void;
    displayProperties: (keyof TData)[];
    modificationComponent: new (props: ModificationComponentProps) => React.Component<ModificationComponentProps>;
    modifying: boolean;
    reloadAsync: () => Promise<void>;

    isEven: boolean;
}
type TableRecordState = {

}

class TableRecord<TData extends TableData> extends React.Component<TableRecordProps<TData>, TableRecordState> {
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
            <RowGroup
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
                    />
                }
            />
        )
    }
}

type RowGroupProps = {
    isEven: boolean;
    openedModification: boolean;
    columnsCount: number;
    dataRow: JSX.Element;
    modificationComponent: JSX.Element;
}
const RowGroup = (props: RowGroupProps) => {
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