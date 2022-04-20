import * as React from "react";

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
}

export const Loader = (props: LoaderProps) => {
    let sizeStyle;

    switch (props.size) {
        case LoaderSize.Large:
            sizeStyle = {};
            break;
        case LoaderSize.Medium:
            sizeStyle = {
                transform: 'scale(0.5)'
            };
            break;
        default:
            sizeStyle = {
                transform: 'scale(0.3)'
            };
            break;
    }

    const dots = (
        <svg style={sizeStyle} id="loader-dots" width="132px" height="58px" viewBox="0 0 132 58" version="1.1" xmlns="http://www.w3.org/2000/svg" xmlnsXlink="http://www.w3.org/1999/xlink">
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

    switch (props.type) {
        case LoaderType.DivWholeSpace:
            return (
                <div className="loader-div-whole-space">
                    {dots}
                </div>
            );

        case LoaderType.Absolute:
            return (
                <div className="loader-absolute">
                    {dots}
                </div>
            );

        default:
            return (
                <>
                    {dots}
                </>
            )
    }
}

export default Loader;