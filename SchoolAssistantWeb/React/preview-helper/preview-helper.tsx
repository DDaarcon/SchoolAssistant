﻿import React from "react";
import { enumAssignSwitch } from "../shared/enum-help";
import { ModalSpace } from "../shared/modals";
import DefaultMenu from "./components/default-menu";
import FloatingPin from "./components/floating-pin";
import LoginMenu from "./components/login-menu";
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