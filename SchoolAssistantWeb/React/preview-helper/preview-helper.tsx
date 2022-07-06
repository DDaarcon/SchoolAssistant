import React from "react";
import FloatingPin from "./components/floating-pin";
import './preview-helper.css';

type PreviewHelperProps = {}
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
                    textOnHover="Dane logowania i więcej"
                    onClick={this.toggleVisibility}
                    attentionGrabbing={this.state.hidden}
                />



            </div>
        )
    }

    private toggleVisibility = () => {
        this.setState({ hidden: !this.state.hidden })
    }
}