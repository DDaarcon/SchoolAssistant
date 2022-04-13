interface SubjectData extends TableData {
    id: number;
    name: string;
    sampleData: string;
}

type PageProps = {

};
type PageState = {
    tableData: SubjectData[];
}

class SubjectsPage extends React.Component<PageProps, PageState> {
    private readonly _fetchUrl = `${baseUrl}?handler=SubjectEntries`;

    state: PageState = {
        tableData: []
    }

    constructor(props) {
        super(props);

    }

    componentDidMount() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', this._fetchUrl, true);
        xhr.onload = () => {
            const res = JSON.parse(xhr.responseText);
            this.setState({ tableData: res });
        };
        xhr.send();
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
        "Przykładowe dane"
    ];
    const properties: (keyof SubjectData)[] = [
        "name",
        "sampleData"
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
type SubjectModificationComponentState = {
    data: SubjectData;
}

class SubjectModificationComponent extends React.Component<SubjectModificationComponentProps, SubjectModificationComponentState> {
    constructor(props) {
        super(props);

        const xhr = new XMLHttpRequest();
        xhr.open('get', `${baseUrl}?handler=SubjectData&id=${this.props.recordId}`, true);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        };
        xhr.send();
    }

    render() {
        return (
            <div>
                {this.state?.data.id},{this.state?.data.name},{this.state?.data.sampleData}
            </div>
        )
    }
}