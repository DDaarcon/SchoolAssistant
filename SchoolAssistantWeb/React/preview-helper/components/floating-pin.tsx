import React from "react";
import './floating-pin.css';


const FloatingPin = (props: {
    textOnHover: string;
    onClick: () => void;
    attentionGrabbing?: boolean;
}) => {

    return (
        <div className="ph-floating-pin-container">
            <div className="ph-floating-pin-container-in">
                <div className={`ph-floating-pin ${props.attentionGrabbing ? 'ph-floating-pin-bipping ph-floating-pin-menu-hidden' : ''}`}
                    onClick={props.onClick}
                >
                    <span>Menu podglądu</span>

                    <span>{props.textOnHover}</span>
                </div>
            </div>
        </div>
    )
}
export default FloatingPin;