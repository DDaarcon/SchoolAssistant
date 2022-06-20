import React from "react";
import ButtonProps from "./button-props"

type IconButtonProps = ButtonProps & {
    faIcon: string;
}

const IconButton = (props: IconButtonProps) => (
    <button
        className={"my-button icon-button " + (props.className ?? "")}
        onClick={props.onClick}
        title={props.label}
    >
        <i className={props.faIcon}></i>
    </button>
)
export default IconButton;