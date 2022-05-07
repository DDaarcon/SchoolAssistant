import React from "react";
import Loader, { LoaderSize, LoaderType } from "../shared/loader";
import { ResponseJson } from "../shared/server-connection";
import { server } from "./main";
import { ColumnSetting, ModificationComponentProps, TableData } from "./shared-table";
import { Table } from "./table";

export interface SubjectListEntry extends TableData {
    name: string;
}






type SubjectsPageProps = {

};
type SubjectsPageState = {

}

export default class SubjectsPage extends React.Component<SubjectsPageProps, SubjectsPageState> {

    render() {
        return (
            <div className="dm-subjects-page">
                <SubjectTable />
            </div>
        )
    }
}






type SubjectTableProps = {

}

const SubjectTable = (props: SubjectTableProps) => {
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
        <Table
            columnsSetting={columnsSetting}
            modificationComponent={SubjectModificationComponent}
            loadDataAsync={loadAsync}
        />
    );
}






type SubjectModificationComponentProps = ModificationComponentProps;
type SubjectModificationComponentState = {
    awaitingData: boolean;
    data: SubjectListEntry;
}

class SubjectModificationComponent extends React.Component<SubjectModificationComponentProps, SubjectModificationComponentState> {
    constructor(props) {
        super(props);

        this.state = {
            awaitingData: this.props.recordId > 0,
            data: {
                name: ''
            }
        }

        if (this.props.recordId)
            this.fetchAsync();
    }

    private async fetchAsync() {
        let data = await server.getAsync<SubjectListEntry>("SubjectDetails", {
            id: this.props.recordId
        });

        this.setState({ data, awaitingData: false });
    }


    onNameChange: React.ChangeEventHandler<HTMLInputElement> = (event) => {
        const value = event.target.value;

        this.setState(prevState => {
            const data: SubjectListEntry = { ...prevState.data };
            data.name = value;
            return { data };
        });

        this.props.onMadeAnyChange();
    }

    onSubmitAsync: React.FormEventHandler<HTMLFormElement> = async (event) => {
        event.preventDefault();

        let response = await server.postAsync<ResponseJson>("SubjectData", undefined, this.state.data);

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
                        <label htmlFor="name-input">Nazwa</label>
                        <input
                            type="text"
                            id="name-input"
                            className="form-control"
                            value={this.state.data.name}
                            onChange={this.onNameChange}
                        />
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