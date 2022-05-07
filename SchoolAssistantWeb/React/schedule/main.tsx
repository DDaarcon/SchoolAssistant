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