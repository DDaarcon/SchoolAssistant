﻿import React from "react";
import ModCompBase from "../../../../shared/form-controls/mod-comp-base";
import MarkInput from "../../../marks/mark-input";
import StoreAndSaveService from "../../../services/store-and-save-service";
import GiveMarkModel from "./give-mark-model";
import StudentSelectionEntry from "./student-selection-entry";
import './giving-mark-page.css';
import { Input, SubmitButton, TextArea } from "../../../../shared/form-controls";

type GivingMarkPageProps = {}
type GivingMarkPageState = {
    data: GiveMarkModel;
}

export default class GivingMarkPage extends ModCompBase<GiveMarkModel, GivingMarkPageProps, GivingMarkPageState> {

    constructor(props) {
        super(props);

        this.state = {
            data: {
                description: ""
            }
        };

        this._validator.setRules({
            description: {
                notNull: true, notEmpty: 'Należy wprowadzić opis oceny'
            },
            mark: {
                notNull: 'Należy podać ocenę',
                other: (model, on) => {
                    if (!model[on].value)
                        return {
                            error: 'Należy podać ocenę',
                            on
                        }

                    return undefined;
                }
            },
            studentId: {
                notNull: true
            },
            weight: {
                other: (model, on) => {
                    // TODO: validate weight
                    return undefined;
                }
            }
        });
    }

    render() {
        return (
            <form className="giving-mark-page"
                onSubmit={this.submitAsync}
            >

                <div className="giving-mark-details">

                    <div className="giving-mark-details-first-row">
                        <MarkInput
                            mark={this.state.data.mark}
                            onChange={mark => this.setStateFnData(data => data.mark = mark)}
                            errorMessages={this._validator.getErrorMsgsFor('mark')}
                            warningMessages={this._validator.getWarningMsgsFor('mark')}
                            containerClassName="one-mark-input"
                        />

                        <SubmitButton
                            value="Zapisz"
                        />
                    </div>

                    <TextArea
                        label="Opis"
                        name="description-input"
                        value={this.state.data.description}
                        onChange={value => this.setStateFnData(data => data.description = value)}
                        errorMessages={this._validator.getErrorMsgsFor('description')}
                        warningMessages={this._validator.getWarningMsgsFor('description')}
                        inputClassName="description-input"
                    />

                    <Input
                        label="Waga"
                        name="weight-input"
                        type="number"
                        value={this.state.data.weight}
                        onChangeV={value => this.setStateFnData(data => data.weight = parseInt(value))}
                        warningMessages={this._validator.getWarningMsgsFor('weight')}
                        containerClassName="weight-input-container"
                        inputClassName="weight-input"
                    />
                </div>

                <div className="student-selection-list">
                    {StoreAndSaveService.students.map(student => (
                        <StudentSelectionEntry
                            key={student.id}
                            selected={this.state.data.studentId == student.id}
                            select={studentId => this.setStateFnData(data => data.studentId = studentId)}
                            {...student}
                        />
                    ))}
                </div>
            </form>
        )
    }


    private submitAsync: React.FormEventHandler<HTMLFormElement> = async (event) => {
        event.preventDefault();

        if (!this._validator.validate()) {
            this.forceUpdate();
            return;
        }
    }

    private validateMark
}