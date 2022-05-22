import React from "react";
import { GroupList } from "../../shared/lists";
import ColumnSetting from "../../shared/lists/interfaces/column-setting";
import GroupListEntry from "../../shared/lists/interfaces/group-list-entry";
import { server } from "../main";
import StudentListEntry from "./interfaces/student-list-entry";
import StudentModComp from "./student-mod-comp";

type StudentsListProps = {
    classId: number;
}
const StudentsList = (props: StudentsListProps) => {
    const columnsSetting: ColumnSetting<StudentListEntry>[] = [
        {
            header: "Numer",
            prop: 'numberInJournal',
            style: {
                width: '20px'
            }
        },
        {
            header: "Imię i nazwisko",
            prop: "name",
        }
    ];

    const loadAsync = async (): Promise<GroupListEntry<StudentListEntry>[]> => {
        let response = await server.getAsync<StudentListEntry[]>("StudentEntries", {
            classId: props.classId
        });
        return [prepareStudentsData(response)];
    }

    const prepareStudentsData = (received: StudentListEntry[]): GroupListEntry<StudentListEntry> => {
        const highestJournalNr = findHightestNr(received.map(x => x.numberInJournal));

        const data: StudentListEntry[] = [];
        for (let i = 1; i <= highestJournalNr; i++) {
            const existing = received.find(x => x.numberInJournal == i);
            if (existing)
                data.push(existing);
            else
                data.push({
                    numberInJournal: i,
                    id: 0,
                    name: '',
                });
        }
        return {
            id: props.classId,
            entries: data
        }
    }

    const findHightestNr = (numbers: number[]): number => {
        let highest = 0;
        for (const nr of numbers)
            highest = highest < nr ? nr : highest;
        return highest;
    }

    return (
        <GroupList<StudentListEntry>
            columnsSetting={columnsSetting}
            loadDataAsync={loadAsync}
            modificationComponent={StudentModComp}
        />
    )
}
export default StudentsList;