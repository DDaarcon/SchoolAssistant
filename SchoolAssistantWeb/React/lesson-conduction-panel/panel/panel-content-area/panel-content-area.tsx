import React from "react";
import LessonCondPanelContentContainerState from "../enums/lesson-cond-panel-content-container-state";
import './panel-content-area.css';

type PanelContentAreaProps = {
    children: React.ReactNode;
}
type PanelContentAreaState = {}

export default class PanelContentArea extends React.Component<PanelContentAreaProps, PanelContentAreaState> {

    constructor(props) {
        super(props);

        this._children = props.children;
        this._containerState = LessonCondPanelContentContainerState.Visible;
    }

    render() {
        return (
            <div className="lcp-content-area"
                ref={ref => this._container = ref}
                onTransitionEnd={this.finishedTransition}
            >
                {this._children}
            </div>
        )
    }

    private _children: React.ReactNode;
    private _containerState: LessonCondPanelContentContainerState;
    private _container: HTMLDivElement;

    private readonly HIDDEN_CLASS = "lcp-content-area-hidden";

    shouldComponentUpdate(nextProps: Readonly<PanelContentAreaProps>) {
        // when received new children
        if (this._children != nextProps.children)
        {
            setTimeout(() => {
                this._containerState = LessonCondPanelContentContainerState.ToHidden;
                this._container.classList.add(this.HIDDEN_CLASS);
            });
            return false;
        }

        return false;
    }


    private finishedTransition: React.TransitionEventHandler<HTMLDivElement> = (event) => {
        // when hiding transition has finished
        if (this._containerState == LessonCondPanelContentContainerState.ToHidden) {
            this._containerState = LessonCondPanelContentContainerState.Hidden;
            this._children = this.props.children;

            setTimeout(() => {
                this._containerState = LessonCondPanelContentContainerState.ToVisible;
                this._container.classList.remove(this.HIDDEN_CLASS);
            });

            this.forceUpdate();
            return;
        }

    }
}