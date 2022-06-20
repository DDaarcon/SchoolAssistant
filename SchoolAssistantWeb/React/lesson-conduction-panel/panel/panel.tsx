import React from "react";
import { enumSwitch } from "../../shared/enum-help";
import TogglePanelService from "../services/toggle-panel-service";
import Clock from "./components/clock";
import Controls from "./components/controls";
import LessonDetailsEdition from "./panel-content-area/lesson-details-edition/lesson-details-edition";
import PanelContentArea from "./panel-content-area/panel-content-area";
import './panel.css';

type PanelProps = {

}
type PanelState = {
    show: boolean;
    content: React.ReactNode;
}

export default class Panel extends React.Component<PanelProps, PanelState> {

    constructor(props) {
        super(props);

        this.state = {
            show: true,
            content: <></>
        }

        this._start = new Date();
        this._dur = 45;

        TogglePanelService.registerPanel(this);
    }

    public show() {
        this.setState({ show: true });
    }

    public hide() {
        this.setState({ show: false });
    }

    public get isShown() { return this.state.show; }

    private _start: Date;
    private _dur: number;

    render() {
        return (
            <div className={"lcp-panel-container " + (this.isShown ? "" : "lcp-panel-container-hide")}>

                <div className="lcp-panel">

                    <div className="lcp-panel-top">

                        <Clock
                            startTime={this._start}
                            duration={this._dur}
                        />

                        <Controls
                            goTo={this.changeContent}
                        />

                    </div>

                    <PanelContentArea>
                        {this.state.content}
                    </PanelContentArea>

                </div>

            </div>
        )
    }

    private _currentContent: LessonCondPanelContent;

    private changeContent = (content: LessonCondPanelContent) => {
        if (this._currentContent == content)
            return;

        enumSwitch(LessonCondPanelContent, content, {
            LessonDetailsEdit: () => this.setState({ content: this.getLessonDetailsEdition() }),
            AttendanceEdit: () => this.setState({ content:  }),
            GivingMark: () => this.setState({ content:  }),
            GivingGroupMark: () => this.setState({ content:  }),
        });
    }


    private _lessonDetailsEdition?: React.ReactNode;
    private getLessonDetailsEdition = () => {
        this._lessonDetailsEdition ??= <LessonDetailsEdition />
        return this._lessonDetailsEdition;
    }
}