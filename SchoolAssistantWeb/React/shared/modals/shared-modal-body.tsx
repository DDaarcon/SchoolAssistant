import * as React from "react";

export type CommonModalProps = {
    assignedAtPresenter: {
        uniqueId?: number;
        close?: (uniqueId: number) => void;
    }
}


type ModalBodyProps = CommonModalProps & {
    style?: React.CSSProperties;
    closeBtnStyle?: React.CSSProperties;
    children: React.ReactNode;
}
export const ModalBody: (props: ModalBodyProps) => JSX.Element = (props) => {
    const close = () => {
        props.assignedAtPresenter?.close(props.assignedAtPresenter.uniqueId);
    }

    return (
        <div className="modal-container"
            style={props.style}
        >
            <a className="modal-close-btn"
                style={props.closeBtnStyle}
                onClick={close}
            >
                <i className="fa-solid fa-xmark"></i>
            </a>
            {props.children}
        </div>
    )
}