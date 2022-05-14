import * as React from "react";
import { CommonModalProps } from "./shared-modal-body";

type ModalPresenterState = {
    modals: {
        component: React.ReactElement<CommonModalProps>;
        onClose?: () => void;
    }[];
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
            modal.props.assignedAtPresenter.close = () => this.removeModalById(uniqueId);
        }
        this.setState(state => {
            return {
                modals: [...state.modals, {
                    component: modal
                }]
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
            const index = state.modals.findIndex(x => x.component.props.assignedAtPresenter.uniqueId == uniqueId);
            if (index == -1)
                return state;

            const modal = state.modals.splice(index, 1)[0];
            modal.onClose?.();

            return state;
        });
    }

    setOnCloseAction = (uniqueId: number, onClose?: () => void) => {
        const modal = this.state.modals?.find(x => x.component.props.assignedAtPresenter?.uniqueId == uniqueId);
        if (!modal) return;

        modal.onClose = onClose;
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

                        {this.state.modals.map(x => x.component)}
                    </div>
                </div>
            </>
        )
    }
}