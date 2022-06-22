import React from "react";
import { Input, SubmitButton } from "../../shared/form-controls";
import ModCompBase from "../../shared/form-controls/mod-comp-base";
import TopBar from "../../shared/top-bar";
import UserTypeForManagement from "../enums/user-type-for-management";
import AddUserRequest from "./interfaces/add-user-request";
import AddUserResponse from "./interfaces/add-user-response";
import CreatedUserInfo from "./interfaces/created-user-info";
import SimpleRelatedObject from "./interfaces/simple-related-object";
import serverCreationForm from "./server-creation-form";

type UserDetailsFormProps = {
    type: UserTypeForManagement;
    object: SimpleRelatedObject;

    changePage: (createdUser?: CreatedUserInfo) => void;
    backToObjectSelect: () => void;

}
type UserDetailsFormState = {
    data: AddUserRequest;
}

export default class UserDetailsForm extends ModCompBase<AddUserRequest, UserDetailsFormProps, UserDetailsFormState> {

    constructor(props) {
        super(props);

        this.state = {
            data: {
                userName: '',
                email: this.props.object.email ?? '',
                relatedType: this.props.type,
                relatedId: this.props.object.id
            }
        }

        this._validator.setRules({
            userName: {
                notNull: "Należy podać nazwę użytkownika",
                notEmpty: "Należy podać nazwę użytkownika",
                other: (model, prop) => {
                    // TODO: Check if userName is taken
                    return undefined;
                }
            },
            email: {
                notNull: "Należy podać adres email",
                notEmpty: "Należy podać adres email",
                other: (model, prop) => {
                    // TODO: Check if email has a valid form
                    return undefined;
                }
            }
        })
    }

    render() {
        TopBar.Ref.setGoBackAction(this.props.backToObjectSelect);

        return (
            <form onSubmit={this.submitAsync}>

                {this.renderRelatedObjectInfo()}

                <Input
                    label="Nazwa użytkownika"
                    name="username-input"
                    value={this.state.data.userName}
                    onChange={this.changeUserName}
                    errorMessages={this._validator.getErrorMsgsFor('userName')}
                    type="text"
                />

                <Input
                    label="Adres email"
                    name="email-input"
                    value={this.state.data.email}
                    onChange={this.changeEmail}
                    errorMessages={this._validator.getErrorMsgsFor('email')}
                    type="email"
                />

                <Input
                    label="Numer telefonu"
                    name="phone-number-input"
                    value={this.state.data.phoneNumber}
                    onChange={this.changePhoneNumber}
                    errorMessages={this._validator.getErrorMsgsFor('phoneNumber')}
                    type="tel"
                />

                <SubmitButton
                    value="Zapisz"
                />
            </form>
        )
    }

    private renderRelatedObjectInfo = () => {
        const info = Object.keys(this.props.object).map(key =>
            key == 'id' || key == 'email' || key == 'firstName' || key == 'lastName' ? '' : this.props.object[key])
            .filter(x => x);


        return (
            <div className="related-object-info">
                <p>
                    Tworzysz użytkonika dla osoby:
                </p>
                <h3>
                    {this.props.object.lastName} {this.props.object.firstName}
                </h3>
                <p>
                    {info.join(', ')}
                </p>
            </div>
        )
    }


    private changeUserName: React.ChangeEventHandler<HTMLInputElement> = (ev) => {
        const value = ev.target.value;

        this.setStateFnData(data => data.userName = value);
    }

    private changeEmail: React.ChangeEventHandler<HTMLInputElement> = (ev) => {
        const value = ev.target.value;

        this.setStateFnData(data => data.email = value);
    }

    private changePhoneNumber: React.ChangeEventHandler<HTMLInputElement> = (ev) => {
        const value = ev.target.value;

        this.setStateFnData(data => data.phoneNumber = value);
    }


    private submitAsync: React.FormEventHandler<HTMLFormElement> = async (e) => {
        e.preventDefault();

        if (!this._validator.validate()) {
            this.forceUpdate();
            return;
        }

        var res = await serverCreationForm.postAsync<AddUserResponse>("AddUser", undefined, this.state.data);

        if (res.success) {
            this.props.changePage({
                firstName: this.props.object.firstName,
                lastName: this.props.object.lastName,
                userName: this.state.data.userName,
                email: this.state.data.email,
                passwordDeformed: res.passwordDeformed
            });
        }
        else
            console.debug(res.message);
    }
}