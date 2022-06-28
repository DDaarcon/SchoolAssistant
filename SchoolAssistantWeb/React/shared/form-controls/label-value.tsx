import React from "react";
import './label-value.css';

interface LabelValueClassNames {
    containerClassName?: string;
    labelContainerClassName?: string;
    labelClassName?: string;
    valueContainerClassName?: string;
    valueClassName?: string;
}

type LabelValueProps = LabelValueClassNames & {
    label: string;
    value: React.ReactNode;

    width?: number;
}

const LabelValue = (props: LabelValueProps) => {

    const getClassName = (_for: keyof LabelValueClassNames) => {
        if (props[_for])
            return LVbaseClassNames[_for] + " " + props[_for];
        return LVbaseClassNames[_for];
    }

    return (
        <div className={getClassName('containerClassName')}
            {...props.width != undefined ? {
                style: { width: props.width }
            } : {} }>
            <div className={getClassName('labelContainerClassName')}>
                <span className={getClassName('labelClassName')}>
                    {props.label}
                </span>
            </div>
            <div className={getClassName('valueContainerClassName')}>
                <div className={getClassName('valueClassName')}>
                    {props.value}
                </div>
            </div>
        </div>
    )
}
export default LabelValue;

const LVbaseClassNames: LabelValueClassNames = {
    containerClassName: "label-value",
    labelContainerClassName: "lab-val-label-container",
    labelClassName: "lab-val-label",
    valueContainerClassName: "lab-val-value-container",
    valueClassName: "lab-val-value"
}