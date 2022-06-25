import React from "react";
import { ActionButton } from "../../../shared/components";
import SaveButtonService from "../../services/save-button-service";
import './save-button.css';

type SaveButtonProps = {}
type SaveButtonState = {
    shown: boolean;
    onClick?: () => void;
}

export default class SaveButton extends React.Component<SaveButtonProps, SaveButtonState> {

    constructor(props) {
        super(props);

        this.state = {
            shown: false
        }

        SaveButtonService.registerButton(this);
    }

    render() {
        return (
            <ActionButton
                label="Zapisz"
                onClick={this._click}
                className={"lcp-save-btn " + (!this.state.shown ? "lcp-save-btn-hidden" : "")}
            />
        )
    }

    private get _click(): (() => void) | undefined
        { return this.state.shown ? this.state.onClick : undefined }
}