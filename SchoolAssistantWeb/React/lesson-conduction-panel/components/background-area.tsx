import React from "react";
import TogglePanelService from "../services/toggle-panel-service";
import './background-area.css';

type BackgroundAreaProps = {}
type BackgroundAreaState = {
    show: boolean;
}

export default class BackgroundArea extends React.Component<BackgroundAreaProps, BackgroundAreaState> {

    constructor(props) {
        super(props);

        this.state = {
            show: false
        }

        TogglePanelService.registerBackgoundArea(this);
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
            <div className={"lcp-background " + (this.isShown ? "" : "lcp-bg-hide")}>

            </div>
        )
    }
}