import * as React from "react";
import { enumAssignSwitch, enumSwitch } from "./enum-help/enum-switch";
import './loader.css';

export enum LoaderType {
    DivWholeSpace,
    Absolute,
    Inline
}

export enum LoaderSize {
    Large,
    Medium,
    Small
}

export type LoaderProps = {
    enable?: boolean,
    type?: LoaderType;
    size?: LoaderSize;
    className?: string;
}

export const Loader = React.forwardRef<HTMLDivElement, LoaderProps>((props: LoaderProps, ref: React.LegacyRef<HTMLDivElement>) => {
    let sizeStyle;

    enumSwitch(LoaderSize, props.size, {
        Large: () => sizeStyle = {},
        Medium: () => sizeStyle = {
            transform: 'scale(0.5)'
        },
        _: () => sizeStyle = {
            transform: 'scale(0.3)'
        }
    });

    const dots = (
        <svg style={sizeStyle}
            className="loader-dots"
            width="132px"
            height="58px"
            viewBox="0 0 132 58"
            version="1.1"
            xmlns="http://www.w3.org/2000/svg"
            xmlnsXlink="http://www.w3.org/1999/xlink"
        >
            {/*Generator: Sketch 3.5.1 (25234) - http://www.bohemiancoding.com/sketch*/}
            <title>dots</title>
            <desc>Created with Sketch.</desc>
            <defs></defs>
            <g id="Page-1" stroke="none" strokeWidth="1" fill="none" fillRule="evenodd">
                <g id="dots" fill="#A3A3A3">
                    <circle id="dot1" cx="25" cy="30" r="13"></circle>
                    <circle id="dot2" cx="65" cy="30" r="13"></circle>
                    <circle id="dot3" cx="105" cy="30" r="13"></circle>
                </g>
            </g>
        </svg>
    );

    if (!props.enable)
        return (<></>)



    let className = enumAssignSwitch<string, typeof LoaderType>(LoaderType, props.type, {
        DivWholeSpace: "loader-div-whole-space",
        Absolute: "loader-absolute",
        _: "loader-inline"
    });

    if (props.className)
        className += " " + props.className;

    return (
        <div className={className}
            ref={ref}
        >
            {dots}
        </div>
    );
});

export default Loader;