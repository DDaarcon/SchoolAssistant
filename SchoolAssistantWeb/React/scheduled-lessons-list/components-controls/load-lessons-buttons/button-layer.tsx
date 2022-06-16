import React from "react";

export enum LoadLessonsButtonLayout {
    Upright,
    UpsideDown
}
const classNameUpright = "sll-load-lessons-btn-upright";
const classNameUpsideDown = "sll-load-lessons-btn-upside-down";


type LoadLessonsButtonLayerProps = {
    layout: LoadLessonsButtonLayout;
    amounts: number[];
    amountIdx?: number;
    onClick: (amount: number) => void;
    children?: React.ReactNode;
}

const LoadLessonsButtonLayer = (props: LoadLessonsButtonLayerProps) => {
    const index = props.amountIdx ?? 0;

    if (index >= props.amounts.length) {
        // content of most inner layer
        if (!props.children) return <></>

        return (
            <div className="sll-load-lessons-inner-content">
                {props.children}
            </div>
        )
    }

    const amount = props.amounts[index];

    let className = `sll-load-lessons-btn sll-load-lessons-btn-${amount} `;
    switch (props.layout) {
        case LoadLessonsButtonLayout.Upright:
            className += classNameUpright;
            break;
        case LoadLessonsButtonLayout.UpsideDown:
            className += classNameUpsideDown;
            break;
    }

    const onClick = () => props.onClick(amount);

    return (
        <div
            className={className}
            onClick={onClick}
            role="button"
        >
            <div className="sll-load-lessons-btn-inner">
                <LoadLessonsButtonLayer
                    {...props}
                    amountIdx={index + 1}
                />
            </div>
            <div className="sll-load-lessons-btn-val">
                {amount}
            </div>
        </div>
    )
}
export default LoadLessonsButtonLayer;
