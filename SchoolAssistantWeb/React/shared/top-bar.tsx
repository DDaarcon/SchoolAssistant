import React from "react";
import './top-bar.css';

type TopBarProps = {}
type TopBarState = {}

class TopBarImpl extends React.Component<TopBarProps, TopBarState> {
    constructor(props) {
        super(props);

    }


    render() {
        return (
            <div className="top-bar-container">
                {this._renderBar()}
            </div>
        )
    }

    private _renderBar = () => {
        const style: React.CSSProperties = {
            transform: `translateY(${this.NAVBAR_BOTTOM_MARGIN * -1}px)`
        }

        return (
            <div className="top-bar" style={style}>
                {this._renderGoBackBtn()}
            </div>
        )
    }

    private _renderGoBackBtn = () => (
        <button
            className="top-bar-go-back-btn"
            onClick={() => this._goBack()}
        >
            Wróć
        </button>
    )

    private _goBack?: () => void;

    private readonly NAVBAR_BOTTOM_MARGIN = 16;

    public setGoBackAction(action: () => void) {
        this._goBack = action;
    }
}


export default class TopBar extends React.Component<TopBarProps> {

    static Ref: TopBarImpl;

    render() {
        return (
            <TopBarImpl
                ref={ref => TopBar.Ref = ref }
                {...this.props}
            />
        )
    }
}