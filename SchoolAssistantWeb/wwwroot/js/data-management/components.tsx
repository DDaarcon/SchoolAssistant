interface TableData {
    [index: string]: string | number;
    id: number;
}

type ModificationComponentProps = {
    recordId: number;
}

type TableProps<TData extends TableData> = {
    tableRecordComponent: new (props: TableRecordProps<TData>) => TableRecord<TData>;
    headers: string[];
    data: TData[];
    modificationComponent: new (props: ModificationComponentProps) => React.Component<ModificationComponentProps>;
    displayProperties: (keyof TData)[];
}
type TableState = {

}

class Table<TData extends TableData> extends React.Component<TableProps<TData>, TableState> {
    private editedRecordId?: number;

    openModificationFor = (id: number) => {
        this.editedRecordId = id;
    }

    render() {
        const TableRecordComponent = this.props.tableRecordComponent;
        return (
            <table className="dm-table">
                <thead>
                    <tr>{this.props.headers.map((h, i) => <th key={i}>{h}</th>)}</tr>
                </thead>
                <tbody>
                    {this.props.data.map((data, i) => {
                        const recordId = data.id;
                        return (
                            <TableRecordComponent key={i}
                                recordId={recordId}
                                recordData={data}
                                modificationComponent={this.props.modificationComponent}
                                modifying={recordId == this.editedRecordId}
                                displayProperties={this.props.displayProperties}
                                onOpenEdit={this.openModificationFor}
                            />
                        )
                    })}
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
                <td colSpan={keys.length}>
                    <ModificationComponent recordId={this.props.recordId} />
                </td>;
        else
            modificationRow = undefined;

        return (
            <>
                <tr>
                    {keys.map((key, index) => <td key={index}>{this.props.recordData[key]}</td>)}
                    <td>
                        <a onClick={this.editThis}>
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