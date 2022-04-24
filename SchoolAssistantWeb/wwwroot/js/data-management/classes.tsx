interface ClassListEntry extends TableData {
    name: string;
    specialization: string;
    amountOfStudents: number;
}


interface ClassDetails {
    id?: number;
    grade: number;
    distinction?: string;
    specialization?: string;

}


type ClassesPageProps = {
    onRedirect: RedirectMethod;
};
type ClassesPageState = {

};

class ClassesPage extends React.Component<ClassesPageProps, ClassesPageState> {

    moveToStudents = (studentsPageProps: StudentsPageProps) => {
        this.props.onRedirect(Category.Students, StudentsPage, studentsPageProps);
    }

    render() {
        return (
            <ClassesTable
                onMoveToStudents={this.moveToStudents}
            />
        )
    }
}


type ClassesTableProps = {
    onMoveToStudents: (studentsPageProps: StudentsPageProps) => void;
}
const ClassesTable = (props: ClassesTableProps) => {
    const columnsSetting: ColumnSetting<ClassListEntry>[] = [
        {
            header: "Klasa",
            prop: "name",
        },
        {
            header: "Kierunek",
            prop: "specialization"
        },
        {
            header: "Liczba uczniów",
            prop: "amountOfStudents"
        }
    ];

    const loadAsync = async (): Promise<ClassListEntry[]> => {
        let response = await server.getAsync<ClassListEntry[]>("ClassEntries");
        return response;
    }

    const informationRow = (irProps: InformationRowProps<ClassListEntry>) => 
        <>
            {irProps.recordDataKeys.map((key, index) => <td key={index}>{irProps.recordData[key]}</td>)}
            <td className="dm-edit-btn-cell">
                <a onClick={() => props.onMoveToStudents({
                    classId: irProps.recordData.id,
                    className: irProps.recordData.name,
                    classSpecialization: irProps.recordData.specialization
                })} href="#">
                    Uczniowie
                </a>
                <a onClick={irProps.onClickedEditBtn} href="#">
                    Edytuj
                </a>
            </td>
        </>

    return (
        <Table<ClassListEntry>
            columnsSetting={columnsSetting}
            loadDataAsync={loadAsync}
            modificationComponent={ClassModificationComponent}
            customInformationRowComponent={informationRow}
        />
    )
}




interface ClassModificationData {
    data: ClassDetails;
}


type ClassModificationComponentProps = ModificationComponentProps;
type ClassModificationComponentState = {
    awaitingData: boolean;
    data: ClassDetails;
}
class ClassModificationComponent extends React.Component<ClassModificationComponentProps, ClassModificationComponentState> {
    constructor(props) {
        super(props);

        this.state = {
            awaitingData: this.props.recordId > 0,
            data: {
                grade: 1,
                distinction: '',
                specialization: ''
            }
        }

        if (this.props.recordId)
            this.fetchAsync();
    }

    private async fetchAsync() {
        let response = await server.getAsync<ClassModificationData>("ClassModificationData", {
            id: this.props.recordId
        });

        this.setState({ data: response.data, awaitingData: false });
    }

    createOnTextChangeHandler: (property: keyof ClassDetails) => React.ChangeEventHandler<HTMLInputElement> = (property) => {
        return (event) => {
            const value = event.target.value;

            this.setState(prevState => {
                const data: ClassDetails = { ...prevState.data };
                data[property] = (value as unknown) as never;
                return { data };
            });

            this.props.onMadeAnyChange();
        }
    }

    onSubmitAsync: React.FormEventHandler<HTMLFormElement> = async (event) => {
        event.preventDefault();

        let response = await server.postAsync<ResponseJson>("ClassData", undefined, {
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
                        <label htmlFor="grade-input">Numer klasy</label>
                        <input
                            type="number"
                            className="form-control"
                            name="grade-input"
                            value={this.state.data.grade}
                            onChange={this.createOnTextChangeHandler('grade')}
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="distinction-input">Dodatkowe rozróżnienie</label>
                        <input
                            type="text"
                            className="form-control"
                            name="distinction-input"
                            value={this.state.data.distinction}
                            onChange={this.createOnTextChangeHandler('distinction')}
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="specialization-input">Kierunek</label>
                        <input
                            type="text"
                            className="form-control"
                            name="specialization-input"
                            value={this.state.data.specialization}
                            onChange={this.createOnTextChangeHandler('specialization')}
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