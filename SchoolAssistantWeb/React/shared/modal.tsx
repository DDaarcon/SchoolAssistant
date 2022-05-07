﻿
import React from "react";
import { ModificationComponentProps } from "../data-management/shared-table";

/*
 *  Instance of ModalController (`modalController`) is used for ordering modal displays
 * 
 */
type ModalPropsToPass<TModalProps extends CommonModalProps> = Omit<TModalProps, "assignedAtPresenter">;

class ModalController {
    private _modalSpaceRef: React.RefObject<ModalPresenter>;
    get modalSpaceRef() { return this._modalSpaceRef; }

    private static _uniqueIdCounter = 0;
    get newUniqueId() {
        return ModalController._uniqueIdCounter++;
    }

    private get _modalPresenter() {
        if (this._modalSpaceRef.current != undefined)
            return this._modalSpaceRef.current;

        throw new Error("There is no ModalSpace in the document");
    }

    constructor() {
        this._modalSpaceRef = React.createRef<ModalPresenter>();
    }

    add(props: ModalPropsToPass<ModalProps>) {
        const id = this.newUniqueId;
        this._modalPresenter.addModal(
            <Modal
                key={id}
                assignedAtPresenter={{}}
                {...props}
            />,
            id
        );
        return id;
    }

    addConfirmation(props: ModalPropsToPass<ConfirmationModalProps>) {
        const id = this.newUniqueId;
        this._modalPresenter.addModal(
            <ConfirmationModal
                key={id}
                assignedAtPresenter={{}}
                {...props}
            />,
            id
        );
        return id;
    }

    addModificationComponent
        <TMCPassedProps extends ModificationComponentProps>
        (props: ModalPropsToPass<ModificationComponentModalProps<TMCPassedProps>>) {

        const id = this.newUniqueId;
        this._modalPresenter.addModal(
            <ModificationComponentModal
                key={id}
                assignedAtPresenter={{}}
                {...props}
            />,
            id
        );
        return id;
    }

    addCustom(modal: React.ReactElement<CommonModalProps>) {
        const id = this.newUniqueId;
        this._modalPresenter.addModal(modal, id);
        return id;
    }

    closeById(id: number) {
        this._modalPresenter.removeModalById(id);
    }
}

const modalController = new ModalController;
export default modalController;




/*
 *  Below are defined modal templated
 * 
 * 
 * 
 */
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
const ModalBody: (props: ModalBodyProps) => JSX.Element = (props) => {
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






type ModalProps = CommonModalProps & {
    style?: React.CSSProperties;
    children: React.ReactNode;
}
type ModalState = {

}
export class Modal extends React.Component<ModalProps, ModalState> {

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






type ConfirmationModalProps = CommonModalProps & {
    onConfirm: () => void;
    onDecline?: () => void;
    header: string;
    text: string;
    style?: React.CSSProperties;
}
type ConfirmationModalState = {

}
export class ConfirmationModal extends React.Component<ConfirmationModalProps, ConfirmationModalState> {
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
                style={this.props.style }
            >
                <h3>{this.props.header}</h3>
                <p>{this.props.text}</p>
                <button
                    type="button"
                    onClick={this.onCloseConfirm}
                >
                    Ok
                </button>
                <button
                    type="button"
                    onClick={this.onCloseDecline}
                >
                    Anuluj
                </button>
            </ModalBody>
        )
    }
}




export type ModaledModificationComponentProps = ModificationComponentProps & {

}

type ModificationComponentModalProps
    <TMCPassedProps extends ModaledModificationComponentProps> = CommonModalProps &
{
    modificationComponent: new (props: TMCPassedProps & CommonModalProps) => React.Component<TMCPassedProps & CommonModalProps>;
    modificationComponentProps: TMCPassedProps;
    style?: React.CSSProperties;
}
type ModificationComponentModalState = {

}
export class ModificationComponentModal
    <TModificationComponentProps extends ModaledModificationComponentProps>
    extends React.Component<ModificationComponentModalProps<TModificationComponentProps>, ModificationComponentModalState>
{

    render() {
        return (
            <ModalBody
                assignedAtPresenter={this.props.assignedAtPresenter}
                style={this.props.style}
            >
                <this.props.modificationComponent
                    {...this.props.modificationComponentProps}
                    assignedAtPresenter={this.props.assignedAtPresenter}
                />
            </ModalBody>
        )
    }
}

















/*
 *  Components below are responsible for displaying modals added via `modalController`
 */

export class ModalSpace extends React.Component {
    private static _validSpaceAlreadyExists: boolean = false;
    private _thisSpaceIsValid: boolean = false;

    constructor(props) {
        super(props);

        if (!ModalSpace._validSpaceAlreadyExists) {
            ModalSpace._validSpaceAlreadyExists = true;
            this._thisSpaceIsValid = true;
        }
    }

    render() {
        if (!this._thisSpaceIsValid)
            return <></>;

        return (
            <ModalPresenter
                ref={modalController.modalSpaceRef}
            />
        )
    }
}




type ModalPresenterState = {
    modals: React.ReactElement<CommonModalProps>[];
}
class ModalPresenter extends React.Component<{}, ModalPresenterState> {

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