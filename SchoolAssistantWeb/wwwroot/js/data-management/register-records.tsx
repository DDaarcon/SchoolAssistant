
interface StudentRegisterRecordDetails {
    id?: number;

    firstName: string;
    secondName?: string;
    lastName: string;

    dateOfBirth: string;
    placeOfBirth: string;

    personalId: string;
    address: string;

    firstParent: ParentRegisterSubecordDetails;
    secondParent?: ParentRegisterSubecordDetails;
}

interface ParentRegisterSubecordDetails {
    firstName: string;
    secondName?: string;
    lastName: string;

    phoneNumber: string;
    email?: string;

    address: string;
}

interface StudentRegisterRecordModificationData {
    data: StudentRegisterRecordDetails;
}







type StudentRegisterRecordMCProps = ModaledModificationComponentProps & CommonModalProps & {
    selectRecord: (id: number) => void;
}
type StudentRegisterRecordMCState = {
    data: StudentRegisterRecordDetails;
    awaitingData: boolean;
    addressSameAsChilds: {
        firstParent: boolean;
        secondParent: boolean;
    }
}
class StudentRegisterRecordMC extends React.Component<StudentRegisterRecordMCProps, StudentRegisterRecordMCState> {
    constructor(props) {
        super(props);

        this.state = {
            awaitingData: this.props.recordId > 0,
            data: {
                firstName: '',
                secondName: '',
                lastName: '',
                dateOfBirth: '',
                address: '',
                personalId: '',
                placeOfBirth: '',
                firstParent: EmptyParentRegisterSubrecordDetails(),
                secondParent: EmptyParentRegisterSubrecordDetails()
            },
            addressSameAsChilds: {
                firstParent: true,
                secondParent: true
            }
        }

        if (this.props.recordId)
            this.fetchAsync();
    }

    private async fetchAsync() {
        let response = await server.getAsync<StudentRegisterRecordModificationData>("StudentRegisterRecordModificationData", {
            id: this.props.recordId
        });

        this.setState({
            data: response.data,
            awaitingData: false,
            addressSameAsChilds: {
                firstParent: response.data.address == response.data.firstParent.address,
                secondParent: response.data.secondParent == undefined ? false
                    : response.data.address == response.data.secondParent.address
            }
        });
    }

    createOnTextChangeHandler: (property: keyof StudentRegisterRecordDetails) => React.ChangeEventHandler<HTMLInputElement> = (property) => {
        return (event) => {
            const value = event.target.value;

            this.setState(prevState => {
                const data = { ...prevState.data };
                data[property] = (value as unknown) as never;
                return { data };
            });

            this.props.onMadeAnyChange();
        }
    }

    onAddressChange: React.ChangeEventHandler<HTMLInputElement> = (event) => {
        const value = event.target.value;

        this.setState(prevState => {
            const data = { ...prevState.data };

            data.address = value;
            if (this.state.addressSameAsChilds.firstParent)
                data.firstParent.address = value;
            if (this.state.addressSameAsChilds.secondParent && this.state.data.secondParent)
                data.secondParent.address = value;

            return { data };
        });

        this.props.onMadeAnyChange();
    }


    onSubmitAsync: React.FormEventHandler<HTMLFormElement> = async (event) => {
        event.preventDefault();

        let response = await server.postAsync<SaveResponseJson>("StudentRegisterRecordData", undefined, {
            ...this.state.data
        });

        if (response.success) {
            this.props.selectRecord(response.id);
            await this.props.reloadAsync();
            this.props.assignedAtPresenter.close(this.props.assignedAtPresenter.uniqueId);
        }
        else
            console.debug(response);
    }

    render() {
        if (this.state.awaitingData)
            return (
                <Loader
                    enable={true}
                    size={LoaderSize.Medium}
                    type={LoaderType.DivWholeSpace}
                />
            )

        return (
            <div>
                <form onSubmit={this.onSubmitAsync}>
                    <div className="form-group">
                        <label htmlFor="first-name-input">Imię</label>
                        <input
                            type="text"
                            className="form-control"
                            name="first-name-input"
                            value={this.state.data.firstName}
                            onChange={this.createOnTextChangeHandler('firstName')}
                        />
                    </div>
                    
                    <div className="form-group">
                        <label htmlFor="second-name-input">Drugie imię</label>
                        <input
                            type="text"
                            className="form-control"
                            name="second-name-input"
                            value={this.state.data.secondName}
                            onChange={this.createOnTextChangeHandler('secondName')}
                        />
                    </div>
                    
                    <div className="form-group">
                        <label htmlFor="last-name-input">Nazwisko</label>
                        <input
                            type="text"
                            className="form-control"
                            name="last-name-input"
                            value={this.state.data.lastName}
                            onChange={this.createOnTextChangeHandler('lastName')}
                        />
                    </div>
                    
                    <div className="form-group">
                        <label htmlFor="date-of-birth-input">Data urodzenia</label>
                        <input
                            type="date"
                            className="form-control"
                            name="date-of-birth-input"
                            value={this.state.data.dateOfBirth}
                            onChange={this.createOnTextChangeHandler('dateOfBirth')}
                        />
                    </div>
                    
                    <div className="form-group">
                        <label htmlFor="place-of-birth-input">Miejsce urodzenia</label>
                        <input
                            type="text"
                            className="form-control"
                            name="place-of-birth-input"
                            value={this.state.data.placeOfBirth}
                            onChange={this.createOnTextChangeHandler('placeOfBirth')}
                        />
                    </div>
                    
                    <div className="form-group">
                        <label htmlFor="personal-id-input">Numer identyfikacyjny (np. PESEL)</label>
                        <input
                            type="text"
                            className="form-control"
                            name="personal-id-input"
                            value={this.state.data.personalId}
                            onChange={this.createOnTextChangeHandler('personalId')}
                        />
                    </div>
                    
                    <div className="form-group">
                        <label htmlFor="address-input">Adres zamieszkania</label>
                        <input
                            type="text"
                            className="form-control"
                            name="address-input"
                            value={this.state.data.address}
                            onChange={this.onAddressChange}
                        />
                    </div>

                    <div className="container">
                        <div className="row align-items-start">
                            {this.parentInputs(this.state.data.firstParent, "firstParent")}
                            {this.parentInputs(this.state.data.secondParent, "secondParent")}
                        </div>
                    </div>

                    <div className="form-group">
                        <input
                            type="submit"
                            value="Zapisz"
                            className="form-control"
                        />
                    </div>
                </form>
            </div>
        )
    }



    createParentOnTextChangeHandler: (parent: "firstParent" | "secondParent", property: keyof ParentRegisterSubecordDetails) => React.ChangeEventHandler<HTMLInputElement> = (parent, property) => {
        return (event) => {
            const value = event.target.value;

            this.setState(prevState => {
                const data = { ...prevState.data };

                if (data[parent] == undefined)
                    data[parent] = EmptyParentRegisterSubrecordDetails();

                data[parent][property] = (value as unknown) as never;

                return { data };
            });

            this.props.onMadeAnyChange();
        }
    }

    createOnAddressSameAsChildsChangeHandler: (parent: "firstParent" | "secondParent") => React.ChangeEventHandler<HTMLInputElement> = (parent) => {
        return (event) => {
            const value = event.target.checked;

            this.setState(prevState => {
                const data = { ...prevState.data };
                const addressSameAsChilds = { ...prevState.addressSameAsChilds };

                addressSameAsChilds[parent] = value;

                if (value) {
                    data[parent].address = data.address;
                }

                return { data, addressSameAsChilds };
            });

            this.props.onMadeAnyChange();
        }
    }

    removeSecondParent = () => {
        this.setState(prevState => {
            const data = { ...prevState.data };
            data.secondParent = undefined;
            return { data };
        });
    }

    addSecondParent = () => {
        this.setState(prevState => {
            const data = { ...prevState.data };
            data.secondParent = EmptyParentRegisterSubrecordDetails();
            return { data };
        });
    }


    parentInputs = (
        parent: ParentRegisterSubecordDetails,
        parentProp: "firstParent" | "secondParent"
    ): JSX.Element => {
        if (parent == undefined)
            return (
                <>
                    <button
                        onClick={this.addSecondParent}
                    >
                        Dodaj drugiego rodzica
                    </button>
                </>
            );

        return (
            <div className="col">
                <h3>Rodzic #{parentProp == "firstParent" ? 1 : 2}</h3>
                {parentProp == "firstParent" ? <></>
                    : (
                        <button
                            onClick={this.removeSecondParent}
                        >
                            Usuń
                        </button>)}
                
                <div className="form-group">
                    <label htmlFor={`${parentProp}-first-name-input`}>Imię</label>
                    <input
                        type="text"
                        className="form-control"
                        name={`${parentProp}-first-name-input`}
                        value={parent.firstName}
                        onChange={this.createParentOnTextChangeHandler(parentProp, 'firstName')}
                    />
                </div>
                
                <div className="form-group">
                    <label htmlFor={`${parentProp}-second-name-input`}>Drugie imię</label>
                    <input
                        type="text"
                        className="form-control"
                        name={`${parentProp}-second-name-input`}
                        value={parent.secondName}
                        onChange={this.createParentOnTextChangeHandler(parentProp, 'secondName')}
                    />
                </div>
                
                <div className="form-group">
                    <label htmlFor={`${parentProp}-last-name-input`}>Nazwisko</label>
                    <input
                        type="text"
                        className="form-control"
                        name={`${parentProp}-last-name-input`}
                        value={parent.lastName}
                        onChange={this.createParentOnTextChangeHandler(parentProp, 'lastName')}
                    />
                </div>
                
                <div className="form-group">
                    <label htmlFor={`${parentProp}-address-input`}>Adres zamieszkania</label>
                    <input
                        type="text"
                        className="form-control"
                        name={`${parentProp}-address-input`}
                        value={parent.address}
                        disabled={this.state.addressSameAsChilds[parentProp]}
                        onChange={this.createParentOnTextChangeHandler(parentProp, 'address')}
                    />
                </div>

                <div className="form-check">
                    <label htmlFor={`${parentProp}-address-same-as-childs-input`}>Adres taki sam jak ucznia</label>
                    <input
                        type="checkbox"
                        className="form-check-input"
                        name={`${parentProp}-address-same-as-childs-input`}
                        checked={this.state.addressSameAsChilds[parentProp]}
                        onChange={this.createOnAddressSameAsChildsChangeHandler(parentProp)}
                    />
                </div>
                
                <div className="form-group">
                    <label htmlFor={`${parentProp}-phone-number-input`}>Numer telefonu</label>
                    <input
                        type="text"
                        className="form-control"
                        name={`${parentProp}-phone-number-input`}
                        value={parent.phoneNumber}
                        onChange={this.createParentOnTextChangeHandler(parentProp, 'phoneNumber')}
                    />
                </div>
                
                <div className="form-group">
                    <label htmlFor={`${parentProp}-email-input`}>Email</label>
                    <input
                        type="text"
                        className="form-control"
                        name={`${parentProp}-email-input`}
                        value={parent.email}
                        onChange={this.createParentOnTextChangeHandler(parentProp, 'email')}
                    />
                </div>
            </div>
        )
    }
}

function EmptyParentRegisterSubrecordDetails(): ParentRegisterSubecordDetails {
    return {
        firstName: '',
        secondName: '',
        lastName: '',
        address: '',
        phoneNumber: '',
        email: ''
    };
}