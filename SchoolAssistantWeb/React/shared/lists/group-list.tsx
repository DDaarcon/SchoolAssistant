import React from "react";
import SharedListComponent, { SharedListProps, SharedListState } from "./components/shared-list-component";
import confirmCloseMod from "./help/confirm-close-mod";
import GroupListEntry from "./interfaces/group-list-entry";
import ListEntry from "./interfaces/list-entry";
import { SharedGroupModCompProps } from "./interfaces/shared-group-mod-comp-props";

type GroupListProps<TEntry extends ListEntry> = SharedListProps<TEntry, SharedGroupModCompProps, GroupListEntry<TEntry>>;
type GroupListState<TData extends ListEntry> = SharedListState<TData, GroupListEntry<TData>> & {
    editedRecord?: {
        groupId: string | number;
        id: number;
    };
    addingNewOfGroup?: string | number;
}
export default class GroupList<TEntry extends ListEntry> extends SharedListComponent<TEntry, SharedGroupModCompProps, GroupListEntry<TEntry>, GroupListProps<TEntry>, GroupListState<TEntry>> {

    protected closeAllModCompState: <TKey extends keyof SharedListState<TEntry, GroupListEntry<TEntry>> | "editedRecord" | "addingNewOfGroup">() => Pick<GroupListState<TEntry>, TKey> =
        //@ts-ignore
        () => ({ editedRecord: undefined, addingNewOfGroup: undefined });
    

    constructor(props) {
        super(props);

        this.state = {
            loading: true
        }
    }

    openOrCloseModification = (id: number, groupId?: string | number) => {
        if (id == this.state.editedRecord?.id
            && groupId == this.state.editedRecord?.groupId)
            this.setState(this.closeAllModCompState);
        else
            this.setState({ editedRecord: { id, groupId }, addingNewOfGroup: undefined });
    }

    createOpenOrCloseAddingNewHandler = (groupId: string | number) => {
        return async () => {
            if (this._madeAnyChange) {
                const confirmation = await confirmCloseMod();
                if (!confirmation) return;
            }

            this._madeAnyChange = false;
            if (groupId == this.state.addingNewOfGroup)
                this.setState(this.closeAllModCompState);
            else
                this.setState({ editedRecord: undefined, addingNewOfGroup: groupId });
        }
    }

    render() {
        const displayProperties = this.props.columnsSetting.map(x => x.prop);

        return (
            <this.ListFundation
                tbody={
                    <>
                        {this.state.data?.map((group) =>
                            <React.Fragment key={group.id}>
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
                                    <this.ListEntryComponent
                                        key={`${entry.id}${entryIndex}`}
                                        recordId={entry.id}
                                        groupId={group.id}
                                        recordData={entry}
                                        modificationComponent={this.ModificationComponent}
                                        listEntryInnerComponent={this.ListEntryInnerComponent}
                                        modifying={entry.id == this.state?.editedRecord?.id && group.id == this.state?.editedRecord?.groupId}
                                        displayProperties={displayProperties}
                                        onOpenEdit={this.openOrCloseModification}
                                        reloadAsync={this.loadAsync}
                                        isEven={entryIndex % 2 == 1}
                                        customButtons={this.props.customButtons}
                                    />
                                )}
                                <this.ListEntryInnerComponent
                                    isEven={group.entries?.length % 2 == 1}
                                    openedModification={this.state.addingNewOfGroup == group.id}
                                    columnsCount={this.columnsCount}
                                    entryInfoComponent={
                                        <td className={"dm-ei-row-cell"} colSpan={this.columnsCount}>
                                            <button className={"dm-ei-row-button"}
                                                onClick={this.createOpenOrCloseAddingNewHandler(group.id)}
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