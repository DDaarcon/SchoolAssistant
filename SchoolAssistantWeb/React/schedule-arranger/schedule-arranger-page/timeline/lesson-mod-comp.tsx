import React from "react"
import { getEnumNames, getEnumValues } from "../../../shared/enum-help";
import { Select, Option, OnChangeIdHandler, Input } from "../../../shared/form-controls";
import ModCompBase from "../../../shared/form-controls/mod-comp-base";
import { CommonModalProps } from "../../../shared/modals/shared-modal-body"
import Validator from "../../../shared/validator";
import { DayOfWeek } from "../../enums/day-of-week";
import { displayMinutes, nameForDayOfWeek } from "../../help-functions";
import { Lesson } from "../../interfaces/lesson";
import { LessonTimelineEntry } from "../../interfaces/lesson-timeline-entry";
import { Time } from "../../interfaces/shared";
import { scheduleArrangerConfig } from "../../main";
import dataService from "../../schedule-data-service";
import LessonEditModel from "./interfaces/lesson-edit-model";

type LessonModCompProps = CommonModalProps & {
    day: DayOfWeek;
    lesson: LessonTimelineEntry;
}
type LessonModCompState = {
    data: LessonEditModel;
    defaultDuration: boolean;
    overlappingLessons?: Lesson[];
}
export default class LessonModComp extends ModCompBase<LessonEditModel, LessonModCompProps & CommonModalProps, LessonModCompState> {

    private _dayOptions: Option<number>[];

    constructor(props) {
        super(props);

        this.state = {
            data: {
                id: this.props.lesson.id,
                day: this.props.day,
                time: this.props.lesson.time,
                customDuration: this.props.lesson.customDuration,
                subjectId: this.props.lesson.subject.id,
                lecturerId: this.props.lesson.lecturer.id,
                roomId: this.props.lesson.room.id,
            },
            defaultDuration: this.props.lesson.customDuration == undefined
        }

        this._validator.setRules({
            subjectId: { notNull: true },
            lecturerId: { notNull: true },
            roomId: { notNull: true }
        });

        this._dayOptions = getEnumValues(DayOfWeek).map(x => ({
            value: x,
            label: nameForDayOfWeek(x)
        }))
    }

    changeTime: React.ChangeEventHandler<HTMLInputElement> = (event) => {
        const value = event.target.value;

        this.setStateFnData(data => data.time = this.timeFromInput(value));
    }

    changeCustomDuration: React.ChangeEventHandler<HTMLInputElement> = (event) => {
        const value = event.target.value;

        this.setStateFnData(data => data.customDuration = parseInt(value));
    }

    changeUseDefaultDuration: React.ChangeEventHandler<HTMLInputElement> = event => {
        const value = event.target.checked;

        this.setStateFn(state => state.defaultDuration = value);
    }

    changeDay: OnChangeIdHandler<number> = value => {
        if (value instanceof Array) return;

        if (this.validateDay(value))
            this.setStateFn(state => state.data.day = value);
    }

    createOnSelectChangeHandler: (property: 'subjectId' | 'lecturerId' | 'roomId') => OnChangeIdHandler<number> = (property) =>
        (value) => {
            this.setStateFnData(data => data[property] = value as number);
        }

    submitAsync: React.FormEventHandler<HTMLFormElement> = async (e) => {
        e.preventDefault();


    }


    render() {
        return (
            <form onSubmit={this.submitAsync}>

                <Input
                    label="Godzina rozpoczęcia"
                    name="time-input"
                    value={this.timeToInput(this.state.data.time)}
                    errorMessages={this._validator.getErrorMsgsFor('time')}
                    onChange={this.changeTime}
                    type="time"
                />

                <Select
                    label="Dzień"
                    name="day-input"
                    value={this.state.data.day}
                    errorMessages={this._validator.getErrorMsgsFor('day')}
                    onChangeId={this.changeDay}
                    options={this._dayOptions}
                />

                <Input
                    label="Czas trwania"
                    name="duration-input"
                    value={this.state.data.customDuration ?? scheduleArrangerConfig.defaultLessonDuration}
                    errorMessages={this._validator.getErrorMsgsFor('customDuration')}
                    onChange={this.changeCustomDuration}
                    disabled={this.state.defaultDuration}
                    type="number"
                />

                <Input
                    inputClassName="form-check-input"
                    label={`Domyślny czas trwania (${scheduleArrangerConfig.defaultLessonDuration} minut)`}
                    name="default-duration-input"
                    checked={this.state.defaultDuration}
                    onChange={this.changeUseDefaultDuration}
                    type="checkbox"
                />

                <Select
                    label="Przedmiot"
                    name="subject-input"
                    value={this.state.data.subjectId}
                    onChangeId={this.createOnSelectChangeHandler('subjectId')}
                    options={dataService.subjects.map(x => ({
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
                    options={dataService.teachers.map(x => ({
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
                    options={dataService.rooms.map(x => ({
                        label: x.name,
                        value: x.id
                    }))}
                    errorMessages={this._validator.getErrorMsgsFor('roomId')}
                />

            </form>
        )
    }


    private validateDay(day: number): boolean {
        if (!getEnumValues(DayOfWeek).includes(day))
            return false;

        return true;
    }

    private timeFromInput(display: string): Time {
        const numbers = display.split(':').map(x => parseInt(x));
        if (numbers.some(x => isNaN(x)))
            return { hour: 0, minutes: 0 }

        return {
            hour: numbers[0],
            minutes: numbers[1]
        }
    }

    private timeToInput(time: Time): string {
        return `${displayMinutes(time.hour)}:${displayMinutes(time.minutes)}`;
    }


    private getOverlappingLessons() {

}
}