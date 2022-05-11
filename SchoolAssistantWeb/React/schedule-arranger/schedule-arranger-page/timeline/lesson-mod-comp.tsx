import React from "react"
import { Select } from "../../../shared/form-controls";
import { CommonModalProps } from "../../../shared/modals/shared-modal-body"
import Validator from "../../../shared/validator";
import { LessonTimelineEntry } from "../../interfaces/lesson-timeline-entry";

type LessonModCompProps = CommonModalProps & {
    lesson: LessonTimelineEntry;
}
type LessonModCompState = {
    data: LessonTimelineEntry;
}
export default class LessonModComp extends React.Component<LessonModCompProps & CommonModalProps, LessonModCompState> {
    private _validator = new Validator<LessonTimelineEntry>();

    constructor(props) {
        super(props);

        this.state = {
            data: this.props.lesson
        }

        this._validator.forModelGetter(() => this.state.data);
        this._validator.setRules({
            lecturer: {
                notNull: true, subValidator: (getModel, prop) => ({
                    id: { other: () => undefined }
                })
            }
        });


    }



    submitAsync: React.FormEventHandler<HTMLFormElement> = async (e) => {
        e.preventDefault();


    }


    render() {
        return (
            <form onSubmit={ }>

                <Select
                    label="Przedmiot"
                    name="subject-input"
                    value={this.state.data.subject.id}
                    onChange={ }
                />

            </form>
        )
    }
}