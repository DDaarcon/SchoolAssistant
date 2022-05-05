/**
 *  This file contains:
 *  
 *  ScheduleLessonPrefabTile - prefab template for selector component.
 *  
 *  ScheduleAddPrefabTile - button for opening modal, where prefabs are composed.
 * 
 *  ScheduleLessonModificationComponent - component injected into modal, here prefabs are constructed.
 *      
 * 
 * */


type ScheduleLessonPrefabTileProps = {
    data: ScheduleLessonPrefab;
}
type ScheduleLessonPrefabTileState = {

}
class ScheduleLessonPrefabTile extends React.Component<ScheduleLessonPrefabTileProps, ScheduleLessonPrefabTileState> {

    onStart: React.DragEventHandler<HTMLDivElement> = (event) => {
        event.dataTransfer.setData("prefab", JSON.stringify(this.props.data));
        dispatchEvent(new CustomEvent('dragBegan', {
            detail: this.props.data
        }));
    }

    onEnd: React.DragEventHandler<HTMLDivElement> = (event) => {
        dispatchEvent(new Event("hideLessonShadow"));
    }

    render() {
        return (
            <div className="sa-lesson-prefab"
                draggable
                onDragStart={this.onStart}
                onDragEnd={this.onEnd}
            >
                <span className="sa-lesson-prefab-subject">
                    {this.props.data.subject.name}
                </span>
                <div className="sa-lesson-prefab-bottom">
                    <div className="sa-lesson-prefab-lecturer">
                        {this.props.data.lecturer.name}
                    </div>
                    <div className="sa-lesson-prefab-room">
                        {this.props.data.room?.name}
                    </div>
                </div>
            </div>
        )
    }
}





type ScheduleAddPrefabTileProps = {
    onClick: () => void;
}
const ScheduleAddPrefabTile = (props: ScheduleAddPrefabTileProps) => {
    return (
        <div className="sa-add-lesson-prefab"
            onClick={props.onClick}
        >
            <i className="fa-solid fa-plus"></i>
        </div>
    )
}






interface ScheduleTeacherOptionEntry extends IdName {
    mainSubject: boolean;
}

type ScheduleLessonModificationComponentProps = ScheduleLessonModificationData & {
    submit: (info: ScheduleLessonModificationData) => void;
}
type ScheduleLessonModificationComponentState = ScheduleLessonModificationData & { }
class ScheduleLessonModificationComponent extends React.Component<ScheduleLessonModificationComponentProps, ScheduleLessonModificationComponentState> {
    private _validator = new Validator<ScheduleLessonModificationData>();

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

    createOnSelectChangeHandler: (property: keyof ScheduleLessonModificationComponentState) => React.ChangeEventHandler<HTMLSelectElement> = (property) =>
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
                <FormSelect
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

                <FormSelect
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

                <FormSelect
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