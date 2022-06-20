import React from "react";
import TogglePanelService from "../../services/toggle-panel-service";
import './anchor.css';

type AnchorProps = {}
type AnchorState = {}

export default class Anchor extends React.Component<AnchorProps, AnchorState> {


    render() {
        return (
            <button className="lcp-anchor"
                onClick={this.click}
            >

            </button>
        )
    }

    private click = () => {
        TogglePanelService.toggle();
    }
}