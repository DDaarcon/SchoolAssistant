import React from "react";
import { ModalSpace } from "../shared/modals";
import ServerConnection from "../shared/server-connection";
import dataService from './schedule-data-service';
import ScheduleClassSelectorPage from './class-selector';
import ScheduleArrangerConfig from "./interfaces/page-model-to-react/schedule-arranger-config";
import ScheduleClassSelectorEntry from "./interfaces/page-model-to-react/schedule-class-selector-entry";
import ScheduleSubjectEntry from "./interfaces/page-model-to-react/schedule-subject-entry";
import ScheduleTeacherEntry from "./interfaces/page-model-to-react/schedule-teacher-entry";
import ScheduleRoomEntry from "./interfaces/page-model-to-react/schedule-room-entry";

export let scheduleArrangerConfig: ScheduleArrangerConfig;
export let scheduleChangePageScreen: (pageComponent: JSX.Element) => void;
export const server = new ServerConnection("/ScheduleArranger");




type MainScreenProps = {
    config: ScheduleArrangerConfig;
    classes: ScheduleClassSelectorEntry[];
    subjects: ScheduleSubjectEntry[];
    teachers: ScheduleTeacherEntry[];
    rooms: ScheduleRoomEntry[];
    classId?: number;
}
type MainScreenState = {
    pageComponent: JSX.Element;
}
export default class MainScreen extends React.Component<MainScreenProps, MainScreenState> {
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

        dataService.classes = this.props.classes;
        dataService.subjects = this.props.subjects;
        dataService.teachers = this.props.teachers;
        dataService.rooms = this.props.rooms;
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