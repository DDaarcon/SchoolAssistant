interface TableData {
    [index: string]: string | number;
    id?: number;
}

type ModificationComponentProps = {
    recordId?: number;
    reloadAsync: () => Promise<void>;
    onMadeAnyChange: () => void;
}



type ColumnSetting<TData extends TableData> = {
    prop: keyof TData;
    style?: React.CSSProperties;
}


type TableProps<TData extends TableData> = {
    headers: string[];
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

    renderModificationComponent = () => {
        const ModificationComponent = this.props.modificationComponent;
        if (this.state.addingNew)
            return (
                <td colSpan={this.props.columnsSetting.length + 1} className="dm-modification-component-container">
                    <ModificationComponent
                        recordId={undefined}
                        reloadAsync={this.loadAsync}
                        onMadeAnyChange={this.onMadeAnyChange}
                    />
                </td>
            )
        else
            return (<></>);
    }

    render() {
        const displayProperties = this.props.columnsSetting.map(x => x.prop);

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
                        <tr>{this.props.headers.map((h, i) => <th key={i}>{h}</th>)}</tr>
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
                                />
                            )
                        })}
                        <tr>
                            <td colSpan={this.props.columnsSetting.length + 1}>
                                <a onClick={this.openOrCloseAddingNew} href="#">
                                    Dodaj
                                </a>
                            </td>
                        </tr>
                        <tr>
                            {this.renderModificationComponent()}
                        </tr>
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

    renderModificationComponent = () => {
        const ModificationComponent = this.props.modificationComponent;

        if (this.props.modifying)
            return (
                <td colSpan={this.props.displayProperties.length + 1} className="dm-modification-component-container">
                    <ModificationComponent
                        recordId={this.props.recordId}
                        reloadAsync={this.props.reloadAsync}
                        onMadeAnyChange={this.onMadeAnyChange}
                    />
                </td>
            )
        else
            return (<></>);
    }

    render() {
        const keys = this.props.displayProperties;

        return (
            <>
                <tr>
                    {keys.map((key, index) => <td key={index}>{this.props.recordData[key]}</td>)}
                    <td className="dm-edit-btn-cell">
                        <a onClick={this.onClickedEditBtn} href="#">
                            Edytuj
                        </a>
                    </td>
                </tr>
                <tr>
                    {this.renderModificationComponent()}
                </tr>
            </>
        )
    }
}


function confirmClosing() {
    return confirm("Zakończyć edycję? Wprowadzone zmiany zostaną utracone");
}