import React from "react";
import CustomRowButton from "../interfaces/custom-row-button";
import ListEntry from "../interfaces/list-entry";

export type EntryInfoProps<TEntry extends ListEntry> = {
    recordDataKeys: (keyof TEntry)[];
    recordData: TEntry;
    onClickedEditBtn: (entry: TEntry) => void;
    buttons?: CustomRowButton<TEntry>[];
}
const EntryInfoCopmponent = <TData extends ListEntry>(props: EntryInfoProps<TData>) => {

    

    return (
        <>
            {props.recordDataKeys.map((key, index) => (
                <td key={index}>{props.recordData[key]}</td>
            ))}

            {props.buttons.map((button, index) => (
                <td className={"dm-ei-row-cell " + button.cellClassName}
                    key={index}
                >
                    <button className={"dm-ei-row-button " + button.buttonClassName}
                        onClick={() => button.action(props.recordData)}
                    >
                        {button.label}
                    </button>
                </td>
            ))}
            
        </>
    )
}
export default EntryInfoCopmponent;