import React from "react";
import { Input, SubmitButton, TextArea } from "../../../../shared/form-controls";
import ModCompBase from "../../../../shared/form-controls/mod-comp-base";
import StoreAndSaveService from "../../../services/store-and-save-service";
import GiveGroupMarkModel from "./give-group-mark-model";
import StudentMarkInsertionEntry from "./student-mark-insertion-entry";

type GivingGroupMarkPageProps = {}
type GivingGroupMarkPageState = {
    data: GiveGroupMarkModel;
}

export default class GivingGroupMarkPage extends ModCompBase<GiveGroupMarkModel, GivingGroupMarkPageProps, GivingGroupMarkPageState> {

    constructor(props) {
        super(props);

        this.state = {
            data: {
                description: '',
                marks: {}
            }
        }

        this._validator.setRules({
            description: {
                notNull: true, notEmpty: 'Należy wprowadzić opis oceny'
            },
            marks: {
                other: (model, on) => {
                    // TODO: validate marks

                    return undefined;
                }
            },
            weight: {
                other: (model, on) => {
                    // TODO: validated weight
                    return undefined;
                }
            }
        });
    }

    render() {
        return (
            <form className="giving-group-mark-page"
                onSubmit={this.submitAsync}
            >
                <div className="giving-group-mark-details">
                    <SubmitButton
                        value="Zapisz"
                        containerClassName="giving-group-mark-submit-btn"
                    />

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
                        onChangeV={this.changeWeight}
                        warningMessages={this._validator.getWarningMsgsFor('weight')}
                        containerClassName="weight-input-container"
                        inputClassName="weight-input"
                    />
                </div>

                <div className="student-mark-insertion-list">
                    {StoreAndSaveService.students.map(student => (
                        <StudentMarkInsertionEntry
                            key={student.id}
                            mark={this.state.data.marks[student.id]}
                            onChange={mark => this.setStateFnData(data => data.marks[student.id] = mark)}
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

    private changeWeight = (value: string) => {
        if (!value) {
            this.setStateFnData(data => data.weight = undefined);
            return;
        }
        const int = parseInt(value);
        if (int)
            this.setStateFnData(data => data.weight = int);
    }
}