import React from "react";
import { EntryInfoProps } from "../lists/components/entry-info-component";
import ColumnSetting from "../lists/interfaces/column-setting";
import List from "../lists/list";
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

    const entryInfoComponent = (irProps: EntryInfoProps<ClassListEntry>) =>
        <>
            {irProps.recordDataKeys.map((key, index) => <td key={index}>{irProps.recordData[key]}</td>)}
            <td className="dm-edit-btn-cell">
                <a onClick={() => props.onMoveToStudents({
                    classId: irProps.recordData.id,
                    className: irProps.recordData.name,
                    classSpecialization: irProps.recordData.specialization
                })} href="#">
                    Uczniowie
                </a>
                <a onClick={irProps.onClickedEditBtn} href="#">
                    Edytuj
                </a>
            </td>
        </>

    return (
        <List<ClassListEntry>
            columnsSetting={columnsSetting}
            loadDataAsync={loadAsync}
            modificationComponent={ClassModComp}
            customEntryInfoComponent={entryInfoComponent}
        />
    )
}
export default ClassesList;