import React from "react";
import { confirmCloseMod } from "../help/confirm-close-mod";
import ListEntry from "../interfaces/list-entry";
import { SharedGroupModCompProps } from "../interfaces/shared-group-mod-comp-props";
import ModCompProps from "../interfaces/shared-mod-comp-props";
import { EntryInfoProps } from "./entry-info-component";
import { ListEntryInnerProps } from "./list-entry-inner-component";

export type ListEntryProps<
    TData extends ListEntry,
    TModificationComponentProps extends ModCompProps | SharedGroupModCompProps
    > = {
        recordId?: number;
        recordData: TData;
        onOpenEdit?: (id: number, groupId?: string | number) => void;
        displayProperties: (keyof TData)[];
        modificationComponent: new (props: TModificationComponentProps) => React.Component<TModificationComponentProps>;
        listEntryInnerComponent: new (props: ListEntryInnerProps) => React.Component<ListEntryInnerProps>;
        entryInfoComponent: (props: EntryInfoProps<TData>) => JSX.Element;
        modifying: boolean;
        reloadAsync: () => Promise<void>;

        isEven: boolean;
        groupId?: string | number;
    }
type TableRecordState = {

}

export default class ListEntryComponent<
    TData extends ListEntry,
    TModificationComponentProps extends ModCompProps | SharedGroupModCompProps
    >
    extends React.Component<ListEntryProps<TData, TModificationComponentProps>, TableRecordState>
{
    protected get EntryInfoComponent() { return this.props.entryInfoComponent; }
    protected get ListEntryInnerComponent() { return this.props.listEntryInnerComponent; }
    protected get ModificationComponent() { return this.props.modificationComponent; }

    private _madeAnyChange: boolean = false;

    onClickedEditBtn = () => {
        if (this._madeAnyChange) {
            const confirmation = confirmCloseMod();
            if (!confirmation) return;
        }

        this._madeAnyChange = false;
        this.props.onOpenEdit?.(this.props.recordId, this.props.groupId);
    }

    onMadeAnyChange = () => {
        this._madeAnyChange = true;
    }

    render() {
        return (
            <this.ListEntryInnerComponent
                isEven={this.props.isEven}
                openedModification={this.props.modifying}
                columnsCount={this.props.displayProperties.length + 1}
                dataRow={
                    <this.EntryInfoComponent
                        recordData={this.props.recordData}
                        recordDataKeys={this.props.displayProperties}
                        onClickedEditBtn={this.onClickedEditBtn}
                    />
                }
                modificationComponent={
                    <this.ModificationComponent
                        recordId={this.props.recordId}
                        reloadAsync={this.props.reloadAsync}
                        onMadeAnyChange={this.onMadeAnyChange}
                        //@ts-ignore
                        groupId={this.props.groupId}
                    />
                }
            />
        )
    }
}