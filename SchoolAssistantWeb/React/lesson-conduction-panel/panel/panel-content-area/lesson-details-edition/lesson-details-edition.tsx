import React from "react";
import { TextInput } from "../../../../shared/form-controls";
import ModCompBase from "../../../../shared/form-controls/mod-comp-base";
import StoreAndSaveService from "../../../services/store-and-save-service";
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
                topic: StoreAndSaveService.topic
            }
        }
    }

    render() {
        return (
            <form onSubmit={this.submit}>

                <TextInput
                    label="Temat zajęć"
                    name="topic-input"
                    onChange={this.changeTopic}
                    errorMessages={this._validator.getErrorMsgsFor('topic')}
                    value={this.state.data.topic}
                    placeholder="Temat zajęć"
                />

            </form>
        )
    }

    private submit: React.FormEventHandler<HTMLFormElement> = async (ev) => {

    }

    private changeTopic = (value: string) => this.setStateFnData(data => data.topic = value);
}