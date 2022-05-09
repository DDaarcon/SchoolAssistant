import React from "react";
import { Input, Select, Option } from "../../shared/form-controls";
import Loader, { LoaderSize, LoaderType } from "../../shared/loader";
import { modalController } from "../../shared/modals";
import { ResponseJson } from "../../shared/server-connection";
import Validator from "../../shared/validator";
import { SharedGroupModCompProps } from "../lists/interfaces/shared-group-mod-comp-props";
import { server } from "../main";
import StudentRegisterRecordModComp, { StudentRegisterRecordModCompProps } from "./components/student-reg-rec-mod-comp";
import StudentDetails from "./interfaces/student-details";
import StudentModificationData from "./interfaces/student-modification-data";
import StudentRegisterRecordListEntry from "./interfaces/student-reg-rec-list-entry";

type StudentModCompProps = SharedGroupModCompProps;
type StudentModCompState = {
    awaitingData: boolean;
    data: StudentDetails;
    registerRecords: StudentRegisterRecordListEntry[];
}
export default class StudentModComp extends React.Component<StudentModCompProps, StudentModCompState> {
    private _validator = new Validator<StudentDetails>()

    constructor(props) {
        super(props);

        this.state = {
            awaitingData: this.props.recordId > 0,
            data: {
                organizationalClassId: this.props.groupId as number
            },
            registerRecords: []
        }

        this._validator.forModelGetter(() => this.state.data);
        this._validator.setRules({
            numberInJournal: {
                notNull: true,
                other: (model, prop) => (model.numberInJournal < 1
                    ? { error: 'Numer w dzienniku musi być większy od 0', on: prop }
                    : undefined)
            },
            registerRecordId: { notNull: true }
        });

        if (this.props.recordId)
            this.fetchAsync();
        else
            this.fetchRegisterRecords();
    }

    private async fetchAsync() {
        let response = await server.getAsync<StudentModificationData>("StudentModificationData", {
            id: this.props.recordId
        });

        this.setState({
            data: response.data,
            registerRecords: response.registerRecords,
            awaitingData: false
        });
    }

    private async fetchRegisterRecords() {
        let response = await server.getAsync<StudentRegisterRecordListEntry[]>("StudentRegisterRecordEntries");

        this.setState({ registerRecords: response, awaitingData: false });
    }

    private refetchRegisterRecords = async () => {
        this.setState({ awaitingData: true });
        await this.fetchRegisterRecords();
    }

    createOnTextChangeHandler: (property: keyof StudentDetails) => React.ChangeEventHandler<HTMLInputElement> = (property) => {
        return (event) => {
            const value = event.target.value;

            this.setState(prevState => {
                const data = { ...prevState.data };
                data[property] = (value as unknown) as never;
                return { data };
            });

            this.props.onMadeAnyChange();
        }
    }

    onRegisterRecordChangeHandler: (value: Option<number>) => void = (value) => {
        const setRecord = () => {
            this.setState(prevState => {
                const data = { ...prevState.data };
                data.registerRecordId = value.value;
                return { data };
            });
        }

        const selected = this.state.registerRecords.find(x => x.id == value.value);

        if (selected?.className != undefined) {
            modalController.addConfirmation({
                header: "Ten uczeń jest już przypisany do klasy",
                text: `Ten uczeń jest przypisany do klasy ${selected.className}. Czy chcesz przepisać go do tej?`,
                onConfirm: () => {
                    setRecord();
                }
            })
        }
        else
            setRecord();

        this.props.onMadeAnyChange();
    }

    openStudentRegisterRecordMCForCreation = () => {
        modalController.addCustomComponent<StudentRegisterRecordModCompProps>({
            modificationComponent: StudentRegisterRecordModComp,
            modificationComponentProps: {
                reloadAsync: this.refetchRegisterRecords,
                selectRecord: this.selectRecord
            },
            style: {
                width: '500px'
            }
        });
    }

    openStudentRegisterRecordMCForModification = () => {
        if (!this.state.data.registerRecordId) return;

        modalController.addCustomComponent<StudentRegisterRecordModCompProps>({
            modificationComponent: StudentRegisterRecordModComp,
            modificationComponentProps: {
                recordId: this.state.data.registerRecordId,
                reloadAsync: this.refetchRegisterRecords,
                selectRecord: this.selectRecord
            }
        })
    }

    private selectRecord = (id: number) => {
        this.setState(prevState => {
            const data = { ...prevState.data };
            data.registerRecordId = id;
            return { data };
        });
    }


    onSubmitAsync: React.FormEventHandler<HTMLFormElement> = async (event) => {
        event.preventDefault();

        if (!this._validator.validate()) {
            this.forceUpdate();
            return;
        }

        let response = await server.postAsync<ResponseJson>("StudentData", undefined, {
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
                    <Input
                        name="number-in-journal-input"
                        label="Numer w dzienniku"
                        value={this.state.data.numberInJournal}
                        onChange={this.createOnTextChangeHandler('numberInJournal')}
                        errorMessages={this._validator.getErrorMsgsFor('numberInJournal')}
                        type="number"
                    />

                    <Select
                        name="register-record-input"
                        label="Dane ucznia"
                        value={this.state.data.registerRecordId}
                        onChange={this.onRegisterRecordChangeHandler}
                        options={this.state.registerRecords.map(x => ({
                            label: x.name,
                            value: x.id
                        })) }
                        errorMessages={this._validator.getErrorMsgsFor('registerRecordId')}
                    />

                    {this.state.data.registerRecordId != undefined
                        ? (
                            <button
                                type="button"
                                onClick={this.openStudentRegisterRecordMCForModification}
                            >
                                Edytuj dane ucznia
                            </button>
                        ) : undefined}

                    <button
                        type="button"
                        onClick={this.openStudentRegisterRecordMCForCreation}
                    >
                        Dodaj nowego ucznia
                    </button>

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