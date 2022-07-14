import * as React from "react";
import { CommonModalProps, ModalBody } from "./shared-modal-body";

export type ConfirmationModalProps = CommonModalProps & {
    onConfirm: () => (void | Promise<void>);
    onDecline?: () => (void | Promise<void>);
    header: string;
    text: string;
    style?: React.CSSProperties;
}
type ConfirmationModalState = {

}
export default class ConfirmationModal extends React.Component<ConfirmationModalProps, ConfirmationModalState> {
    constructor(props) {
        super(props);
        this.props.assignedAtPresenter.onClose = this.onCloseDecline;
    }

    onCloseConfirm = () => this.onCloseWithAsync(this.props.onConfirm);
    onCloseDecline = () => this.onCloseWithAsync(this.props.onDecline);

    onCloseWithAsync = async (action?: () => (void | Promise<void>)) => {
        const res = action?.();
        if (res)
            await res;
        this.props.assignedAtPresenter?.close();
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