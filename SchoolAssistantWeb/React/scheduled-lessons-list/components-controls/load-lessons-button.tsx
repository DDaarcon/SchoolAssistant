import React from "react";
import './load-lessons-button.css';

type LoadLessonsButtonProps = {
    text: string;
    onClick: (amount: number) => void;
}

export default class LoadLessonsButton extends React.Component<LoadLessonsButtonProps> {

    render() {
        return (
            <div className="sll-load-lessons-button">
                <button
                    className="sll-load-lessons-button-main"
                    onClick={() => this.props.onClick(5)}
                >
                    {this.props.text}
                </button>
                <button
                    className="sll-load-lessons-button-10"
                    onClick={() => this.props.onClick(10)}
                >
                    10
                </button>
                <button
                    className="sll-load-lessons-button-20"
                    onClick={() => this.props.onClick(20)}
                >
                    20
                </button>
                <button
                    className="sll-load-lessons-button-50"
                    onClick={() => this.props.onClick(50)}
                >
                    50
                </button>
            </div>
        )
    }
}