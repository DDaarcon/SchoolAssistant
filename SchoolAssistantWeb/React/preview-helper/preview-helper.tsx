import React from "react";
import { enumAssignSwitch } from "../shared/enum-help";
import DefaultMenu from "./components/default-menu";
import FloatingPin from "./components/floating-pin";
import LoginMenu from "./components/login-menu";
import PreviewMenuType from "./enums/preview-menu-type";
import './preview-helper.css';

type PreviewHelperProps = {
    type: PreviewMenuType;
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
            </div>
        )
    }

    private toggleVisibility = () => {
        this.setState({ hidden: !this.state.hidden });
    }

    private renderMenu() {
        return enumAssignSwitch<JSX.Element, typeof PreviewMenuType>(PreviewMenuType, this.props?.type, {
            LoginMenu: <LoginMenu />,
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