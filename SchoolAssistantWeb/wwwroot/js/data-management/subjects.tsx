interface SubjectData extends TableData {
    id?: number;
    name: string;
}






type PageProps = {

};
type PageState = {

}

class SubjectsPage extends React.Component<PageProps, PageState> {
    constructor(props) {
        super(props);

    }

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
    const headers = [
        "Nazwa",
    ];
    const properties: (keyof SubjectData)[] = [
        "name",
    ];

    const loadAsync = async (): Promise<SubjectData[]> => {
        let response = await server.getAsync<SubjectData[]>("SubjectEntries");
        return response;
    }

    return (
        <Table
            headers={headers}
            displayProperties={properties}
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
                    <label>
                        Nazwa:
                        <input type="text" value={this.state.name} onChange={this.onNameChange} />
                        <input type="submit" value="Zapisz" />
                    </label>
                </form>
            </div>
        )
    }
}