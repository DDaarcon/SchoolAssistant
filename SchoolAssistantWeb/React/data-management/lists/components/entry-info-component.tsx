import React from "react";
import ListEntry from "../interfaces/list-entry";

export type EntryInfoProps<TEntry extends ListEntry> = {
    recordDataKeys: (keyof TEntry)[];
    recordData: TEntry;
    onClickedEditBtn: React.MouseEventHandler<HTMLAnchorElement>;
}
const EntryInfoCopmponent = <TData extends ListEntry>(props: EntryInfoProps<TData>) => {
    return (
        <>
            {props.recordDataKeys.map((key, index) => <td key={index}>{props.recordData[key]}</td>)}
            <td className="dm-edit-btn-cell">
                <a onClick={props.onClickedEditBtn} href="#">
                    Edytuj
                </a>
            </td>
        </>
    )
}
export default EntryInfoCopmponent;