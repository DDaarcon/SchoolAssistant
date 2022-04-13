interface SubjectData extends TableData {
    id: number;
    name: string;
    sampleData: string;
}

type PageProps = {

};
type PageState = {

}

class SubjectsPage extends React.Component<PageProps, PageState> {
    private _tableData?: SubjectData[];


    fetchTableData() {
        this._tableData = [
            {
                id: 1,
                name: "Język polski",
                sampleData: "Some sample data"
            },
            {
                id: 2,
                name: "Język angielski",
                sampleData: "Some sample data for eng"
            },
            {
                id: 3,
                name: "Język polski",
                sampleData: "Some sample data"
            },
            {
                id: 4,
                name: "Język angielski",
                sampleData: "Some sample data for eng"
            },
            {
                id: 5,
                name: "Język polski",
                sampleData: "Some sample data"
            },
            {
                id: 6,
                name: "Język angielski",
                sampleData: "Some sample data for eng"
            },
        ]
    }

    render() {
        this.fetchTableData();

        return (
            <div className="dm-subjects-page">
                <SubjectTable data={this._tableData} />
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
    }

    componentWillMount() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', `/DataManagement/DataManagement?handler=SubjectData&id=${this.props.recordId}`, true);
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