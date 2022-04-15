interface StaffPersonData extends TableData {
    name: string;
    role: string;
}

interface StaffPersonDetailedData {
    id?: number;
    firstName: string;
    secondName?: string;
    lastName: string;

    roleId?: number;

    mainSubjectsIds?: number[];
    additionalSubjectsIds?: number[];
}

type StaffPageProps = {

}

type StaffPageState = {

}
class StaffPage extends React.Component<StaffPageProps, StaffPageState> {

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
            header: "Rola",
            prop: "role"
        }
    ];

    const loadAsync = async (): Promise<StaffPersonData[]> => {
        let response = await server.getAsync<StaffPersonData[]>("StaffPeopleEntries");
        return response;
    }

    return (
        <Table
            columnsSetting={columnsSetting}
            loadDataAsync={loadAsync}
            modificationComponent={StaffPersonModificationComponent}
        />
    )
}





type StaffPersonModificationComponentProps = ModificationComponentProps;
type StaffPersonModificationComponentState = StaffPersonDetailedData & {

}
class StaffPersonModificationComponent extends React.Component<StaffPersonModificationComponentProps, StaffPersonModificationComponentState> {
    constructor(props) {
        super(props);

        this.state = {
            firstName: '',
            secondName: '',
            lastName: ''
        }

        if (this.props.recordId)
            this.fetchAsync();
    }

    private async fetchAsync() {
        let data = await server.getAsync<StaffPersonDetailedData>("StaffPersonDetails", {
            id: this.props.recordId
        });

        this.setState(data);
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
                        <label htmlFor="first-name-input">Imię</label>
                        <input
                            type="text"
                            className="form-control"
                            id="first-name-input"
                            value={this.state.firstName}
                            onChange={this.createOnTextChangeHandler('firstName')}
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="first-name-input">Drugie imię</label>
                        <input
                            type="text"
                            className="form-control"
                            id="first-name-input"
                            value={this.state.secondName}
                            onChange={this.createOnTextChangeHandler('secondName')}
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="first-name-input">Nazwisko</label>
                        <input
                            type="text"
                            className="form-control"
                            id="first-name-input"
                            value={this.state.lastName}
                            onChange={this.createOnTextChangeHandler('lastName')}
                        />
                    </div>
                </form>
            </div>
        )
    }
}