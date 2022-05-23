import React from "react";
import { isValidEnumValue } from "../shared/enum-help";
import UserTypeForManagement from "./enums/user-type-for-management";
import SimpleRelatedObject from "./users-creation-form/interfaces/simple-related-object";
import RelatedObjectSelector from "./users-creation-form/related-object-selector";
import UserDetailsForm from "./users-creation-form/user-details-form";
import UserTypeSelector from "./users-creation-form/user-type-selector";

type UsersCreationFormProps = {}
type UsersCreationFormState = {
    selectedType?: UserTypeForManagement;
    selectedObject?: SimpleRelatedObject;
}

export default class UsersCreationForm extends React.Component<UsersCreationFormProps, UsersCreationFormState> {

    render() {
        return (
            <div className="whole-page">
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

        if (this.state.selectedObject == undefined)
            return (
                <RelatedObjectSelector
                    type={this.state.selectedType}
                    selectRelatedObject={this.selectRelatedObject}
                />
            )

        return (
            <UserDetailsForm
                type={this.state.selectedType}
                object={this.state.selectedObject}
            />
        )
    }

    private selectType = (type: UserTypeForManagement) => {
        if (!isValidEnumValue(UserTypeForManagement, type))
            return;

        this.setState({ selectedType: type, selectedObject: undefined });
    }

    private selectRelatedObject = (obj: SimpleRelatedObject) => {

        this.setState({ selectedObject: obj });
    }
}