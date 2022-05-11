import React from "react"
import { Select, Option, OnChangeIdHandler } from "../../../shared/form-controls";
import { CommonModalProps } from "../../../shared/modals/shared-modal-body"
import Validator from "../../../shared/validator";
import { LessonTimelineEntry } from "../../interfaces/lesson-timeline-entry";
import scheduleDataService from "../../schedule-data-service";
import LessonEditModel from "./interfaces/lesson-edit-model";

type LessonModCompProps = CommonModalProps & {
    lesson: LessonTimelineEntry;
}
type LessonModCompState = {
    data: LessonEditModel;
}
export default class LessonModComp extends React.Component<LessonModCompProps & CommonModalProps, LessonModCompState> {
    private _validator = new Validator<LessonEditModel>();

    constructor(props) {
        super(props);

        this.state = {
            data: {
                id: this.props.lesson.id,
                time: this.props.lesson.time,
                customDuration: this.props.lesson.customDuration,
                subjectId: this.props.lesson.subject.id,
                lecturerId: this.props.lesson.lecturer.id,
                roomId: this.props.lesson.room.id,
            }
        }

        this._validator.forModelGetter(() => this.state.data);
        this._validator.setRules({
            subjectId: { notNull: true },
            lecturerId: { notNull: true },
            roomId: { notNull: true }
        });


    }

    createOnSelectChangeHandler: (property: 'subjectId' | 'lecturerId' | 'roomId') => OnChangeIdHandler<number> = (property) =>
        (value) => {
            this.setState(prevState => {
                const state = { ...prevState };
                state.data[property] = value as number;
                return state;
            });
        }

    submitAsync: React.FormEventHandler<HTMLFormElement> = async (e) => {
        e.preventDefault();


    }


    render() {
        return (
            <form onSubmit={this.submitAsync}>

                <Select
                    label="Przedmiot"
                    name="subject-input"
                    value={this.state.data.subjectId}
                    onChangeId={this.createOnSelectChangeHandler('subjectId')}
                    options={scheduleDataService.subjects.map(x => ({
                        label: x.name,
                        value: x.id
                    }))}
                    errorMessages={this._validator.getErrorMsgsFor('subjectId')}
                />

                <Select
                    label="Nauczyciel"
                    name="lecturer-input"
                    value={this.state.data.lecturerId}
                    onChangeId={this.createOnSelectChangeHandler('lecturerId')}
                    options={scheduleDataService.teachers.map(x => ({
                        label: x.name,
                        value: x.id
                    }))}
                    errorMessages={this._validator.getErrorMsgsFor('lecturerId')}
                />

                <Select
                    label="Pomieszczenie"
                    name="room-input"
                    value={this.state.data.roomId}
                    onChangeId={this.createOnSelectChangeHandler('roomId')}
                    options={scheduleDataService.rooms.map(x => ({
                        label: x.name,
                        value: x.id
                    }))}
                    errorMessages={this._validator.getErrorMsgsFor('roomId')}
                />

            </form>
        )
    }
}