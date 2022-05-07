import React from "react";
import Loader, { LoaderSize, LoaderType } from "../shared/loader";
import modalController from "../shared/modal";
import { ResponseJson } from "../shared/server-connection";
import { server } from "./main";
import { StudentRegisterRecordDetails, StudentRegisterRecordMC } from "./register-records";
import { ColumnSetting, GroupedModificationComponentProps, GroupedTableData, TableData } from "./shared-table";
import { GroupedTable } from "./table";

interface StudentListEntry extends TableData {
    name: string;
    numberInJournal: number;
}

export interface StudentRegisterRecordListEntry {
    id: number;
    name: string;
    className?: string;
}

export interface StudentDetails {
    id?: number;

    registerRecordId?: number;
    registerRecord?: StudentRegisterRecordDetails;

    organizationalClassId?: number;

    numberInJournal?: number;
}


export interface StudentModificationData {
    data: StudentDetails;
    registerRecords: StudentRegisterRecordListEntry[];
}




export type StudentsPageProps = {
    classId: number;
    className: string;
    classSpecialization?: string;
}
type StudentsPageState = {

}
export default class StudentsPage extends React.Component<StudentsPageProps, StudentsPageState> {
    constructor(props) {
        super(props);
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
    awaitingData: boolean;
    data: StudentDetails;
    registerRecords: StudentRegisterRecordListEntry[];
}
class StudentModificationComponent extends React.Component<StudentModificationComponentProps, StudentModificationComponentState> {
    constructor(props) {
        super(props);

        this.state = {
            awaitingData: this.props.recordId > 0,
            data: {
                organizationalClassId: this.props.groupId as number
            },
            registerRecords: []
        }

        if (this.props.recordId)
            this.fetchAsync();
        else
            this.fetchRegisterRecords();
    }

    private async fetchAsync() {
        let response = await server.getAsync<StudentModificationData>("StudentModificationData", {
            id: this.props.recordId
        });

        this.setState({
            data: response.data,
            registerRecords: response.registerRecords,
            awaitingData: false
        });
    }

    private async fetchRegisterRecords() {
        let response = await server.getAsync<StudentRegisterRecordListEntry[]>("StudentRegisterRecordEntries");

        this.setState({ registerRecords: response, awaitingData: false });
    }

    private refetchRegisterRecords = async () => {
        this.setState({ awaitingData: true });
        await this.fetchRegisterRecords();
    }

    createOnTextChangeHandler: (property: keyof StudentDetails) => React.ChangeEventHandler<HTMLInputElement> = (property) => {
        return (event) => {
            const value = event.target.value;

            this.setState(prevState => {
                const data = { ...prevState.data };
                data[property] = (value as unknown) as never;
                return { data };
            });

            this.props.onMadeAnyChange();
        }
    }

    onRegisterRecordChangeHandler: React.ChangeEventHandler<HTMLSelectElement> = (event) => {
        let value = event.target.value == ""
            ? undefined
            : parseInt(event.target.value);

        const setRecord = () => {
            this.setState(prevState => {
                const data = { ...prevState.data };
                data.registerRecordId = value;
                return { data };
            });
        }

        const selected = this.state.registerRecords.find(x => x.id == value);

        if (selected?.className != undefined) {
            modalController.addConfirmation({
                header: "Ten uczeń jest już przypisany do klasy",
                text: `Ten uczeń jest przypisany do klasy ${selected.className}. Czy chcesz przepisać go do tej?`,
                onConfirm: () => {
                    setRecord();
                }
            })
        }
        else
            setRecord();

        this.props.onMadeAnyChange();
    }

    openStudentRegisterRecordMCForCreation = () => {
        modalController.addModificationComponent({
            modificationComponent: StudentRegisterRecordMC,
            modificationComponentProps: {
                onMadeAnyChange: () => { },
                reloadAsync: this.refetchRegisterRecords,
                selectRecord: this.selectRecord
            },
            style: {
                width: '500px'
            }
        });
    }

    openStudentRegisterRecordMCForModification = () => {
        if (!this.state.data.registerRecordId) return;
        modalController.addModificationComponent({
            modificationComponent: StudentRegisterRecordMC,
            modificationComponentProps: {
                recordId: this.state.data.registerRecordId,
                onMadeAnyChange: () => { },
                reloadAsync: this.refetchRegisterRecords,
                selectRecord: this.selectRecord
            }
        })
    }

    private selectRecord = (id: number) => {
        this.setState(prevState => {
            const data = { ...prevState.data };
            data.registerRecordId = id;
            return { data };
        });
    }


    onSubmitAsync: React.FormEventHandler<HTMLFormElement> = async (event) => {
        event.preventDefault();

        let response = await server.postAsync<ResponseJson>("StudentData", undefined, {
            ...this.state.data
        });

        if (response.success)
            await this.props.reloadAsync();
        else
            console.debug(response);
    }

    render() {
        if (this.state.awaitingData)
            return (
                <Loader
                    enable={true}
                    size={LoaderSize.Medium}
                    type={LoaderType.DivWholeSpace}
                />
            )

        return (
            <div>
                <form onSubmit={this.onSubmitAsync}>
                    <div className="form-group">
                        <label htmlFor="number-in-journal-input">Numer w dzienniku</label>
                        <input
                            type="number"
                            className="form-control"
                            name="number-in-journal-input"
                            value={this.state.data.numberInJournal}
                            onChange={this.createOnTextChangeHandler('numberInJournal')}
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="register-record-input">Dane ucznia</label>
                        <select
                            className="form-select"
                            name="register-record-input"
                            value={this.state.data.registerRecordId}
                            onChange={this.onRegisterRecordChangeHandler}
                        >
                            <option value="">Wybierz</option>
                            {this.state.registerRecords.map(x =>
                                <option key={x.id}
                                    value={x.id}
                                >
                                    {x.name}
                                </option>
                            )}
                        </select>

                        {this.state.data.registerRecordId != undefined
                            ? (
                                <button
                                    type="button"
                                    onClick={this.openStudentRegisterRecordMCForModification}
                                >
                                    Edytuj dane ucznia
                                </button>
                            ) : undefined}

                        <button
                            type="button"
                            onClick={this.openStudentRegisterRecordMCForCreation}
                        >
                            Dodaj nowego ucznia
                        </button>

                    </div>

                    <div className="form-group">
                        <input
                            type="submit"
                            value="Zapisz"
                            className="form-control"
                        />
                    </div>
                </form>
            </div>
        )
    }
}