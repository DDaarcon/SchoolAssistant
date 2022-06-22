import React from "react";
import { Select, Option, OnChangeIdHandler, SubmitButton } from "../../../shared/form-controls";
import ModCompBase from "../../../shared/form-controls/mod-comp-base";
import LessonModificationData from "../../interfaces/lesson-modification-data";
import dataService from "../../schedule-data-service";



type LessonPrefabModCompProps = LessonModificationData & {
    submit: (info: LessonModificationData) => void;
}
type LessonPrefabModCompState = {
    data: LessonModificationData;
}
export default class LessonPrefabModComp extends ModCompBase<LessonModificationData, LessonPrefabModCompProps, LessonPrefabModCompState> {

    constructor(props) {
        super(props);

        this.state = {
            data: {
                subjectId: this.props.subjectId,
                teacherId: this.props.teacherId,
                roomId: this.props.roomId
            }
        }

        this._validator.setRules({
            subjectId: { notNull: true },
            teacherId: { notNull: true },
            roomId: { notNull: true }
        })
    }

    createOnSelectChangeHandler: (property: keyof LessonModificationData) => OnChangeIdHandler<number> = (property) =>
        (value) => {
            this.setStateFnData(data => data[property] = value as number);
        }

    changeSubject: OnChangeIdHandler<number> = value => {
        this.setStateFnData(data => {
            if (value != data.subjectId)
                data.teacherId = undefined;
            data.subjectId = value as number;
        })
    }


    onSubmit: React.FormEventHandler<HTMLFormElement> = (e) => {
        e.preventDefault();

        if (!this._validator.validate()) {
            console.log(this._validator.errors);
            this.forceUpdate();
            return;
        }

        this.props.submit(this.state.data);
    }

    render() {
        return (
            <form onSubmit={this.onSubmit}>
                <Select
                    label="Przedmiot"
                    name="subject-input"
                    value={this.state.data.subjectId}
                    onChangeId={this.changeSubject}
                    errorMessages={this._validator.errors.filter(x => x.on == 'subjectId').map(x => x.error)}
                    options={dataService.subjects.map(x => ({
                        label: x.name,
                        value: x.id
                    }))}
                />

                <Select
                    label="Nauczyciel"
                    name="teacher-input"
                    value={this.state.data.teacherId}
                    onChangeId={this.createOnSelectChangeHandler('teacherId')}
                    errorMessages={this._validator.errors.filter(x => x.on == 'teacherId').map(x => x.error)}
                    options={dataService.getTeachersBySubject(this.state.data.subjectId).map(x => ({
                        label: x.name,
                        value: x.id
                    })) }
                />

                <Select
                    label="Pomieszczenie"
                    name="room-input"
                    value={this.state.data.roomId}
                    onChangeId={this.createOnSelectChangeHandler('roomId')}
                    errorMessages={this._validator.errors.filter(x => x.on == 'roomId').map(x => x.error)}
                    options={dataService.rooms.map(x => ({
                        label: x.name,
                        value: x.id
                    })) }
                />

                <SubmitButton
                    value="Zapisz"
                />
            </form>
        )
    }
}