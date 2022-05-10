import * as React from "react";
import { CommonModalProps, ModalBody } from "./shared-modal-body";

export type ConfirmationModalProps = CommonModalProps & {
    onConfirm: () => void;
    onDecline?: () => void;
    header: string;
    text: string;
    style?: React.CSSProperties;
}
type ConfirmationModalState = {

}
export default class ConfirmationModal extends React.Component<ConfirmationModalProps, ConfirmationModalState> {
    onCloseConfirm = () => this.onCloseWith(this.props.onConfirm);
    onCloseDecline = () => this.onCloseWith(this.props.onDecline);

    onCloseWith = (action?: () => void) => {
        action?.();
        this.props.assignedAtPresenter?.close(this.props.assignedAtPresenter.uniqueId);
    }

    render() {
        return (
            <ModalBody
                assignedAtPresenter={this.props.assignedAtPresenter}
                style={this.props.style}
            >
                <h3>{this.props.header}</h3>
                <p>{this.props.text}</p>
                <div className="confirmation-modal-buttons">
                    <button
                        className="confirmation-modal-confirm-btn"
                        type="button"
                        onClick={this.onCloseConfirm}
                    >
                        Ok
                    </button>
                    <button
                        className="confirmation-modal-decline-btn"
                        type="button"
                        onClick={this.onCloseDecline}
                    >
                        Anuluj
                    </button>
                </div>
            </ModalBody>
        )
    }
}