import React from "react";
import ColumnSetting from "../lists/interfaces/column-setting";
import List from "../lists/list";
import { server } from "../main";
import SubjectListEntry from "./interfaces/subject-list-entry";
import SubjectModComp from "./subject-mod-comp";

type SubjectListProps = {

}

const SubjectList = (props: SubjectListProps) => {
    const columnsSetting: ColumnSetting<SubjectListEntry>[] = [
        {
            header: "Nazwa",
            prop: "name",
            style: { width: '50%' }
        }
    ];

    const loadAsync = async (): Promise<SubjectListEntry[]> => {
        let response = await server.getAsync<SubjectListEntry[]>("SubjectEntries");
        return response;
    }

    return (
        <List
            columnsSetting={columnsSetting}
            modificationComponent={SubjectModComp}
            loadDataAsync={loadAsync}
        />
    );
}
export default SubjectList;