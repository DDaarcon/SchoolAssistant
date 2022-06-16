import React from "react";

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
    hoverStates?: boolean[];
}
type LoadLessonsButtonLayerState = {
    hoverStates: boolean[];
}

class ButtonLayer extends React.Component<LoadLessonsButtonLayerProps, LoadLessonsButtonLayerState> {

    constructor(props) {
        super(props);

        this._index = this.props.amountIdx ?? 0;

        this.state = {
            hoverStates: this.props.hoverStates
                ?? Array.from({ length: this.props.amounts.length }).map(x => false)
        }
    }

    render() {
        if (!this._postLastLayer)
            return (
                <div
                    className={this._className}
                    onClick={this._onClick}
                    role="button"
                    onMouseEnter={() => this.hover(true)}
                    onMouseLeave={() => this.hover(false)}
                >
                    <div className="sll-load-lessons-btn-inner">
                        <ButtonLayer
                            {...this.props}
                            amountIdx={this._index + 1}
                            hoverStates={this.state.hoverStates}
                        />
                    </div>
                    <div className="sll-load-lessons-btn-val">
                        {this._amount}
                    </div>
                </div>
            );

        if (!this.props.children)
            return <></>;

        return (
            <div className="sll-load-lessons-inner-content">
                {this._childrenWithProps}
            </div>
        )
    }

    private _index: number;

    private get _postLastLayer() { return this._index >= this.props.amounts.length; }

    private get _amount() {
        return !this._postLastLayer
            ? this.props.amounts[this._index]
            : undefined;
    }

    private get _className() {
        let className = `sll-load-lessons-btn sll-load-lessons-btn-${this._amount} `;
        switch (this.props.layout) {
            case LoadLessonsButtonLayout.Upright:
                className += classNameUpright;
                break;
            case LoadLessonsButtonLayout.UpsideDown:
                className += classNameUpsideDown;
                break;
        }
        return className;
    }

    private get _onClick() { return () => this.props.onClick(this._amount); }


    private get _childrenWithProps() {
        return React.Children.map(this.props.children, child => {
            if (React.isValidElement(child)) {

                const highestHoveredAmount = this.props.amounts
                    .filter((_, index) => this.state.hoverStates[index])
                    .sort()
                    .shift();


                return React.cloneElement(child, {
                    highestHoveredAmount
                });
            }
            return child;
        });
    }

    private hover(hover: boolean) {
        if (this._postLastLayer)
            return;

        const hoverStates = [...this.state.hoverStates];
        hoverStates[this._index] = hover;

        this.setState({
            hoverStates
        });
    }
}