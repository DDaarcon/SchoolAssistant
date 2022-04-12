type PageProps = {

};
type PageState = {

}

class SubjectsPage extends React.Component<PageProps, PageState> {

    fetchTableData() {
        this.tableData = [
            {
                id: 1,
                name: "Język polski",
                sampleData: "Some sample data"
            },
            {
                id: 2,
                name: "Język angielski",
                sampleData: "Some sample data for eng"
            }
        ]
    }

    render() {
        this.fetchTableData();

        return (
            <div className="dm-subjects-page">
                <SubjectTable data={this.tableData} />
            </div>
        )
    }
}

function SubjectTable(props) {
    const headers = [
        "Nazwa",
        "Przykładowe dane"
    ];
    const properties = [
        "name",
        "sampleData"
    ];

    return (
        <Table
            headers={headers}
            data={props.data}
            displayProperties={properties}
            tableRecordComponent={SubjectTableRecord}
            modificationComponent={undefined}
        />
    );
}

function SubjectTableRecord(props) {
    return (
        <TableRecord
            recordId={props.recordId}
            recordData={props.recordData}

            displayProperties={props.displayProperties}
            onOpenEdit={props.onOpenEdit}
            modifying={props.modifying}

            modificationComponent={props.modificationComponent}

        />
    )
}

class SubjectModificationRow extends React.Component {
    constructor(props) {
        super()
    }
}