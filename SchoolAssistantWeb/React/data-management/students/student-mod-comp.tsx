import React from "react";
import { ActionButton } from "../../shared/components";
import { Input, Select, OnChangeIdHandler, SubmitButton } from "../../shared/form-controls";
import ModCompBase from "../../shared/form-controls/mod-comp-base";
import { SharedGroupModCompProps } from "../../shared/lists/interfaces/shared-group-mod-comp-props";
import Loader, { LoaderSize, LoaderType } from "../../shared/loader";
import { modalController } from "../../shared/modals";
import { ResponseJson } from "../../shared/server-connection";
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
export default class StudentModComp extends ModCompBase<StudentDetails, StudentModCompProps, StudentModCompState> {

    constructor(props) {
        super(props);

        this.state = {
            awaitingData: this.props.recordId > 0,
            data: {
                organizationalClassId: this.props.groupId as number
            },
            registerRecords: []
        }

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

    changeNumberInJournal: React.ChangeEventHandler<HTMLInputElement> = (event) => {
        const value = event.target.value;

        this.setStateFnData(data => data.numberInJournal = (value as unknown) as number);

        this.props.onMadeAnyChange();
    }

    onRegisterRecordChangeHandler: OnChangeIdHandler<number> = (id) => {
        const setRecord = () => this.setStateFnData(data => data.registerRecordId = id as number);

        const selected = this.state.registerRecords.find(x => x.id == id);

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
                selectRecord: id => this.setStateFnData(data => data.registerRecordId = id)
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
                selectRecord: id => this.setStateFnData(data => data.registerRecordId = id)
            }
        })
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
                        onChange={this.changeNumberInJournal}
                        errorMessages={this._validator.getErrorMsgsFor('numberInJournal')}
                        type="number"
                    />

                    <Select
                        name="register-record-input"
                        label="Dane ucznia"
                        value={this.state.data.registerRecordId}
                        onChangeId={this.onRegisterRecordChangeHandler}
                        options={this.state.registerRecords.map(x => ({
                            label: x.name,
                            value: x.id
                        })) }
                        errorMessages={this._validator.getErrorMsgsFor('registerRecordId')}
                    />

                    {this.state.data.registerRecordId != undefined
                        ? (
                            <ActionButton
                                label="Edytuj dane ucznia"
                                onClick={this.openStudentRegisterRecordMCForModification}
                            />
                        ) : undefined}

                    <ActionButton
                        label="Dodaj nowego ucznia"
                        onClick={this.openStudentRegisterRecordMCForCreation}
                    />

                    <SubmitButton
                        value="Zapisz"
                    />
                </form>
            </div>
        )
    }
}