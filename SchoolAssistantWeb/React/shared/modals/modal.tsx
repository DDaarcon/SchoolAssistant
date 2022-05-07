import * as React from "react";
import { CommonModalProps, ModalBody } from "./shared-modal-body";

export type ModalProps = CommonModalProps & {
    style?: React.CSSProperties;
    children: React.ReactNode;
}
type ModalState = {

}
export default class Modal extends React.Component<ModalProps, ModalState> {

    render() {
        return (
            <ModalBody
                assignedAtPresenter={this.props.assignedAtPresenter}
                style={this.props.style}
            >
                {this.props.children}
            </ModalBody>
        )
    }
}
