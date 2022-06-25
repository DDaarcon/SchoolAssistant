import React from "react";
import server from "../../../../scheduled-lessons-list/server";
import { TextInput } from "../../../../shared/form-controls";
import ModCompBase from "../../../../shared/form-controls/mod-comp-base";
import { ResponseJson } from "../../../../shared/server-connection";
import SaveButtonService from "../../../services/save-button-service";
import StoreService from "../../../services/store-service";
import LessonDetailsEditModel from "./lesson-details-edit-model";

type LessonDetailsEditionProps = {}
type LessonDetailsEditionState = {
    data: LessonDetailsEditModel;
}

export default class LessonDetailsEdition extends ModCompBase<LessonDetailsEditModel, LessonDetailsEditionProps, LessonDetailsEditionState> {

    constructor(props) {
        super(props);

        const data = StoreService.lessonDetailsSvc.model;
        this.state = {
            data
        }

        this._validator.setRules({
            topic: {
                notNull: true, notEmpty: 'Należy wprowadzić temat zajęć'
            }
        });

        SaveButtonService.setAction(this.submitAsync);
    }

    render() {
        return (
            <form>

                <TextInput
                    label="Temat zajęć"
                    name="topic-input"
                    onChange={this.changeTopic}
                    errorMessages={this._validator.getErrorMsgsFor('topic')}
                    value={this.state.data.topic}
                />

            </form>
        )
    }

    componentWillUpdate() {
        this.preRenderOperations();
    }

    private submitAsync = async () => {

        if (!this._validator.validate()) {
            this.forceUpdate();
            return;
        }

        const res = await server.postAsync<ResponseJson>("LessonDetails", {}, this.state.data);

        // TODO: handle server errors
        if (res.success) {
            StoreService.lessonDetailsSvc.applyToMain();
            SaveButtonService.hide();
        }
        else
            console.debug(res.message);
    }

    private changeTopic = (value: string) => {
        this.setStateFnData(data => data.topic = value);
    }

    private preRenderOperations() {
        StoreService.lessonDetailsSvc.update(this.state.data);

        SaveButtonService.change(StoreService.lessonDetailsSvc.anyChangesToMainModel());
    }
}