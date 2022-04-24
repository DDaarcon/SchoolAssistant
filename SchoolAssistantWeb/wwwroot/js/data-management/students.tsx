interface StudentListEntry extends TableData {
    name: string;
    numberInJournal: number;
}

interface StudentDetails {
    id?: number;

    registerRecordId?: number;
    registerRecord?: StudentRegisterRecordDetails;

    organizationalClassId?: number;

    numberInJournal: number;
}

interface StudentRegisterRecordDetails {
    id?: number;

    firstName: string;
    secondName?: string;
    lastName: string;

    dateOfBirth: string;
    placeOfBirth: string;

    personalId: string;
    address: string;

}




type StudentsPageProps = {
    classId: number;
    className: string;
    classSpecialization?: string;
}
type StudentsPageState = {

}
class StudentsPage extends React.Component<StudentsPageProps, StudentsPageState> {
    constructor(props) {
        super(props);

        modalController.add({
            children:
                <div style={{ width: 1000, height: 1000, backgroundColor: 'green' }}>

                </div>
        });
    }

    render() {
        return (
            <>
                <ClassInfoPanel
                    name={this.props.className}
                    specialization={this.props.classSpecialization}
                />
                <StudentsTable
                    classId={this.props.classId}
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

    const loadAsync = async (): Promise<GroupedTableData<StudentListEntry>[]> => {
        let response = await server.getAsync<StudentListEntry[]>("StudentEntries", {
            classId: props.classId
        });
        return [prepareStudentsData(response)];
    }

    const prepareStudentsData = (received: StudentListEntry[]): GroupedTableData<StudentListEntry> => {
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
            <>
            </>
        )
    }
}