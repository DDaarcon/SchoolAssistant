import React from "react";
import { enumAssignSwitch } from "../../../shared/enum-help";
import ArrowAnimationEventHelper from "./animation-events";
import { LoadLessonsButtonLayout } from "./layered-button";
import './load-lessons-button-icon.css';

type LoadLessonsButtonIconProps = {
    layout: LoadLessonsButtonLayout;
    maxAmount?: number;
}
type LoadLessonsButtonIconState = {
    animateForAmount?: number;
}

export default class LoadLessonsButtonIcon extends React.Component<LoadLessonsButtonIconProps, LoadLessonsButtonIconState> {

    constructor(props) {
        super(props);
        this.state = {};
    }

    componentDidMount() {
        ArrowAnimationEventHelper.addListener(this.props.layout, (details) => {
            if (details.on) {
                this.setState({ animateForAmount: details.amount });
                return;
            }
            if (this.state?.animateForAmount == details.amount) {
                this.setState({ animateForAmount: undefined });
                return;
            }
        })
    }

    render() {
        return (
            <div className={this._boxClassName}>
                <div className={this._className}
                    style={{
                        animationDuration: `${this._speed}s`,
                    }}
                >
                    <i className="fa-solid fa-angles-right"></i>
                </div>
            </div>
        )
    }

    private get _speed() {
        if (!this.state.animateForAmount)
            return 0;
        else {
            return this.props.maxAmount / this.state.animateForAmount  * 0.5;
        }
    }

    private get _boxClassName() {
        return "all-load-button-icon-box " + enumAssignSwitch<string, typeof LoadLessonsButtonLayout>(LoadLessonsButtonLayout, this.props.layout, {
            Upright: () => "all-load-button-icon-box-upright",
            UpsideDown: () => "all-load-button-icon-box-upside-down"
        })
    }

    private get _className() {
        return "sll-load-button-icon " + enumAssignSwitch<string, typeof LoadLessonsButtonLayout>(LoadLessonsButtonLayout, this.props.layout, {
            Upright: () => "sll-load-button-icon-upright",
            UpsideDown: () => "sll-load-button-icon-upside-down"
        })
    }   
}