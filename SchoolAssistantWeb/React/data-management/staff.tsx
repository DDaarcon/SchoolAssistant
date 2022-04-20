import * as React from "react";
import Loader, { LoaderSize, LoaderType } from "../shared/loader";
import { ResponseJson } from "../shared/server-connection";
import { server } from "./main";
import { SubjectData } from "./subjects";
import { ColumnSetting, GroupedModificationComponentProps, GroupedTable, GroupedTableData, TableData } from "./table";
//import * as Select from 'react-select';
//import makeAnimated from 'react-select/animated';

interface StaffPersonData extends TableData {
    name: string;
    specialization?: string;
}

interface StaffPersonDetailedData {
    id?: number;
    firstName: string;
    secondName?: string;
    lastName: string;

    mainSubjectsIds?: number[];
    additionalSubjectsIds?: number[];
}

type StaffPageProps = {

}

type StaffPageState = {

}
export default class StaffPage extends React.Component<StaffPageProps, StaffPageState> {

    render() {
        return (
            <StaffTable />
        )
    }
}





type StaffTableProps = {

}
const StaffTable = (props: StaffTableProps) => {
    const columnsSetting: ColumnSetting<StaffPersonData>[] = [
        {
            header: "Imię i nazwisko",
            prop: "name",
        },
        {
            header: "Specjalizacja",
            prop: "specialization"
        }
    ];

    const loadAsync = async (): Promise<GroupedTableData<StaffPersonData>[]> => {
        let response = await server.getAsync<GroupedTableData<StaffPersonData>[]>("StaffPersonsEntries");
        return response;
    }

    return (
        <GroupedTable<StaffPersonData>
            columnsSetting={columnsSetting}
            loadDataAsync={loadAsync}
            modificationComponent={StaffPersonModificationComponent}
        />
    )
}



//const animatedmultiselectComponent = makeAnimated();

type StaffPersonModificationComponentProps = GroupedModificationComponentProps;
type StaffPersonModificationComponentState = {
    awaitingPersonData: boolean;
    data: StaffPersonDetailedData;
    availableSubjects: SubjectData[];
}
class StaffPersonModificationComponent extends React.Component<StaffPersonModificationComponentProps, StaffPersonModificationComponentState> {
    constructor(props) {
        super(props);

        this.state = {
            awaitingPersonData: this.props.recordId > 0,
            data: {
                firstName: '',
                secondName: '',
                lastName: ''
            },
            availableSubjects: []
        }

        globalThis.alert("jebać cie w ryj");

        if (this.props.recordId)
            this.fetchAsync();

        this.fetchSubjectsAsync();
    }

    private async fetchAsync() {
        let data = await server.getAsync<StaffPersonDetailedData>("StaffPersonDetails", {
            id: this.props.recordId
        });

        this.setState({ data, awaitingPersonData: false });
    }

    private async fetchSubjectsAsync() {
        let availableSubjects = await server.getAsync<SubjectData[]>("AvailableSubjects");

        this.setState({ availableSubjects })
    }

    createOnTextChangeHandler: (property: keyof StaffPersonData) => React.ChangeEventHandler<HTMLInputElement> = (property) => {
        return (event) => {
            const stateUpdate = {};
            stateUpdate[property] = event.target.value;

            this.setState(stateUpdate);

            this.props.onMadeAnyChange();
        }
    }

    onSubmitAsync: React.FormEventHandler<HTMLFormElement> = async (event) => {
        event.preventDefault();

        let response = await server.postAsync<ResponseJson>("SubjectData", undefined, {
            groupId: this.props.groupId,
            ...this.state
        });

        if (response.success)
            await this.props.reloadAsync();
        else
            console.debug(response);
    }

    render() {
        if (this.state.awaitingPersonData)
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
                        <label htmlFor="first-name-input">Imię</label>
                        <input
                            type="text"
                            className="form-control"
                            id="first-name-input"
                            value={this.state.data.firstName}
                            onChange={this.createOnTextChangeHandler('firstName')}
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="first-name-input">Drugie imię</label>
                        <input
                            type="text"
                            className="form-control"
                            id="first-name-input"
                            value={this.state.data.secondName}
                            onChange={this.createOnTextChangeHandler('secondName')}
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="first-name-input">Nazwisko</label>
                        <input
                            type="text"
                            className="form-control"
                            id="first-name-input"
                            value={this.state.data.lastName}
                            onChange={this.createOnTextChangeHandler('lastName')}
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="main-subejcts-input">Główne przedmioty</label>
                        <select
                            className="form-select"
                            multiple
                            id="main-subject-select"
                            value={this.state.data.mainSubjectsIds?.map(x => `${x}`)}
                        >
                            {this.state.availableSubjects.map(x =>
                                <option key={x.id}
                                    value={x.id}
                                >
                                    {x.name}
                                </option>
                            )}
                        </select>
                    </div>
                </form>
            </div>
        )
    }
}