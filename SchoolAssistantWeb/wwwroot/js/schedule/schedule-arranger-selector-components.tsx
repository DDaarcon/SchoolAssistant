type ScheduleLessonPrefabTileProps = {
    data: ScheduleLessonPrefab;
}
type ScheduleLessonPrefabTileState = {

}
class ScheduleLessonPrefabTile extends React.Component<ScheduleLessonPrefabTileProps, ScheduleLessonPrefabTileState> {

    onStart: React.DragEventHandler<HTMLDivElement> = (event) => {
        event.dataTransfer.setData("prefab", JSON.stringify(this.props.data));
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

        </div>
    )
}





interface ScheduleLessonModificationData {
    subjectId?: number;
    teacherId?: number;
    roomId?: number;
}

interface ScheduleTeacherOptionEntry extends IdName {
    mainSubject: boolean;
}

type ScheduleLessonModificationComponentProps = ScheduleLessonModificationData & {
    submit: (info: ScheduleLessonModificationData) => void;
}
type ScheduleLessonModificationComponentState = ScheduleLessonModificationData & { }
class ScheduleLessonModificationComponent extends React.Component<ScheduleLessonModificationComponentProps, ScheduleLessonModificationComponentState> {

    private get _subjectFilteredTeachers(): ScheduleTeacherOptionEntry[] {
        return [
            ...scheduleDataService.teachers
                .filter(x => x.mainSubjectIds.includes(this.state.subjectId))
                .map(x => ({ id: x.id, name: x.name, mainSubject: true })),
            ...scheduleDataService.teachers
                .filter(x => x.additionalSubjectIds.includes(this.state.subjectId))
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

    }

    render() {
        return (
            <form onSubmit={this.onSubmit}>
                <div className="form-group">
                    <label htmlFor="subject-input">Przedmiot</label>
                    <select
                        className="form-select"
                        name="subject-input"
                        value={this.state.subjectId}
                        onChange={this.createOnSelectChangeHandler('subjectId')}
                    >
                        <option value="">Wybierz</option>
                        {scheduleDataService.subjects.map(x =>
                            <option key={x.id}
                                value={x.id}
                            >
                                {x.name}
                            </option>
                        )}
                    </select>
                </div>
                
                <div className="form-group">
                    <label htmlFor="teacher-input">Nauczyciel</label>
                    <select
                        className="form-select"
                        name="teacher-input"
                        value={this.state.teacherId}
                        onChange={this.createOnSelectChangeHandler('teacherId')}
                    >
                        <option value="">Wybierz</option>
                        {this._subjectFilteredTeachers.map(x =>
                            <option className={x.mainSubject ? "sa-lesson-modify-teacher-main" : "sa-lesson-modify-teacher-addit"}
                                key={x.id}
                                value={x.id}
                            >
                                {x.name}
                            </option>
                        )}
                    </select>
                </div>
                
                <div className="form-group">
                    <label htmlFor="room-input">Pomieszczenie</label>
                    <select
                        className="form-select"
                        name="room-input"
                        value={this.state.roomId}
                        onChange={this.createOnSelectChangeHandler('roomId')}
                    >
                        <option value="">Wybierz</option>
                        {scheduleDataService.rooms.map(x =>
                            <option key={x.id}
                                value={x.id}
                            >
                                {x.name}
                            </option>
                        )}
                    </select>
                </div>
            </form>
        )
    }
}