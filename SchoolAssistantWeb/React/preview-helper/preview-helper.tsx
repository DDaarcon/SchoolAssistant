import React from "react";
import FloatingPin from "./components/floating-pin";
import './preview-helper.css';

export default class PreviewHelper extends React.Component {

    render() {
        return (
            <div className="preview-helper ph-shown">
                <FloatingPin
                    onClick={this.toggleVisibility}
                    attentionGrabbing
                />



            </div>
        )
    }

    private toggleVisibility = () => {

    }
}