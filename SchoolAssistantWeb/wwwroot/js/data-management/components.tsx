interface TableData {
    [index: string]: string | number;
    id: number;
}

type ModificationComponentProps = {
    recordId?: number;
}

type TableProps<TData extends TableData> = {
    headers: string[];
    data: TData[];
    modificationComponent: new (props: ModificationComponentProps) => React.Component<ModificationComponentProps>;
    displayProperties: (keyof TData)[];
}
type TableState = {
    editedRecordId?: number;
    addingNew: boolean;
}

class Table<TData extends TableData> extends React.Component<TableProps<TData>, TableState> {
    state: TableState = {
        addingNew: false
    }

    openModificationFor = (id: number) => {
        this.setState({ editedRecordId: id, addingNew: false });
    }

    openAddingNew = () => {
        this.setState({ editedRecordId: undefined, addingNew: true });
    }

    render() {
        const ModificationComponent = this.props.modificationComponent;

        let addingNewRow;
        if (this.state.addingNew)
            addingNewRow =
                <td colSpan={this.props.displayProperties.length + 1}>
                    <ModificationComponent recordId={undefined} />
                </td>;
        else
            addingNewRow = undefined;

        return (
            <table className="dm-table">
                <thead>
                    <tr>{this.props.headers.map((h, i) => <th key={i}>{h}</th>)}</tr>
                </thead>
                <tbody>
                    {this.props.data.map((data, i) => {
                        const recordId = data.id;
                        return (
                            <TableRecord<TData> key={i}
                                recordId={recordId}
                                recordData={data}
                                modificationComponent={this.props.modificationComponent}
                                modifying={recordId == this.state?.editedRecordId}
                                displayProperties={this.props.displayProperties}
                                onOpenEdit={this.openModificationFor}
                            />
                        )
                    })}
                    <tr>
                        <td colSpan={this.props.displayProperties.length + 1}>
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
        )
    }
}

type TableRecordProps<TData extends TableData> = {
    recordId: number;
    recordData: TData;
    onOpenEdit?: (id: number) => void;
    displayProperties: (keyof TData)[];
    modificationComponent: new (props: ModificationComponentProps) => React.Component<ModificationComponentProps>;
    modifying: boolean;
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
                <td colSpan={keys.length + 1}>
                    <ModificationComponent recordId={this.props.recordId} />
                </td>;
        else
            modificationRow = undefined;

        return (
            <>
                <tr>
                    {keys.map((key, index) => <td key={index}>{this.props.recordData[key]}</td>)}
                    <td>
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