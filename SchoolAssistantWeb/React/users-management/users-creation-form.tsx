import React from "react";
import { isValidEnumValue } from "../shared/enum-help";
import UserTypeForManagement from "./enums/user-type-for-management";
import CreatedUserInfo from "./users-creation-form/interfaces/created-user-info";
import SimpleRelatedObject from "./users-creation-form/interfaces/simple-related-object";
import RelatedObjectSelector from "./users-creation-form/related-object-selector";
import UserCreatedPage from "./users-creation-form/user-created-page";
import UserDetailsForm from "./users-creation-form/user-details-form";
import UserTypeSelector from "./users-creation-form/user-type-selector";

type UsersCreationFormProps = {}
type UsersCreationFormState = {
    selectedType?: UserTypeForManagement;
    selectedObject?: SimpleRelatedObject;
    createdUser?: CreatedUserInfo;
}

export default class UsersCreationForm extends React.Component<UsersCreationFormProps, UsersCreationFormState> {

    render() {
        return (
            <div className="whole-page">
                <button onClick={() => this.setState({
                    selectedType: UserTypeForManagement.Student,
                    createdUser: {
                        lastName: 'Dmowski',
                        firstName: 'Roman',
                        email: 'roma.dmow@gmail.com',
                        passwordDeformed: "xxxxxx",
                        userName: 'dmowski.roman'
                    }
                })}>

                </button>
                {this.renderContent()}
            </div>
        )
    }

    private renderContent(): JSX.Element {
        if (this.state?.selectedType == undefined)
            return (
                <UserTypeSelector
                    selectType={this.selectType}
                />
            )

        if (this.state.selectedObject == undefined && this.state.createdUser == undefined)
            return (
                <RelatedObjectSelector
                    type={this.state.selectedType}
                    selectRelatedObject={this.selectRelatedObject}
                />
            )

        if (this.state.createdUser == undefined)
            return (
                <UserDetailsForm
                    type={this.state.selectedType}
                    object={this.state.selectedObject}
                    changePage={this.redirectFromEdition}
                />
            )

        return (
            <UserCreatedPage
                user={this.state.createdUser}
                returnToSelector={this.returnToSelector}
            />
        )
    }

    private selectType = (type: UserTypeForManagement) => {
        if (!isValidEnumValue(UserTypeForManagement, type))
            return;

        this.setState({ selectedType: type, selectedObject: undefined });
    }

    private selectRelatedObject = (obj: SimpleRelatedObject) => this.setState({ selectedObject: obj });

    private redirectFromEdition = (createdUser?: CreatedUserInfo) =>
        this.setState({ selectedObject: undefined, createdUser });

    private returnToSelector = () =>
        this.setState({ createdUser: undefined });
}