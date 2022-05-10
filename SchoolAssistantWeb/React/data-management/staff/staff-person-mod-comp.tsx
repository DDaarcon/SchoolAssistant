﻿import React from "react";
import { MultiValue } from "react-select";
import { Input, Multiselect, OnChangeIdHandler, Option } from "../../shared/form-controls";
import Loader, { LoaderSize, LoaderType } from "../../shared/loader";
import { ResponseJson } from "../../shared/server-connection";
import Validator from "../../shared/validator";
import { SharedGroupModCompProps } from "../lists/interfaces/shared-group-mod-comp-props";
import { server } from "../main";
import SubjectListEntry from "../subjects/interfaces/subject-list-entry";
import StaffPersonDetails from "./interfaces/staff-person-details";
import StaffPersonListEntry from "./interfaces/staff-person-list-entry";

type StaffPersonModCompProps = SharedGroupModCompProps;
type StaffPersonModCompState = {
    awaitingPersonData: boolean;
    data: StaffPersonDetails;
    availableSubjects: SubjectListEntry[];
}
export default class StaffPersonModComp extends React.Component<StaffPersonModCompProps, StaffPersonModCompState> {
    private _validator = new Validator<StaffPersonDetails>();

    private get _mainSubjectOptions() { return this.getSubjectOptions(this.state.data.mainSubjectsIds); }
    private get _additionalSubjectOptions() { return this.getSubjectOptions(this.state.data.additionalSubjectsIds); }

    private getSubjectOptions(from: number[]): MultiValue<Option<number>> {
        return this.state.availableSubjects.filter(x => from.includes(x.id)).map(x => ({
            label: x.name,
            value: x.id
        }));
    }

    constructor(props) {
        super(props);

        this.state = {
            awaitingPersonData: this.props.recordId > 0,
            data: {
                firstName: '',
                secondName: '',
                lastName: ''
            },
            availableSubjects: []
        }

        this._validator.forModelGetter(() => this.state.data);
        this._validator.setRules({
            firstName: { notNull: true, notEmpty: true },
            lastName: { notNull: true, notEmpty: true },
            additionalSubjectsIds: {
                other: (model, prop) => {
                    if (model.additionalSubjectsIds.some(x => model.mainSubjectsIds.includes(x)))
                        return {
                            on: prop,
                            error: "Ten sam przedmiot nie może być jednocześnie głównym i dodatkowym"
                        };
                }
            }
        });

        if (this.props.recordId)
            this.fetchAsync();

        this.fetchSubjectsAsync();
    }

    private async fetchAsync() {
        let data = await server.getAsync<StaffPersonDetails>("StaffPersonDetails", {
            id: this.props.recordId,
            groupId: this.props.groupId
        });

        this.setState({ data, awaitingPersonData: false });
    }

    private async fetchSubjectsAsync() {
        let availableSubjects = await server.getAsync<SubjectListEntry[]>("AvailableSubjects");

        this.setState({ availableSubjects })
    }

    createOnTextChangeHandler: (property: keyof StaffPersonListEntry) => React.ChangeEventHandler<HTMLInputElement> = (property) => {
        return (event) => {
            const value = event.target.value;

            this.setState(prevState => {
                const data = { ...prevState.data };
                data[property] = value;
                return { data };
            });

            this.props.onMadeAnyChange();
        }
    }

    createOnSubjectsChangeHandler: (property: keyof StaffPersonListEntry) => OnChangeIdHandler<number> = (property) => {
        return (values) => {
            this.setState(prevState => {
                const data = { ...prevState.data };
                data[property] = values;
                return { data };
            });

            this.props.onMadeAnyChange();
        }
    }

    onSubmitAsync: React.FormEventHandler<HTMLFormElement> = async (event) => {
        event.preventDefault();

        if (!this._validator.validate()) {
            this.forceUpdate();
            return;
        }

        let response = await server.postAsync<ResponseJson>("StaffPersonData", undefined, {
            groupId: this.props.groupId,
            ...this.state.data
        });

        if (response.success)
            await this.props.reloadAsync();
        else
            console.debug(response);
    }

    render() {
        if (this.state.awaitingPersonData)
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
                        name="first-name-input"
                        label="Imię"
                        value={this.state.data.firstName}
                        onChange={this.createOnTextChangeHandler('firstName')}
                        errorMessages={this._validator.getErrorMsgsFor('firstName')}
                        type="text"
                    />
                    <Input
                        name="second-name-input"
                        label="Drugie imię"
                        value={this.state.data.secondName}
                        onChange={this.createOnTextChangeHandler('secondName')}
                        errorMessages={this._validator.getErrorMsgsFor('secondName')}
                        type="text"
                    />
                    <Input
                        name="last-name-input"
                        label="Nazwisko"
                        value={this.state.data.lastName}
                        onChange={this.createOnTextChangeHandler('lastName')}
                        errorMessages={this._validator.getErrorMsgsFor('lastName')}
                        type="text"
                    />
                    <Multiselect
                        name="main-subejcts-input"
                        label="Główne przedmioty"
                        value={this._mainSubjectOptions}
                        onChangeId={this.createOnSubjectsChangeHandler('mainSubjectsIds')}
                        errorMessages={this._validator.getErrorMsgsFor('mainSubjectsIds')}
                        options={this.state.availableSubjects.map(x => ({
                            label: x.name,
                            value: x.id
                        }))}
                    />
                    <Multiselect
                        name="additional-subejcts-input"
                        label="Dodatkowe przedmioty"
                        value={this._additionalSubjectOptions}
                        onChangeId={this.createOnSubjectsChangeHandler('additionalSubjectsIds')}
                        errorMessages={this._validator.getErrorMsgsFor('additionalSubjectsIds')}
                        options={this.state.availableSubjects.map(x => ({
                            label: x.name,
                            value: x.id
                        }))}
                    />

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