import React from "react";
import SharedTable, { confirmClosing, GroupedModificationComponentProps, GroupedTableData, ModificationComponentProps, SharedTableProps, SharedTableState, TableData } from "./shared-table";






type TableProps<TData extends TableData> = SharedTableProps<TData, ModificationComponentProps, TData>;
type TableState<TData extends TableData> = SharedTableState<TData, TData> & {
    editedRecordId?: number;
    addingNew: boolean;
}

export class Table<TData extends TableData> extends SharedTable<TData, ModificationComponentProps, TData, TableProps<TData>, TableState<TData>> {
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
                                    recordRowsComponent={this.RecordRowsToUse}
                                    informationRowComponent={this.InformationRowToUse}
                                    modifying={recordId == this.state?.editedRecordId}
                                    displayProperties={displayProperties}
                                    onOpenEdit={this.openOrCloseModification}
                                    reloadAsync={this.loadAsync}
                                    isEven={i % 2 == 1}
                                />
                            )
                        })}
                        <this.RecordRowsToUse
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
export class GroupedTable<TData extends TableData> extends SharedTable<TData, GroupedModificationComponentProps, GroupedTableData<TData>, GroupedTableProps<TData>, GroupedTableState<TData>> {
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
                                        recordRowsComponent={this.RecordRowsToUse}
                                        informationRowComponent={this.InformationRowToUse}
                                        modifying={entry.id == this.state?.editedRecord?.id && group.id == this.state?.editedRecord?.groupId}
                                        displayProperties={displayProperties}
                                        onOpenEdit={this.openOrCloseModification}
                                        reloadAsync={this.loadAsync}
                                        isEven={entryIndex % 2 == 1}
                                    />
                                )}
                                <this.RecordRowsToUse
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