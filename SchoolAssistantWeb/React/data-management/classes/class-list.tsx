import React from "react";
import { List } from "../../shared/lists";
import ColumnSetting from "../../shared/lists/interfaces/column-setting";
import { server } from "../main";
import { StudentsPageProps } from "../students/students-page";
import ClassModComp from "./class-mod-comp";
import ClassListEntry from "./interfaces/class-list-entry";

type ClassesListProps = {
    onMoveToStudents: (studentsPageProps: StudentsPageProps) => void;
}
const ClassesList = (props: ClassesListProps) => {
    const columnsSetting: ColumnSetting<ClassListEntry>[] = [
        {
            header: "Klasa",
            prop: "name",
        },
        {
            header: "Kierunek",
            prop: "specialization"
        },
        {
            header: "Liczba uczniów",
            prop: "amountOfStudents"
        }
    ];

    const loadAsync = async (): Promise<ClassListEntry[]> => {
        let response = await server.getAsync<ClassListEntry[]>("ClassEntries");
        return response;
    }

    return (
        <List<ClassListEntry>
            columnsSetting={columnsSetting}
            loadDataAsync={loadAsync}
            modificationComponent={ClassModComp}
            customButtons={[{
                label: "Uczniowie",
                action: (entry) => props.onMoveToStudents({
                    classId: entry.id,
                    className: entry.name,
                    classSpecialization: entry.specialization
                }),
                columnStyle: {
                    width: '1%',
                    minWidth: 150
                }
            }]}
        />
    )
}
export default ClassesList;