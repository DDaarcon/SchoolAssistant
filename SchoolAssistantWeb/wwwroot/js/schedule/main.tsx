interface ScheduleArrangerConfig {
    defaultLessonDuration: number;
    startHour: number;
    endHour: number;

    cellDuration: number;
    cellHeight: number;

    classId?: number;
}

interface ScheduleClassSelectorEntry {
    id: number;
    name: string;
    specialization?: string;
}

let scheduleArrangerConfig: ScheduleArrangerConfig;
let scheduleChangePageScreen: (pageComponent: JSX.Element) => void;
const scheduleServer = new ServerConnection("/ScheduleArranger");




type ScheduleArrangerMainScreenProps = {
    config: ScheduleArrangerConfig;
    classes: ScheduleClassSelectorEntry[];
    subjects: ScheduleSubjectEntry[];
    teachers: ScheduleTeacherEntry[];
    rooms: ScheduleRoomEntry[];
    classId?: number;
}
type ScheduleArrangerMainScreenState = {
    pageComponent: JSX.Element;
}
class ScheduleArrangerMainScreen extends React.Component<ScheduleArrangerMainScreenProps, ScheduleArrangerMainScreenState> {
    private _classSelectorComponent: JSX.Element;

    constructor(props) {
        super(props);

        this._classSelectorComponent =
            <ScheduleClassSelectorPage
                entries={this.props.classes}
            />;

        this.state = {
            pageComponent: this._classSelectorComponent
        }

        scheduleArrangerConfig = this.props.config;
        scheduleChangePageScreen = this.changeScreen;

        scheduleDataService.subjects = this.props.subjects;
        scheduleDataService.teachers = this.props.teachers;
        scheduleDataService.rooms = this.props.rooms;
    }

    changeScreen = (pageComponent: JSX.Element) => {
        this.setState({ pageComponent });
    }

    render() {
        return (
            <div className="schedule-arranger-main">

                <div className="sa-page-content">
                    {this.state.pageComponent}
                </div>

                <ModalSpace />
            </div>
        )
    }
}




interface ScheduleLessonPrefab {
    subject: IdName;
    lecturer: IdName;
    room?: IdName;
}

interface ScheduleSubjectEntry extends IdName { }
interface ScheduleTeacherEntry extends IdName {
    shortName: string;
    mainSubjectIds: number[];
    additionalSubjectIds: number[];
    lessons: PeriodicLessonTimetableEntry[];
}
interface ScheduleRoomEntry extends IdName {
    floor: number;
    lessons: PeriodicLessonTimetableEntry[];
}

class ScheduleArrangerDataService {
    prefabs: ScheduleLessonPrefab[] = [];

    subjects?: ScheduleSubjectEntry[];
    teachers?: ScheduleTeacherEntry[];
    rooms?: ScheduleRoomEntry[];

    addPrefab(prefab: ScheduleLessonPrefab) {
        this.prefabs.push(prefab);

        dispatchEvent(new CustomEvent('newPrefab', {
            detail: prefab
        }));
    }

    getSubjectName = (id: number) => this.subjects.find(x => x.id == id).name;
    getTeacherName = (id: number) => this.teachers.find(x => x.id == id).shortName;
    getRoomName = (id: number) => this.rooms.find(x => x.id == id).name;

}
const scheduleDataService = new ScheduleArrangerDataService;