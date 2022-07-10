import React from "react";

export type RowButtonProps = {
    text: string;
    className?: string;
    onClickAsync?: () => Promise<void>;
}

const RowButton = ({ text, className, onClickAsync }: RowButtonProps) => {
    return (
        <button
            className={`sll-row-button ${className ?? ''}`}
            onClick={onClickAsync}
        >
            <span>{text}</span>
        </button>
    )
}

export default RowButton;