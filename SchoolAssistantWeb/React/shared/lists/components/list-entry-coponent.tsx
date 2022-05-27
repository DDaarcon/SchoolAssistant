import React from "react";
import confirmCloseMod  from "../help/confirm-close-mod";
import CustomRowButton from "../interfaces/custom-row-button";
import ListEntry from "../interfaces/list-entry";
import { SharedGroupModCompProps } from "../interfaces/shared-group-mod-comp-props";
import ModCompProps from "../interfaces/shared-mod-comp-props";
import EntryInfoComponent from "./entry-info-component";
import { ListEntryInnerProps } from "./list-entry-inner-component";

export type ListEntryProps<
    TEntry extends ListEntry,
    TModificationComponentProps extends ModCompProps | SharedGroupModCompProps | void
    > = {
        recordId?: number;
        recordData: TEntry;
        displayProperties: (keyof TEntry)[];
        onOpenEdit?: (id: number, groupId?: string | number) => void;
        modifying: boolean;
        reloadAsync: () => Promise<void>;

        isEven: boolean;
        groupId?: string | number;

        modificationComponent?: new (props: TModificationComponentProps) => React.Component<TModificationComponentProps>;
        listEntryInnerComponent: new (props: ListEntryInnerProps) => React.Component<ListEntryInnerProps>;
        customButtons?: CustomRowButton<TEntry>[]
    }
type TableRecordState = {

}

export default class ListEntryComponent<
    TEntry extends ListEntry,
    TModificationComponentProps extends ModCompProps | SharedGroupModCompProps
    >
    extends React.Component<ListEntryProps<TEntry, TModificationComponentProps>, TableRecordState>
{
    protected get ListEntryInnerComponent() { return this.props.listEntryInnerComponent; }
    protected get ModificationComponent() { return this.props.modificationComponent; }

    private _madeAnyChange: boolean = false;

    onClickedEditBtn = async () => {
        if (this._madeAnyChange) {
            const confirmation = await confirmCloseMod();
            if (!confirmation) return;
        }

        this._madeAnyChange = false;
        this.props.onOpenEdit?.(this.props.recordId, this.props.groupId);
    }

    onMadeAnyChange = () => {
        this._madeAnyChange = true;
    }

    render() {
        const buttons = this.props.customButtons?.map(x => x) ?? [];
        buttons.push({
            label: "Edytuj",
            buttonClassName: "dm-entry-info-row-button-cell",
            cellClassName: "dm-entry-info-row-button",
            action: this.onClickedEditBtn
        });

        return (
            <this.ListEntryInnerComponent
                isEven={this.props.isEven}
                openedModification={this.props.modifying}
                columnsCount={(this.props.displayProperties?.length ?? 0) + (this.props.customButtons?.length ?? 0) + 1}
                entryInfoComponent={
                    <EntryInfoComponent
                        recordData={this.props.recordData}
                        recordDataKeys={this.props.displayProperties}
                        onClickedEditBtn={this.onClickedEditBtn}
                        buttons={buttons}
                    />
                }
                modificationComponent={this.ModificationComponent != null ?
                    <this.ModificationComponent
                        recordId={this.props.recordId}
                        reloadAsync={this.props.reloadAsync}
                        onMadeAnyChange={this.onMadeAnyChange}
                        //@ts-ignore
                        groupId={this.props.groupId}
                    /> : undefined
                }
            />
        )
    }
}