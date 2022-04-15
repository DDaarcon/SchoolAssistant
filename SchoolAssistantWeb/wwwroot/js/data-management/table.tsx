interface TableData {
    [index: string]: string | number;
    id?: number;
}

type ModificationComponentProps = {
    recordId?: number;
    reloadAsync: () => Promise<void>;
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
    constructor(props) {
        super(props);

        this.state = {
            addingNew: false,
            loading: true
        }
        this.loadAsync();
    }

    openModificationFor = (id: number) => {
        this.setState({ editedRecordId: id, addingNew: false });
    }

    openAddingNew = () => {
        this.setState({ editedRecordId: undefined, addingNew: true });
    }

    loadAsync = async () => {
        this.setState({ loading: true });
        const newData = await this.props.loadDataAsync();
        this.setState({
            data: newData,
            loading: false
        });
    }

    renderColumnSetting = (setting: ColumnSetting<TData>) => {
        if (setting.style)
            return (
                <col style={setting.style} />
            )
        return <col />
    }

    render() {
        const displayProperties = this.props.columnsSetting.map(x => x.prop);
        const ModificationComponent = this.props.modificationComponent;

        let addingNewRow;
        if (this.state.addingNew)
            addingNewRow =
                <td colSpan={this.props.columnsSetting.length + 1} className="dm-modification-component-container">
                    <ModificationComponent recordId={undefined} reloadAsync={this.loadAsync} />
                </td>;
        else
            addingNewRow = undefined;

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
                                    onOpenEdit={this.openModificationFor}
                                    reloadAsync={this.loadAsync}
                                />
                            )
                        })}
                        <tr>
                            <td colSpan={this.props.columnsSetting.length + 1}>
                                <a onClick={this.openAddingNew} href="#">
                                    Dodaj
                                </a>
                            </td>
                        </tr>
                        <tr>
                            {addingNewRow}
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

    editThis = () => {
        this.props.onOpenEdit?.(this.props.recordId);
    }

    render() {
        const keys = this.props.displayProperties;
        const ModificationComponent = this.props.modificationComponent;

        let modificationRow;
        if (this.props.modifying)
            modificationRow =
                <td colSpan={keys.length + 1} className="dm-modification-component-container">
                    <ModificationComponent recordId={this.props.recordId} reloadAsync={this.props.reloadAsync} />
                </td>;
        else
            modificationRow = undefined;

        return (
            <>
                <tr>
                    {keys.map((key, index) => <td key={index}>{this.props.recordData[key]}</td>)}
                    <td className="dm-edit-btn-cell">
                        <a onClick={this.editThis} href="#">
                            Edytuj
                        </a>
                    </td>
                </tr>
                <tr>
                    {modificationRow}
                </tr>
            </>
        )
    }
}