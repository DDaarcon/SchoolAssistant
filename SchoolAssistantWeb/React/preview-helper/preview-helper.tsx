import React from "react";
import { enumAssignSwitch } from "../shared/enum-help";
import { ModalSpace } from "../shared/modals";
import DefaultMenu from "./components/default-menu";
import FloatingPin from "./components/floating-pin";
import LoginMenu from "./components/login-menu";
import CreateUserExplanation from "./components/page-explanations/create-user-explanation";
import DataManagementExplanation from "./components/page-explanations/data-management-explanation";
import IndexExplanation from "./components/page-explanations/index-explanation";
import ScheduleArrangerExplanation from "./components/page-explanations/schedule-arranger-explanation";
import ScheduledLessonsExplanation from "./components/page-explanations/scheduled-lessons-explanation";
import StudentDashboardExplanation from "./components/page-explanations/student-dashboard-explanation";
import TeacherDashboardExplanation from "./components/page-explanations/teacher-dashboard-explanation";
import UserManagementExplanation from "./components/page-explanations/user-management-explanation";
import PreviewMenuType from "./enums/preview-menu-type";
import PreviewLoginsModel from "./interfaces/preview-logins-model";
import './preview-helper.css';

type PreviewHelperProps = {
    type: PreviewMenuType;
    logins?: PreviewLoginsModel;
}
type PreviewHelperState = {
    hidden: boolean;
}

export default class PreviewHelper extends React.Component<PreviewHelperProps, PreviewHelperState> {

    constructor(props) {
        super(props);

        this.state = {
            hidden: true
        }
    }

    render() {
        return (
            <div className={`preview-helper ${this.state.hidden ? 'ph-hidden' : ''}`}>
                <FloatingPin
                    textOnHover={this.getTextOnHoverForPin()}
                    onClick={this.toggleVisibility}
                    attentionGrabbing={this.state.hidden}
                />

                {this.renderMenu()}

                <ModalSpace />
            </div>
        )
    }

    private toggleVisibility = () => {
        this.setState({ hidden: !this.state.hidden });
    }

    private renderMenu() {
        return enumAssignSwitch<JSX.Element, typeof PreviewMenuType>(PreviewMenuType, this.props?.type, {
            LoginMenu: () => this.props.logins != undefined
                ? <LoginMenu
                    logins={this.props.logins}
                />
                : <DefaultMenu />,
            IndexPage:
                <DefaultMenu>
                    <IndexExplanation />
                </DefaultMenu>,
            TeacherDashboard:
                <DefaultMenu>
                    <TeacherDashboardExplanation />
                </DefaultMenu>,
            StudentDashboard:
                <DefaultMenu>
                    <StudentDashboardExplanation />
                </DefaultMenu>,
            ScheduledLessonsPage:
                <DefaultMenu>
                    <ScheduledLessonsExplanation />
                </DefaultMenu>,
            DataManagementPage:
                <DefaultMenu>
                    <DataManagementExplanation />
                </DefaultMenu>,
            ScheduleArrangerPage:
                <DefaultMenu>
                    <ScheduleArrangerExplanation />
                </DefaultMenu>,
            UserManagementPage:
                <DefaultMenu>
                    <UserManagementExplanation />
                </DefaultMenu>,
            CreateUserPage:
                <DefaultMenu>
                    <CreateUserExplanation />
                </DefaultMenu>,
            _: <DefaultMenu />
        })
    }

    private getTextOnHoverForPin() {
        return enumAssignSwitch<string, typeof PreviewMenuType>(PreviewMenuType, this.props?.type, {
            LoginMenu: "Dane logowania",
            _: "Ustaw podglądowe dane"
        })
    }
}