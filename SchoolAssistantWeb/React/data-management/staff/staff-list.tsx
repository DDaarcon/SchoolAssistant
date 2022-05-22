import React from "react";
import { GroupList } from "../../shared/lists";
import ColumnSetting from "../../shared/lists/interfaces/column-setting";
import GroupListEntry from "../../shared/lists/interfaces/group-list-entry";
import { server } from "../main";
import StaffPersonListEntry from "./interfaces/staff-person-list-entry";
import StaffPersonModComp from "./staff-person-mod-comp";

type StaffListProps = {}
const StaffList = (props: StaffListProps) => {
    const columnsSetting: ColumnSetting<StaffPersonListEntry>[] = [
        {
            header: "Imię i nazwisko",
            prop: "name",
        },
        {
            header: "Specjalizacja",
            prop: "specialization"
        }
    ];

    const loadAsync = async (): Promise<GroupListEntry<StaffPersonListEntry>[]> => {
        let response = await server.getAsync<GroupListEntry<StaffPersonListEntry>[]>("StaffPersonsEntries");
        return response;
    }

    return (
        <GroupList<StaffPersonListEntry>
            columnsSetting={columnsSetting}
            loadDataAsync={loadAsync}
            modificationComponent={StaffPersonModComp}
        />
    )
}
export default StaffList;