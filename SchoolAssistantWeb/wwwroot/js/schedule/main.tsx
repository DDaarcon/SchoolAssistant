interface ScheduleArrangerConfig {
    defaultLessonDuration: number;
    startHour: number;
    endHour: number;

    cellDuration: number;
    cellHeight: number;

    classId?: number;
}

let scheduleArrangerConfig: ScheduleArrangerConfig;
let schedulePageChangeScreen: (pageComponent: JSX.Element) => void;
const scheduleServer = new ServerConnection("/ScheduleArranger");




type ScheduleArrangerMainScreenProps = ScheduleArrangerConfig & {

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

        scheduleArrangerConfig = this.props;
        schedulePageChangeScreen = this.changeScreen;
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
            schedulePageChangeScreen(
                <ScheduleArrangerPage
                    classData={result}
                />
            );
        });

    return (
        <h1>Witaj w zarządzaniu planem lekcji</h1>
    )
}
