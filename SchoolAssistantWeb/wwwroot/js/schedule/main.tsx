interface ScheduleArrangerConfig {
    defaultLessonDuration: number;
    startHour: number;
    endHour: number;

    cellDuration: number;
    cellHeight: number;
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
    classId?: number;
}
type ScheduleArrangerMainScreenState = {
    pageComponent: JSX.Element;
}
class ScheduleArrangerMainScreen extends React.Component<ScheduleArrangerMainScreenProps, ScheduleArrangerMainScreenState> {
    constructor(props) {
        super(props);

        this.state = {
            pageComponent: TempMainScreen()
        }

        scheduleArrangerConfig = this.props.config;
        scheduleChangePageScreen = this.changeScreen;
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

const TempMainScreen = () => {

    scheduleServer.getAsync<ScheduleClassLessons>("ClassLessons", { classId: 0 })
        .then((result) => {
            scheduleChangePageScreen(
                <ScheduleArrangerPage
                    classData={result}
                />
            );
        });

    return (
        <h1>Witaj w zarządzaniu planem lekcji</h1>
    )
}
