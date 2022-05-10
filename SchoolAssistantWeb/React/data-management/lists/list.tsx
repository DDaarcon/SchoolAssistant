import React from "react";
import SharedListComponent, { SharedListProps, SharedListState } from "./components/shared-list-component";
import confirmCloseMod from "./help/confirm-close-mod";
import ListEntry from "./interfaces/list-entry";
import ModCompProps from "./interfaces/shared-mod-comp-props";

type ListProps<TData extends ListEntry> = SharedListProps<TData, ModCompProps, TData>;
type ListState<TData extends ListEntry> = SharedListState<TData, TData> & {
    editedRecordId?: number;
    addingNew: boolean;
}

export default class List<TData extends ListEntry> extends SharedListComponent<TData, ModCompProps, TData, ListProps<TData>, ListState<TData>> {

    protected closeAllModCompState: <TKey extends keyof SharedListState<TData, TData> | "editedRecordId" | "addingNew">() => Pick<ListState<TData>, TKey> =
        //@ts-ignore
        () => ({ editedRecordId: undefined, addingNew: false });
    
    
    constructor(props) {
        super(props);

        this.state = {
            addingNew: false,
            loading: true
        }
    }

    openOrCloseModification = (id: number) => {
        if (id == this.state.editedRecordId)
            this.setState(this.closeAllModCompState);
        else
            this.setState({ editedRecordId: id, addingNew: false });
    }

    openOrCloseAddingNew = async () => {
        if (this._madeAnyChange) {
            const confirmation = await confirmCloseMod();
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
                                    customButtons={this.props.customButtons}
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
                            columnsCount={this.columnsCount}
                            entryInfoComponent={
                                <td className={"dm-ei-row-cell"} colSpan={this.columnsCount}>
                                    <button className={"dm-ei-row-button"}
                                        onClick={this.openOrCloseAddingNew}
                                    >
                                        Dodaj
                                    </button>
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