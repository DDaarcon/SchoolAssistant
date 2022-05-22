import React from "react";
import { List } from "../../shared/lists";
import ColumnSetting from "../../shared/lists/interfaces/column-setting";
import { server } from "../main";
import RoomListEntry from "./interfaces/room-list-entry";
import RoomModComp from "./room-mod-comp";

type RoomListProps = {}
const RoomsList = (props: RoomListProps) => {
    const columnsSetting: ColumnSetting<RoomListEntry>[] = [
        {
            header: "Nazwa",
            prop: "name",
            style: { width: '50%' }
        },
        {
            header: "Piętro",
            prop: "floor",
            style: { width: '10%' }
        }
    ];

    const loadAsync = async (): Promise<RoomListEntry[]> => {
        let response = await server.getAsync<RoomListEntry[]>("RoomEntries");
        return response;
    }

    return (
        <List
            columnsSetting={columnsSetting}
            modificationComponent={RoomModComp}
            loadDataAsync={loadAsync}
        />
    );
}
export default RoomsList;