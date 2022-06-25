import React from "react";
import { Input, SubmitButton, TextArea } from "../../../../shared/form-controls";
import ModCompBase from "../../../../shared/form-controls/mod-comp-base";
import StoreService from "../../../services/store-service";
import GiveGroupMarkModel from "./give-group-mark-model";
import StudentMarkInsertionEntry from "./student-mark-insertion-entry";
import './giving-group-mark-page.css';
import SaveButtonService from "../../../services/save-button-service";
import server from "../../../../scheduled-lessons-list/server";
import { ResponseJson } from "../../../../shared/server-connection";

type GivingGroupMarkPageProps = {}
type GivingGroupMarkPageState = {
    data: GiveGroupMarkModel;
}

export default class GivingGroupMarkPage extends ModCompBase<GiveGroupMarkModel, GivingGroupMarkPageProps, GivingGroupMarkPageState> {

    constructor(props) {
        super(props);

        this.state = {
            data: {
                lessonId: StoreService.lessonId,
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

        SaveButtonService.show();
        SaveButtonService.setAction(this.submitAsync);
    }

    render() {
        return (
            <form className="giving-group-mark-page">
                <div className="giving-group-mark-details">
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
                    {StoreService.students.map(student => (
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

    private submitAsync = async () => {

        if (!this._validator.validate()) {
            this.forceUpdate();
            return;
        }

        const { marks, ...rest } = this.state.data;
        const res = await server.postAsync<ResponseJson>("GroupMark", {}, {
            ...rest,
            marks: Object.keys(marks).map(id => ({
                studentId: id,
                mark: marks[id]
            }))
        });

        if (res.success) {
            this.setState({
                data: {
                    lessonId: this.state.data.lessonId,
                    description: "",
                    marks: {}
                }
            });
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