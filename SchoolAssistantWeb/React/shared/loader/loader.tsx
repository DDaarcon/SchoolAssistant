import * as React from "react";
import { enumAssignSwitch } from "../enum-help";
import LoaderSize from "./enums/loader-size";
import LoaderType from "./enums/loader-type";
import LoaderProps from "./interfaces/loader-props";
import './loader.css';


type LoaderState = {
    renderAnythig: boolean;
}

export default class Loader extends React.Component<LoaderProps, LoaderState> {

    constructor(props) {
        super(props);

        this.state = {
            renderAnythig: this.props.enable
        }
    }

    render() {
        if (!this.state.renderAnythig)
            <></>

        return (
            <div className={this._className}
                ref={ref => this._containerRefBF = ref}
            >
                {this.dots}
            </div>
        )
    }

    shouldComponentUpdate(nextProps: Readonly<LoaderProps>) {
        if (this.props.enable == nextProps.enable)
            return true;

        if (nextProps.enable) {
            this.show();
        }
        else {
            this.hide();
        }
        return false;
    }

    private readonly HIDE_TIMEOUT_MS = 300;
    private _timeout?: NodeJS.Timeout;
    private _containerRefBF?: HTMLDivElement
    public get containerRef() { return this._containerRefBF; }


    public show() {
        clearTimeout(this._timeout);
        setTimeout(() => this.setState({ renderAnythig: true }));
        setTimeout(() => this._containerRefBF.classList.remove(this.HIDDEN_CLASS));
    }

    public hide() {
        this._containerRefBF.classList.add(this.HIDDEN_CLASS);

        clearInterval(this._timeout);
        this._timeout = setTimeout(
            () => this.setState({ renderAnythig: false }),
            this.HIDE_TIMEOUT_MS
        );
    }


    private readonly DEFAULT_CLASS = "loader-plane";
    private readonly HIDDEN_CLASS = "loader-plane-hidden";

    private get _className() {

        return this.DEFAULT_CLASS + " " +
            (!this.props.enable ? this.HIDDEN_CLASS + " " : "") +
            enumAssignSwitch<string, typeof LoaderType>(LoaderType, this.props.type, {
                BlockPage: "loader-plane-block-page",
                DivWholeSpace: "loader-div-whole-space",
                Absolute: "loader-plane-absolute",
                _: "loader-plane-inline"
            });
    }





    private dots = (
        <svg style={this._sizeStyle}
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

    private get _sizeStyle() {
        return enumAssignSwitch<React.CSSProperties, typeof LoaderSize>(LoaderSize, this.props.size, {
            Large: {},
            Medium: {
                transform: 'scale(0.5)'
            },
            _: {
                transform: 'scale(0.3)'
            }
        });
    }
}