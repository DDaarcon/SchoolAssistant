import React from "react";
import './floating-pin.css';


const FloatingPin = (props: {
    onClick: () => void;
    attentionGrabbing?: boolean;
}) => {

    return (
        <div className="ph-floating-pin-container">
            <div className="ph-floating-pin-container-in">
                <div className={`ph-floating-pin ${props.attentionGrabbing ? 'ph-floating-pin-bipping' : ''}`}
                    onClick={props.onClick}
                >
                    <span>Menu podglądu</span>
                    <span>Dane logowania i więcej</span>
                </div>
            </div>
        </div>
    )
}
export default FloatingPin;