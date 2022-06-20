import React from "react";
import ButtonProps from "./button-props"

type ActionButtonProps = ButtonProps;

const ActionButton = (props: ActionButtonProps) => (
    <button
        className={"action-button " + (props.className ?? "")}
        onClick={props.onClick}
    >
        {props.label}
    </button>
)
export default ActionButton;