import React from "react";
import server from "../../../../scheduled-lessons-list/server";
import { SubmitButton, TextInput } from "../../../../shared/form-controls";
import ModCompBase from "../../../../shared/form-controls/mod-comp-base";
import { ResponseJson } from "../../../../shared/server-connection";
import StoreService from "../../../services/store-service";
import LessonDetailsEditModel from "./lesson-details-edit-model";

type LessonDetailsEditionProps = {}
type LessonDetailsEditionState = {
    data: LessonDetailsEditModel;
}

export default class LessonDetailsEdition extends ModCompBase<LessonDetailsEditModel, LessonDetailsEditionProps, LessonDetailsEditionState> {

    constructor(props) {
        super(props);

        this.state = {
            data: {
                id: StoreService.lessonId,
                topic: StoreService.topic ?? ""
            }
        }

        this._validator.setRules({
            topic: {
                notNull: true, notEmpty: 'Należy wprowadzić temat zajęć'
            }
        });
    }

    render() {
        return (
            <form onSubmit={this.submitAsync}>

                <TextInput
                    label="Temat zajęć"
                    name="topic-input"
                    onChange={this.changeTopic}
                    errorMessages={this._validator.getErrorMsgsFor('topic')}
                    value={this.state.data.topic}
                />

                <SubmitButton
                    value="Zapisz"
                />

            </form>
        )
    }

    private submitAsync: React.FormEventHandler<HTMLFormElement> = async (ev) => {
        ev.preventDefault();

        if (!this._validator.validate()) {
            this.forceUpdate();
            return;
        }

        const res = await server.postAsync<ResponseJson>("LessonDetails", {}, this.state.data);

        // TODO: handle server errors
        if (res.success)
            StoreService.updateDetails(this.state.data);
        else
            console.debug(res.message);

    }

    private changeTopic = (value: string) => this.setStateFnData(data => data.topic = value);
}