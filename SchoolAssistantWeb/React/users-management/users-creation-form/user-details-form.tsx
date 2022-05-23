﻿import React from "react";
import { Input } from "../../shared/form-controls";
import ModCompBase from "../../shared/form-controls/mod-comp-base";
import UserTypeForManagement from "../enums/user-type-for-management";
import AddUserRequest from "./interfaces/add-user-request";
import SimpleRelatedObject from "./interfaces/simple-related-object";

type UserDetailsFormProps = {
    type: UserTypeForManagement;
    object: SimpleRelatedObject;
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
                email: this.props.object.email,
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
        return (
            <form onSubmit={this.submitAsync}>

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

            </form>
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


    }
}