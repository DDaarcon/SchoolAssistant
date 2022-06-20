import React from "react";
import ButtonProps from "./button-props"

type IconLabelButtonProps = ButtonProps & {
    faIcon: string;
    iconSize?: 'small' | 'normal' | 'large';
    iconAbove?: boolean;
}

const IconLabelButton = (props: IconLabelButtonProps) => {
    

    return (
        <button
            className={
                "icon-label-button " +
                (props.className ?? "") + " " +
                (props.iconSize ? `icon-${props.iconSize}` : "") + " " +
                (props.iconAbove ? 'icon-above' : "")
            }
            onClick={props.onClick}
        >
            <i className={
                props.faIcon + " "
            }></i>
            {props.label}
        </button>
    )
}
export default IconLabelButton;