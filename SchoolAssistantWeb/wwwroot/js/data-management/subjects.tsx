interface SubjectData extends TableData {
    id?: number;
    name: string;
}






type PageProps = {

};
type PageState = {
    tableData: SubjectData[];
}

class SubjectsPage extends React.Component<PageProps, PageState> {
    state: PageState = {
        tableData: []
    }

    constructor(props) {
        super(props);

    }

    async componentDidMount() {
        let response = await server.getAsync<SubjectData[]>("SubjectEntries");
        this.setState({
            tableData: response
        });
    }

    render() {
        return (
            <div className="dm-subjects-page">
                <SubjectTable data={this.state?.tableData ?? []} />
            </div>
        )
    }
}






type SubjectTableProps = {
    data: SubjectData[];

}

const SubjectTable = (props: SubjectTableProps) => {
    const headers = [
        "Nazwa",
    ];
    const properties: (keyof SubjectData)[] = [
        "name",
    ];

    return (
        <Table
            headers={headers}
            data={props.data}
            displayProperties={properties}
            modificationComponent={SubjectModificationComponent}
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

    onSubmit: React.FormEventHandler<HTMLFormElement> = async (event) => {
        event.preventDefault();

        if (this.state.id) {

            let response = await server.postAsync("AA");
            console.log(response);
        }
    }

    render() {
        return (
            <div>
                <form onSubmit={this.onSubmit}>
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