import React from "react";
import { enumAssignSwitch } from "../../../shared/enum-help/enum-switch";
import ArrowAnimationEventHelper from "./animation-events";

export enum LoadLessonsButtonLayout {
    Upright,
    UpsideDown
}
const classNameUpright = "sll-load-lessons-btn-upright";
const classNameUpsideDown = "sll-load-lessons-btn-upside-down";





type LayeredButtonProps = {
    layout: LoadLessonsButtonLayout;
    amounts: number[];
    onClick: (amount: number) => void;
    children?: React.ReactNode;
}

const LayeredButton = (props: LayeredButtonProps) => {
    return (
        <ButtonLayer
            {...props}
        />
    )
};
export default LayeredButton






type LoadLessonsButtonLayerProps = LayeredButtonProps & {
    amountIdx?: number;
}

class ButtonLayer extends React.Component<LoadLessonsButtonLayerProps> {

    constructor(props) {
        super(props);

        this._index = this.props.amountIdx ?? 0;
    }

    render() {
        if (!this._postLastLayer) {
            const { amountIdx, ...propsToPass } = this.props;

            const mouseEvents = {
                onMouseEnter: () => this.hover(true),
                onMouseLeave: () => this.hover(false)
            };

            return (
                <div
                    className={this._className}
                    onClick={this._onClick}
                    role="button"
                    {...this._lastLayer && !this._postLastLayer
                        ? mouseEvents
                        : {}
                    }
                >
                    <div className="sll-load-lessons-btn-inner">
                        <ButtonLayer
                            {...propsToPass}
                            amountIdx={this._index + 1}
                        />
                    </div>
                    <div className="sll-load-lessons-btn-right-edge"
                        {...!this._lastLayer && !this._postLastLayer
                            ? mouseEvents
                            : {}
                        }
                    >
                        <div className="sll-load-lessons-btn-val">
                            {this._amount}
                        </div>
                    </div>
                </div>
            );
        }

        if (!this.props.children)
            return <></>;

        return (
            <div className="sll-load-lessons-inner-content">
                {this.props.children}
            </div>
        )
    }

    private _index: number;

    private get _lastLayer() { return this._index == this.props.amounts.length - 1; }

    private get _postLastLayer() { return this._index >= this.props.amounts.length; }

    private get _amount() {
        return !this._postLastLayer
            ? this.props.amounts[this._index]
            : undefined;
    }

    private get _className() {
        let className = `sll-load-lessons-btn sll-load-lessons-btn-${this._amount} `;

        className += enumAssignSwitch<string, typeof LoadLessonsButtonLayout>(LoadLessonsButtonLayout, this.props.layout, {
            Upright: () => classNameUpright,
            UpsideDown: () => classNameUpsideDown,
            _: () => { throw new Error("invalid 'LoadLessonsButtonLayout' enum value") }
        })

        return className;
    }

    private get _onClick() { return () => this.props.onClick(this._amount); }

    private hover(hover: boolean) {
        if (this._postLastLayer)
            return;

        ArrowAnimationEventHelper.dispatch(this.props.layout, { amount: this._amount, on: hover });
    }
}