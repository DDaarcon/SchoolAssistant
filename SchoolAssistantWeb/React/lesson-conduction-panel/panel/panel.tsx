import React from "react";
import TogglePanelService from "../services/toggle-panel-service";
import Clock from "./components/clock";
import './panel.css';

type PanelProps = {

}
type PanelState = {
    show: boolean;
}

export default class Panel extends React.Component<PanelProps, PanelState> {

    constructor(props) {
        super(props);

        this.state = {
            show: false
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

                    <Clock
                        startTime={this._start}
                        duration={this._dur}
                    />

                </div>

            </div>
        )
    }
}