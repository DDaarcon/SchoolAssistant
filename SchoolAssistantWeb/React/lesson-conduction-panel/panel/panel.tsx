import React from "react";
import TogglePanelService from "../services/toggle-panel-service";
import Clock from "./components/clock";
import Controls from "./components/controls";
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
            content: this.red()
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
                            goToAttendanceEdit={this.showRed}
                            goToGivingMark={this.showBlue}
                        />

                    </div>

                    <PanelContentArea>
                        {this.state.content}
                    </PanelContentArea>

                </div>

            </div>
        )
    }

    private showRed = () => {
        this.setState({
            content: this.red()
        });
    }

    private showBlue = () => {
        this.setState({
            content: this.blue()
        });
    }

    private red = () => (
        <div className="red screen"></div>
    )

    private blue = () => (
        <div className="blue screen"></div>
    )
}