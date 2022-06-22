import React from "react";
import { enumAssignSwitch } from "../../shared/enum-help";
import StoreAndSaveService from "../services/store-and-save-service";
import TogglePanelService from "../services/toggle-panel-service";
import Anchor from "./components/anchor";
import Clock from "./components/clock";
import Controls from "./components/controls";
import LessonCondPanelContent from "./enums/lesson-cond-panel-content";
import AttendanceEdition from "./panel-content-area/attendance-edition/attendance-edition";
import LessonDetailsEdition from "./panel-content-area/lesson-details-edition/lesson-details-edition";
import PanelContentArea from "./panel-content-area/panel-content-area";
import './panel.css';

type PanelProps = {

}
type PanelState = {
    show: boolean;
    contentType: LessonCondPanelContent;
}

export default class Panel extends React.Component<PanelProps, PanelState> {

    constructor(props) {
        super(props);

        this.state = {
            show: false,
            contentType: LessonCondPanelContent.LessonDetailsEdit
        }

        TogglePanelService.registerPanel(this);
    }

    public show() {
        this.setState({ show: true });
    }

    public hide() {
        this.setState({ show: false });
    }

    public get isShown() { return this.state.show; }

    render() {
        return (
            <div className={"lcp-panel-container " + (this.isShown ? "" : "lcp-panel-container-hide")}>

                <div className="lcp-panel">

                    <div className="lcp-panel-top">

                        <Clock
                            startTime={this._startTime}
                            duration={this._duration}
                        />

                        <Controls
                            goTo={this.changeContent}
                        />

                    </div>

                    <PanelContentArea>
                        {this.getContentPage()}
                    </PanelContentArea>

                </div>

                <Anchor />

            </div>
        )
    }

    private get _startTime() { return StoreAndSaveService.startTime; }
    private get _duration() { return StoreAndSaveService.duration; }

    private changeContent = (content: LessonCondPanelContent) => {
        if (this.state.contentType == content)
            return;

        this.setState({ contentType: content });
    }


    private _contentPages: { [index in keyof typeof LessonCondPanelContent]?: React.ReactNode } = {};

    private getContentPage() {
        if (!this._contentPages[this.state.contentType]) {

            this._contentPages[this.state.contentType] =
                enumAssignSwitch<React.ReactNode, typeof LessonCondPanelContent>(LessonCondPanelContent, this.state.contentType, {
                    LessonDetailsEdit: this.createLessonDetailsEdition,
                    AttendanceEdit: this.createAttendanceEdition,
                    GivingMark: this.createGivingMarkPage,
                    GivingGroupMark: this.createGivingGroupMarkPage,
                    _: <></>
                });
        }

        return this._contentPages[this.state.contentType];
    }

    private createLessonDetailsEdition = () => <LessonDetailsEdition />;
    private createAttendanceEdition = () => <AttendanceEdition />;
    private createGivingMarkPage = () => undefined;
    private createGivingGroupMarkPage = () => undefined;
}