interface SubjectData extends TableData {
    name: string;
}






type PageProps = {

};
type PageState = {

}

class SubjectsPage extends React.Component<PageProps, PageState> {

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
    const columnsSetting: ColumnSetting<SubjectData>[] = [
        {
            header: "Nazwa",
            prop: "name",
            style: { width: '50%' }
        }
    ];

    const loadAsync = async (): Promise<SubjectData[]> => {
        let response = await server.getAsync<SubjectData[]>("SubjectEntries");
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
type SubjectModificationComponentState = SubjectData & {

}

class SubjectModificationComponent extends React.Component<SubjectModificationComponentProps, SubjectModificationComponentState> {
    constructor(props) {
        super(props);

        this.state = {
            name: ''
        }

        if (this.props.recordId)
            this.fetchAsync();
    }

    private async fetchAsync() {
        let data = await server.getAsync<SubjectData>("SubjectDetails", {
            id: this.props.recordId
        });

        this.setState(data);
    }


    onNameChange: React.ChangeEventHandler<HTMLInputElement> = (event) => {
        this.setState({ name: event.target.value });
        this.props.onMadeAnyChange();
    }

    onSubmitAsync: React.FormEventHandler<HTMLFormElement> = async (event) => {
        event.preventDefault();

        let response = await server.postAsync<ResponseJson>("SubjectData", undefined, this.state);

        if (response.success)
            await this.props.reloadAsync();
        else
            console.debug(response);
    }

    render() {
        return (
            <div>
                <form onSubmit={this.onSubmitAsync}>
                    <div className="form-group">
                        <label htmlFor="subject-name">Nazwa</label>
                        <input
                            type="text"
                            id="subject-name"
                            className="form-control"
                            value={this.state.name}
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