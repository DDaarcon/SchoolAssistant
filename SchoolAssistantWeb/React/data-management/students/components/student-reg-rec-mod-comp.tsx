import React from "react";
import { Input } from "../../../shared/form-controls";
import Loader, { LoaderSize, LoaderType } from "../../../shared/loader";
import { CommonModalProps } from "../../../shared/modals/shared-modal-body";
import { SaveResponseJson } from "../../../shared/server-connection";
import Validator from "../../../shared/validator";
import { server } from "../../main";
import ParentRegisterSubecordDetails from "../interfaces/parent-reg-subrec-details";
import StudentRegisterRecordDetails from "../interfaces/student-reg-rec-details";
import StudentRegisterRecordModificationData from "../interfaces/student-reg-rec-modification-data";

export type StudentRegisterRecordModCompProps = {
    recordId?: number;
    selectRecord: (id: number) => void;
    reloadAsync: () => Promise<void>;
}
type StudentRegisterRecordModCompState = {
    data: StudentRegisterRecordDetails;
    awaitingData: boolean;
    addressSameAsChilds: {
        firstParent: boolean;
        secondParent: boolean;
    }
}
export default class StudentRegisterRecordModComp extends React.Component<StudentRegisterRecordModCompProps & CommonModalProps, StudentRegisterRecordModCompState> {
    private _validator = new Validator<StudentRegisterRecordDetails>();
    private _firstParentValidator?: Validator<ParentRegisterSubecordDetails>;
    private _secondParentValidator?: Validator<ParentRegisterSubecordDetails>;

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

        this._validator.forModelGetter(() => this.state.data);
        this._validator.setRules({
            firstName: { notNull: true, notEmpty: 'Pole nie może być puste' },
            lastName: { notNull: true, notEmpty: 'Pole nie może być puste' },
            address: { notNull: true, notEmpty: 'Pole nie może być puste' },
            dateOfBirth: { notNull: true, validDate: true },
            personalId: { notNull: true, notEmpty: 'Pole nie może być puste' },
            placeOfBirth: { notNull: true, notEmpty: 'Pole nie może być puste' },
            firstParent: {
                notNull: true,
                other: (model, prop) => {
                    if (!model[prop])
                        return undefined;

                    this._firstParentValidator = this._parentValidator;
                    this._firstParentValidator.forModel(model[prop]);
                    return this._firstParentValidator.validate()
                        ? undefined
                        : {
                            error: "Nieprawidłowe dane rodzica",
                            on: prop
                        };
                }
            },
            secondParent: {
                other: (model, prop) => {
                    if (!model[prop])
                        return undefined;

                    this._secondParentValidator = this._parentValidator;
                    this._secondParentValidator.forModel(model[prop]);
                    return this._secondParentValidator.validate()
                        ? undefined
                        : {
                            error: "Nieprawidłowe dane rodzica",
                            on: prop
                        };
                }
            }
        })

        if (this.props.recordId)
            this.fetchAsync();
    }

    private get _parentValidator() {
        const validator = new Validator<ParentRegisterSubecordDetails>();
        validator.setRules({
            firstName: { notNull: true, notEmpty: 'Pole nie może być puste' },
            lastName: { notNull: true, notEmpty: 'Pole nie może być puste' },
            address: { notNull: true, notEmpty: 'Pole nie może być puste' },
            phoneNumber: { notNull: true, notEmpty: 'Pole nie może być puste' }
        });
        return validator;
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
    }


    onSubmitAsync: React.FormEventHandler<HTMLFormElement> = async (event) => {
        event.preventDefault();

        if (!this._validator.validate()) {
            this.forceUpdate();
            return;
        }

        let response = await server.postAsync<SaveResponseJson>("StudentRegisterRecordData", undefined, {
            ...this.state.data
        });

        if (response.success) {
            this.props.selectRecord(response.id);
            //await this.props.reloadAsync();
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
                    <Input
                        name="first-name-input"
                        label="Imię"
                        value={this.state.data.firstName}
                        onChange={this.createOnTextChangeHandler('firstName')}
                        errorMessages={this._validator.getErrorMsgsFor('firstName')}
                        type="text"
                    />
                    <Input
                        name="second-name-input"
                        label="Drugie imię"
                        value={this.state.data.secondName}
                        onChange={this.createOnTextChangeHandler('secondName')}
                        errorMessages={this._validator.getErrorMsgsFor('secondName')}
                        type="text"
                    />
                    <Input
                        name="last-name-input"
                        label="Nazwisko"
                        value={this.state.data.lastName}
                        onChange={this.createOnTextChangeHandler('lastName')}
                        errorMessages={this._validator.getErrorMsgsFor('lastName')}
                        type="text"
                    />
                    <Input
                        name="date-of-birth-input"
                        label="Data urodzenia"
                        value={this.state.data.dateOfBirth}
                        onChange={this.createOnTextChangeHandler('dateOfBirth')}
                        errorMessages={this._validator.getErrorMsgsFor('dateOfBirth')}
                        type="date"
                    />
                    <Input
                        name="place-of-birth-input"
                        label="Miejsce urodzenia"
                        value={this.state.data.placeOfBirth}
                        onChange={this.createOnTextChangeHandler('placeOfBirth')}
                        errorMessages={this._validator.getErrorMsgsFor('placeOfBirth')}
                        type="text"
                    />
                    <Input
                        name="personal-id-input"
                        label="Numer identyfikacyjny (np. PESEL)"
                        value={this.state.data.personalId}
                        onChange={this.createOnTextChangeHandler('personalId')}
                        errorMessages={this._validator.getErrorMsgsFor('personalId')}
                        type="text"
                    />
                    <Input
                        name="address-input"
                        label="Adres zamieszkania"
                        value={this.state.data.address}
                        onChange={this.onAddressChange}
                        errorMessages={this._validator.getErrorMsgsFor('address')}
                        type="text"
                    />

                    <div className="container">
                        <div className="row align-items-start">
                            {this.parentInputs(this.state.data.firstParent, "firstParent", () => this._firstParentValidator)}
                            {this.parentInputs(this.state.data.secondParent, "secondParent", () => this._secondParentValidator)}
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
        parentProp: "firstParent" | "secondParent",
        validatorGetter: () => Validator<ParentRegisterSubecordDetails> | undefined
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
                        </button>
                    )
                }

                <Input
                    name={`${parentProp}-first-name-input`}
                    label="Imię"
                    value={parent.firstName}
                    onChange={this.createParentOnTextChangeHandler(parentProp, 'firstName') }
                    errorMessages={validatorGetter()?.getErrorMsgsFor('firstName')}
                    type="text"
                />
                <Input
                    name={`${parentProp}-second-name-input`}
                    label="Drugie imię"
                    value={parent.secondName}
                    onChange={this.createParentOnTextChangeHandler(parentProp, 'secondName') }
                    errorMessages={validatorGetter()?.getErrorMsgsFor('secondName')}
                    type="text"
                />
                <Input
                    name={`${parentProp}-last-name-input`}
                    label="Nazwisko"
                    value={parent.lastName}
                    onChange={this.createParentOnTextChangeHandler(parentProp, 'lastName') }
                    errorMessages={validatorGetter()?.getErrorMsgsFor('lastName')}
                    type="text"
                />
                <Input
                    name={`${parentProp}-address-input`}
                    label="Adres zamieszkania"
                    value={parent.address}
                    disabled={this.state.addressSameAsChilds[parentProp]}
                    onChange={this.createParentOnTextChangeHandler(parentProp, 'address') }
                    errorMessages={validatorGetter()?.getErrorMsgsFor('address')}
                    type="text"
                />
                <Input
                    inputClassName="form-check-input"
                    name={`${parentProp}-address-same-as-childs-input`}
                    label="Adres taki sam jak ucznia"
                    checked={this.state.addressSameAsChilds[parentProp]}
                    onChange={this.createOnAddressSameAsChildsChangeHandler(parentProp)}
                    type="checkbox"
                />
                <Input
                    name={`${parentProp}-phone-number-input`}
                    label="Numer telefonu"
                    value={parent.phoneNumber}
                    onChange={this.createParentOnTextChangeHandler(parentProp, 'phoneNumber')}
                    errorMessages={validatorGetter()?.getErrorMsgsFor('phoneNumber')}
                    type="text"
                />
                <Input
                    name={`${parentProp}-email-input`}
                    label="Email"
                    value={parent.email}
                    onChange={this.createParentOnTextChangeHandler(parentProp, 'email')}
                    errorMessages={validatorGetter()?.getErrorMsgsFor('email')}
                    type="text"
                />
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