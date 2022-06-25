import React from "react";
import ButtonProps from "./button-props"

type ActionButtonProps = ButtonProps;

const ActionButton = (props: ActionButtonProps) => {
    const type = props.typeSubmit ? 'submit' : 'button';

    return (
        <button
            className={"my-button action-button " + (props.className ?? "")}
            onClick={props.onClick}
            type={type}
        >
            {props.label}
        </button>
    )
}
export default ActionButton;