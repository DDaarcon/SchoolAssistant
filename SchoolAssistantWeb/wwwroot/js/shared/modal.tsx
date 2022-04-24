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


    add(props: Omit<ModalProps, "assignedAtPresenter">) {
        const id = this.newUniqueId;
        this._modalPresenter.addModal(
            <Modal
                key={id}
                assignedAtPresenter={{}}
                {...props}
            />,
            id
        );
    }

    addCustom(modal: React.ReactElement<CommonModalProps>) {
        this._modalPresenter.addModal(modal, this.newUniqueId);
    }
}

const modalController = new ModalController;






type CommonModalProps = {
    assignedAtPresenter: {
        uniqueId?: number;
        close?: (uniqueId: number) => void;
    }
}

type ModalProps = CommonModalProps & {
    form?: FormConfig;
    children: React.ReactNode;
}
type ModalState = {

}

type FormConfig = {
    onSubmit: () => void;
}
class Modal extends React.Component<ModalProps, ModalState> {
    onClose = () => {
        this.props.assignedAtPresenter?.close(this.props.assignedAtPresenter.uniqueId);
    }

    render() {
        return (
            <div className="modal-container">
                {this.props.children}
                <button
                    type="button"
                    onClick={this.onClose}
                >
                    Zamknij
                </button>
            </div>
        )
    }
}






class ModalSpace extends React.Component {
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