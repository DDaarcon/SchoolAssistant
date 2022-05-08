import React from "react";
import { Select } from "../../../shared/form-controls";
import Validator from "../../../shared/validator";
import { LessonModificationData } from "../../interfaces/lesson-modification-data";
import { IdName } from "../../interfaces/shared";
import scheduleDataService from "../../schedule-data-service";



interface ScheduleTeacherOptionEntry extends IdName {
    mainSubject: boolean;
}


type LessonModificationComponentProps = LessonModificationData & {
    submit: (info: LessonModificationData) => void;
}
type LessonModificationComponentState = LessonModificationData & {}
export default class LessonModificationComponent extends React.Component<LessonModificationComponentProps, LessonModificationComponentState> {
    private _validator = new Validator<LessonModificationData>();

    private get _subjectFilteredTeachers(): ScheduleTeacherOptionEntry[] {
        return [
            ...scheduleDataService.teachers
                .filter(x => x.mainSubjectIds.includes(this.state.subjectId))
                .map(x => ({ id: x.id, name: x.name, mainSubject: true })),
            ...scheduleDataService.teachers
                .filter(x => !x.mainSubjectIds.includes(this.state.subjectId) && x.additionalSubjectIds.includes(this.state.subjectId))
                .map(x => ({ id: x.id, name: x.name, mainSubject: false }))
        ]
    }

    constructor(props) {
        super(props);

        this.state = {
            subjectId: this.props.subjectId,
            teacherId: this.props.teacherId,
            roomId: this.props.roomId
        }

        this._validator.forModelGetter(() => this.state);
        this._validator.setRules({
            subjectId: { notNull: true },
            teacherId: { notNull: true },
            roomId: { notNull: true }
        })
    }

    createOnSelectChangeHandler: (property: keyof LessonModificationComponentState) => React.ChangeEventHandler<HTMLSelectElement> = (property) =>
        (event) => {
            let value = event.target.value == ""
                ? undefined
                : parseInt(event.target.value);

            this.setState(prevState => {
                const state = { ...prevState };
                state[property] = value;
                return state;
            });
        }


    onSubmit: React.FormEventHandler<HTMLFormElement> = (e) => {
        e.preventDefault();

        if (!this._validator.validate()) {
            console.log(this._validator.errors);
            this.forceUpdate();
            return;
        }

        this.props.submit(this.state);
    }

    render() {
        return (
            <form onSubmit={this.onSubmit}>
                <Select
                    label="Przedmiot"
                    name="subject-input"
                    value={this.state.subjectId}
                    onChange={this.createOnSelectChangeHandler('subjectId')}
                    errorMessages={this._validator.errors.filter(x => x.on == 'subjectId').map(x => x.error)}
                    options={
                        <>
                            <option value="">Wybierz</option>
                            {scheduleDataService.subjects.map(x =>
                                <option key={x.id}
                                    value={x.id}
                                >
                                    {x.name}
                                </option>
                            )}
                        </>
                    }
                />

                <Select
                    label="Nauczyciel"
                    name="teacher-input"
                    value={this.state.teacherId}
                    onChange={this.createOnSelectChangeHandler('teacherId')}
                    errorMessages={this._validator.errors.filter(x => x.on == 'teacherId').map(x => x.error)}
                    options={
                        <>
                            <option value="">Wybierz</option>
                            {this._subjectFilteredTeachers.map(x =>
                                <option className={x.mainSubject ? "sa-lesson-modify-teacher-main" : "sa-lesson-modify-teacher-addit"}
                                    key={x.id}
                                    value={x.id}
                                >
                                    {x.name}
                                </option>
                            )}
                        </>
                    }
                />

                <Select
                    label="Pomieszczenie"
                    name="room-input"
                    value={this.state.roomId}
                    onChange={this.createOnSelectChangeHandler('roomId')}
                    errorMessages={this._validator.errors.filter(x => x.on == 'roomId').map(x => x.error)}
                    options={
                        <>
                            <option value="">Wybierz</option>
                            {scheduleDataService.rooms.map(x =>
                                <option key={x.id}
                                    value={x.id}
                                >
                                    {x.name}
                                </option>
                            )}
                        </>
                    }
                />

                <div className="form-group">
                    <input
                        type="submit"
                        value="Zapisz"
                        className="form-control"
                    />
                </div>
            </form>
        )
    }
}