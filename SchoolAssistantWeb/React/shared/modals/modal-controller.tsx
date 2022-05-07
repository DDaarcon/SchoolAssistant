import React from "react";
import ConfirmationModal, { ConfirmationModalProps } from "./confirmation-modal";
import Modal, { ModalProps } from "./modal";
import CustomComponentModal, { CustomComponentModalProps } from './custom-component-modal';
import { CommonModalProps } from './shared-modal-body';
import ModalPresenter from './modal-presenter';



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

    addCustom(modal: React.ReactElement<CommonModalProps>) {
        const id = this.newUniqueId;
        this._modalPresenter.addModal(modal, id);
        return id;
    }

    addCustomComponent<TComponentProps>(props: ModalPropsToPass<CustomComponentModalProps<TComponentProps, TComponentProps & CommonModalProps>>) {
        const id = this.newUniqueId;
        this._modalPresenter.addModal(
            <CustomComponentModal
                key={id}
                assignedAtPresenter={{}}
                {...props}
            />,
            id
        );
        return id;
    }

    closeById(id: number) {
        this._modalPresenter.removeModalById(id);
    }
}

const modalController = new ModalController;
export default modalController;






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