import React from "react"
import DayOfWeek from "../../../../schedule-shared/enums/day-of-week";
import { displayMinutes } from "../../../../schedule-shared/help/time-functions";
import { nameForDayOfWeek } from "../../../../schedule-shared/help/weekdays-function";
import Lesson from "../../../../schedule-shared/interfaces/lesson";
import LessonTimelineEntry from "../../../../schedule-shared/interfaces/lesson-timeline-entry";
import Time from "../../../../schedule-shared/interfaces/shared/time";
import { getEnumValues } from "../../../../shared/enum-help";
import { Select, Option, OnChangeIdHandler, Input, OnChangeHandler } from "../../../../shared/form-controls";
import ModCompBase, { ModifyMethod } from "../../../../shared/form-controls/mod-comp-base";
import { CommonModalProps } from "../../../../shared/modals/shared-modal-body";
import { ResponseJson } from "../../../../shared/server-connection";
import { scheduleArrangerConfig, server } from "../../../main";
import dataService from "../../../schedule-data-service";
import LessonEditModel from "./../interfaces/lesson-edit-model";
import './lesson-mod-comp.css';
import OverlappingLessonPad from "./overlapping-lesson-pad";


type LecturerOption = Option<number> & {
    isMainTeacher: boolean;
}

type LessonModCompProps = CommonModalProps & {
    day: DayOfWeek;
    lesson: LessonTimelineEntry;
    editStoredLesson: (model: LessonEditModel) => void;
}
type LessonModCompState = {
    data: LessonEditModel;
    defaultDuration: boolean;
    overlappingLessons?: Lesson[];
}
export default class LessonModComp extends ModCompBase<LessonEditModel, LessonModCompProps & CommonModalProps, LessonModCompState> {
    private _dayOptions: Option<number>[];

    private get _errorMessage() {
        return this.state.overlappingLessons?.length
            ? "Zajęcia kolidują z innymi"
            : this._validator.errors.length
                ? "Błędy w formularzu"
                : undefined;
    }

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
                classId: scheduleArrangerConfig.classId
            },
            defaultDuration: this.props.lesson.customDuration == undefined
        }

        this._validator.setRules({
            day: { notNull: true },
            time: {
                notNull: true, other: (model, prop) => {
                    return this.validateTime(model[prop])
                        ? undefined
                        : {
                            error: `Termin nie mieści się w ustalonym zakresie (${scheduleArrangerConfig.startHour}:00 - ${scheduleArrangerConfig.endHour}:00)`,
                            on: prop
                        }
                }
            },
            subjectId: { notNull: true },
            lecturerId: { notNull: true },
            roomId: { notNull: true }
        });

        this._dayOptions = getEnumValues(DayOfWeek).map(x => ({
            value: x,
            label: nameForDayOfWeek(x)
        }))
    }

    changeTimeAsync: React.ChangeEventHandler<HTMLInputElement> = async (event) => {
        const value = event.target.value;

        await this.refreshAsync(state => state.data.time = this.timeFromInput(value));
    }

    changeCustomDuration: React.ChangeEventHandler<HTMLInputElement> = async (event) => {
        const value = event.target.value;

        await this.refreshAsync(state => state.data.customDuration = parseInt(value));
    }

    changeUseDefaultDuration: React.ChangeEventHandler<HTMLInputElement> = async event => {
        const value = event.target.checked;

        await this.refreshAsync(state => state.defaultDuration = value);
    }

    changeDay: OnChangeIdHandler<number> = async value => {
        if (value instanceof Array) return;

        if (this.validateDay(value))
            await this.refreshAsync(state => state.data.day = value);
    }

    changeSubject: OnChangeIdHandler<number> = async (value) => {
        await this.refreshAsync(state => {
            if (value != state.data.subjectId)
                state.data.lecturerId = undefined;
            state.data.subjectId = value as number;
        })
    }

    changeLecturer: OnChangeHandler<LecturerOption> = async (value) => {
        if (value instanceof Array)
            value = value[0];

        const id = value.value;
        const isMainTeacher = value.isMainTeacher;
        await this.refreshAsync(state => state.data.lecturerId = id,
            () => {
                if (!isMainTeacher)
                    this._validator.addWarning('lecturerId', "Wybrany nauczyciel należy do dodatkowych z danego przedmiotu");
            });
    }

    changeRoom: OnChangeIdHandler<number> = async (value) => {
        await this.refreshAsync(state => state.data.roomId = value as number);
    }

    submitAsync: React.FormEventHandler<HTMLFormElement> = async (e) => {
        e.preventDefault();

        if (!this._validator.validate()) {
            this.forceUpdate();
            return;
        }

        const response = await server.postAsync<ResponseJson>("LessonModification", {}, this.state.data);

        if (response.success) {
            this.props.editStoredLesson(this.state.data);
            this.props.assignedAtPresenter.close();
        }
    }

    removeLesson = () => {
        dataService.removeLessonAndGetResultAsync(this.state.data.id)
            .then(result => {
                if (result)
                    this.props.assignedAtPresenter.close();
            });
    }

    render() {
        return (
            <form onSubmit={this.submitAsync}>
                <div className="lesson-mod-comp-layout">
                    <div className="lmc-inputs">

                        <Input
                            label="Godzina rozpoczęcia"
                            name="time-input"
                            value={this.timeToInput(this.state.data.time)}
                            errorMessages={this._validator.getErrorMsgsFor('time')}
                            onChange={this.changeTimeAsync}
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
                            onChangeId={this.changeSubject}
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
                            onChange={this.changeLecturer}
                            options={dataService.getTeachersBySubject(this.state.data.subjectId).map<LecturerOption>(x => ({
                                label: x.name,
                                value: x.id,
                                isMainTeacher: x.isMainTeacher
                            }))}
                            errorMessages={this._validator.getErrorMsgsFor('lecturerId')}
                            warningMessages={this._validator.getWarningMsgsFor('lecturerId')}
                            optionStyle={(props) => ({
                                backgroundColor: props.isSelected
                                    ? '#c9fbff'
                                    : props.data.isMainTeacher
                                        ? '#ffffff'
                                        : '#fffa62',
                                color: '#000000'
                            })}
                        />

                        <Select
                            label="Pomieszczenie"
                            name="room-input"
                            value={this.state.data.roomId}
                            onChangeId={this.changeRoom}
                            options={dataService.rooms.map(x => ({
                                label: x.name,
                                value: x.id
                            }))}
                            errorMessages={this._validator.getErrorMsgsFor('roomId')}
                        />

                    </div>
                    <div className="lmc-right-panel">
                        <h4>Kolidujące zajęcia</h4>
                        <div className="lmc-overlap-container">
                            {this.state.overlappingLessons?.map(lesson => (
                                <OverlappingLessonPad
                                    key={lesson.id}
                                    lesson={lesson}
                                    refreshAsync={() => this.refreshAsync()}
                                />
                            ))}
                        </div>

                        <div className="lmc-right-bottom">
                            <button
                                className="lmc-save-btn"
                                disabled={this._errorMessage != undefined}
                            >
                                {this._errorMessage ?? "Zapisz"}
                            </button>
                            <button
                                type="button"
                                className="lmc-remove-btn"
                                onClick={this.removeLesson}
                            >
                                Usuń zajęcia
                            </button>
                        </div>
                    </div>
                </div>
            </form>
        )
    }

    private validateTime(time: Time) {
        const minutes = time.hour * 60 + time.minutes;

        return !(time.hour < scheduleArrangerConfig.startHour
            || minutes > scheduleArrangerConfig.endHour * 60);
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


    private async refreshAsync(modifyStateMethod?: ModifyMethod<LessonModCompState>, beforeRerender?: () => void) {
        const newState = { ...this.state };
        modifyStateMethod?.(newState);

        let lessons: Lesson[];
        if (this._validator.validate()) {
            lessons = await dataService.getOverlappingLessonsAsync({
                day: newState.data.day,
                time: newState.data.time,
                customDuration: newState.defaultDuration ? undefined : newState.data.customDuration,
                teacherId: newState.data.lecturerId,
                roomId: newState.data.roomId
            }, this.state.data.id);
        }
        else
            lessons = [];

        beforeRerender?.();

        this.setStateFn(state => state.overlappingLessons = lessons, modifyStateMethod);
    }
}