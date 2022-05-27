import React from "react";
import './label-value.css';

type LabelValueProps = {
    label: string;
    valueComp: React.ReactNode;

    outerStyle?: React.CSSProperties;
    labelOuterStyle?: React.CSSProperties;
    valueOuterStyle?: React.CSSProperties;
    labelStyle?: React.CSSProperties;
    valueStyle?: React.CSSProperties;

    width?: number;
}

const LabelValue = (props: LabelValueProps) => {
    const outerStyle = props.outerStyle ?? {};

    if (props.width)
        outerStyle.width = props.width;

    return (
        <div className="label-value" style={outerStyle}>
            <div className="lab-val-label-container" style={props.labelOuterStyle ?? {}}>
                <span className="lab-val-label" style={props.labelStyle ?? {}}>
                    {props.label}
                </span>
            </div>
            <div className="lab-val-value-container" style={props.valueOuterStyle ?? {}}>
                <div className="lab-val-value" style={props.valueStyle ?? {}}>
                    {props.valueComp}
                </div>
            </div>
        </div>
    )
}

export default LabelValue;