import React from "react";
import SharedTable, { SharedListProps, SharedListState } from "./components/shared-list-component";
import { confirmCloseMod } from "./help/confirm-close-mod";
import ListEntry from "./interfaces/list-entry";
import ModCompProps from "./interfaces/shared-mod-comp-props";

type ListProps<TData extends ListEntry> = SharedListProps<TData, ModCompProps, TData>;
type ListState<TData extends ListEntry> = SharedListState<TData, TData> & {
    editedRecordId?: number;
    addingNew: boolean;
}

export default class List<TData extends ListEntry> extends SharedTable<TData, ModCompProps, TData, ListProps<TData>, ListState<TData>> {
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
            const confirmation = confirmCloseMod();
            if (!confirmation) return;
        }

        this._madeAnyChange = false;
        this.setState({ editedRecordId: undefined, addingNew: !this.state.addingNew });
    }

    render() {
        const displayProperties = this.props.columnsSetting.map(x => x.prop);

        return (
            <this.ListFundation
                tbody={
                    <>
                        {this.state.data?.map((data, i) => {
                            const recordId = data.id;
                            return (
                                <this.ListEntryComponent key={i}
                                    recordId={recordId}
                                    recordData={data}
                                    modificationComponent={this.ModificationComponent}
                                    listEntryInnerComponent={this.ListEntryInnerComponent}
                                    entryInfoComponent={this.EntryInfoComponent}
                                    modifying={recordId == this.state?.editedRecordId}
                                    displayProperties={displayProperties}
                                    onOpenEdit={this.openOrCloseModification}
                                    reloadAsync={this.loadAsync}
                                    isEven={i % 2 == 1}
                                />
                            )
                        })}
                        <this.ListEntryInnerComponent
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
                                <this.ModificationComponent
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