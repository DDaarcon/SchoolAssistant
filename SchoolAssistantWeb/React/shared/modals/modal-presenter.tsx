import * as React from "react";
import { CommonModalProps } from "./shared-modal-body";

type ModalPresenterState = {
    modals: React.ReactElement<CommonModalProps>[];
}
export default class ModalPresenter extends React.Component<{}, ModalPresenterState> {

    constructor(props) {
        super(props);

        this.state = {
            modals: []
        };
    }

    addModal(modal: React.ReactElement<CommonModalProps>, uniqueId: number) {
        if (modal.props && modal.props.assignedAtPresenter) {
            modal.props.assignedAtPresenter.uniqueId = uniqueId;
            modal.props.assignedAtPresenter.close = this.removeModalById;
        }
        this.setState(state => {
            return {
                modals: [...state.modals, modal]
            }
        });
    }

    removeLastModal = (event) => {
        this.setState(state => {
            return {
                modals: state.modals.slice(0, state.modals.length - 1)
            }
        });
    }

    removeModalById = (uniqueId: number) => {
        this.setState(state => {
            const filtered = state.modals.filter(x => x.props.assignedAtPresenter?.uniqueId != uniqueId);
            return {
                modals: filtered
            }
        });
    }

    render() {
        if (!this.state.modals.length)
            return <></>;


        return (
            <>
                <div className="modal-background">
                    <div className="modal-bg-scroll">
                        <div className="modal-dismiss-listener"
                            onClick={this.removeLastModal}
                        ></div>

                        {this.state.modals}
                    </div>
                </div>
            </>
        )
    }
}