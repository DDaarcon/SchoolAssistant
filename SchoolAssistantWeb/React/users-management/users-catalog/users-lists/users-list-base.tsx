import React from "react";
import { ReadonlyList } from "../../../shared/lists";
import ColumnSetting from "../../../shared/lists/interfaces/column-setting";
import UserListEntry from "../interfaces/user-list-entry";

type UsersListBaseProps<TEntry extends UserListEntry> = {
    initialData?: TEntry[];
}
type UsersListBaseState = {

}

export default abstract class UsersListBase
    <TEntry extends UserListEntry> extends React.Component<UsersListBaseProps<TEntry>, UsersListBaseState> {

    protected _columnsSetting: ColumnSetting<TEntry>[];

    constructor(props) {
        super(props);

        this._columnsSetting = [
            {
                header: "Nazwisko",
                prop: "lastName",
            },
            {
                header: "Imię",
                prop: "firstName"
            },
            {
                header: "Nazwa użytkownika",
                prop: "userName"
            },
            {
                header: "Email",
                prop: "email"
            },
            ...(this.extraColumnsSettings?.() ?? [])
        ];
    }


    protected extraColumnsSettings?(): ColumnSetting<TEntry>[];

    protected abstract loadAsync: () => Promise<TEntry[]>;

    render() {
        return (
            <ReadonlyList
                columnsSetting={this._columnsSetting}
                loadDataAsync={this.loadAsync}
            />
        )
    }
}