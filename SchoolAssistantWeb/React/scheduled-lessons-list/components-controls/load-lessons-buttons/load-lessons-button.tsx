import React from "react";
import LayeredButton, { LoadLessonsButtonLayout } from "./layered-button";
import './load-lessons-button.css';

type LoadLessonsButtonProps = {
    layout: LoadLessonsButtonLayout;
    onClick: (amount: number) => void;
    children?: React.ReactNode;
}

export default class LoadLessonsButton extends React.Component<LoadLessonsButtonProps> {

    render() {
        return (
            <div className="sll-load-lessons-whole-btn">
                <LayeredButton
                    layout={this.props.layout}
                    amounts={LoadLessonsButton._amounts}
                    onClick={this.props.onClick}
                    children={this.props.children}
                />
            </div>
        )
    }

    private static _amounts = [5, 10, 20, 50];
}