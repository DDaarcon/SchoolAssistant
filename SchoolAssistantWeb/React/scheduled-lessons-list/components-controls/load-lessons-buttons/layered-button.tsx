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

            const handlers = {
                onClick: this._onClick,
                onMouseEnter: () => this.hover(true),
                onMouseLeave: () => this.hover(false)
            };

            return (
                <div
                    className={this._className}
                    role="button"
                    {...this._lastLayer && !this._postLastLayer
                        ? handlers
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
                            ? handlers
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
                {this._childrenWithProps}
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


    private _childrenWithPropsBackingField?: React.ReactNode;

    private get _childrenWithProps() {
        this._childrenWithPropsBackingField ??= React.Children
            .map(this.props.children, child => {

                const maxAmount = this.props.amounts.sort((a, b) => a-b).at(-1);

                if (React.isValidElement(child)) {
                    return React.cloneElement(child, { maxAmount });
                }
                return child;
            });

        return this._childrenWithPropsBackingField;
    }
}