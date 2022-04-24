interface StudentListEntry extends TableData {
    name: string;
    numberInJurnal: number;
}






type StudentsPageProps = {
    classId: number;
    className: string;
    classSpecialization?: string;
}
type StudentsPageState = {

}
class StudentsPage extends React.Component<StudentsPageProps, StudentsPageState> {


    render() {
        return (
            <>
                <ClassInfoPanel
                    name={this.props.className}
                    specialization={this.props.classSpecialization}
                />
            </>
        )
    }
}




type ClassInfoPanelProps = {
    name: string;
    specialization?: string;
}
const ClassInfoPanel = (props: ClassInfoPanelProps) => {

    return (
        <div className="dm-students-class-info-panel">
            <div className="dm-cip-name">
                {props.name}
            </div>
            <div className="dm-cip-spec">
                {props.specialization}
            </div>
        </div>
    )
}




type StudentsTableProps = {
    classId: number;
}
const StudentsTable = (props: StudentsTableProps) => {
    const columnsSetting: ColumnSetting<StudentListEntry>[] = [
        {
            header: "Imię i nazwisko",
            prop: "name",
        }
    ];

    const loadAsync = async (): Promise<GroupedTableData<StudentListEntry>[]> => {
        let response = await server.getAsync<StudentListEntry[]>("StudentEntries", {
            classId: props.classId
        });
        return [prepareStudentsData(response)];
    }

    const prepareStudentsData = (received: StudentListEntry[]): GroupedTableData<StudentListEntry> => {
        const highestJournalNr = findHightestNr(received.map(x => x.numberInJurnal));

        const data: StudentListEntry[] = [];
        for (let i = 1; i <= highestJournalNr; i++) {
            const existing = received.find(x => x.numberInJurnal == i);
            if (existing)
                data.push(existing);
            else
                data.push({
                    numberInJurnal: i,
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
        <GroupedTable<StudentListEntry>
            columnsSetting={columnsSetting}
            loadDataAsync={loadAsync}
            modificationComponent={StudentModificationComponent}
        />
    )
}




type StudentModificationComponentProps = GroupedModificationComponentProps;
type StudentModificationComponentState = {

}
class StudentModificationComponent extends React.Component<StudentModificationComponentProps, StudentModificationComponentState> {


    render() {
        return (
            <></>
        )
    }
}