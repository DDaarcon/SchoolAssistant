import React from "react";
import UserTypeForManagement from "../enums/user-type-for-management";
import SimpleRelatedObject from "./interfaces/simple-related-object";
import { ParentObjectsList, StudentObjectsList, TeacherObjectsList } from "./related-object-selector/related-objects-lists-spec";

type RelatedObjectSelectorProps = {
    type: UserTypeForManagement;
    selectRelatedObject: (obj: SimpleRelatedObject) => void;
}
type RelatedObjectSelectorState = {}

export default class RelatedObjectSelector extends React.Component<RelatedObjectSelectorProps, RelatedObjectSelectorState> {

    render() {
        return (
            <div className="related-object-selector">
                {this.renderObjectsList()}
            </div>
        )
    }

    private renderObjectsList(): JSX.Element {
        switch (this.props.type) {
            case UserTypeForManagement.Student:
                return (
                    <StudentObjectsList
                        selectObject={this.props.selectRelatedObject}
                    />
                );
            case UserTypeForManagement.Teacher:
                return (
                    <TeacherObjectsList
                        selectObject={this.props.selectRelatedObject}
                    />
                );
            case UserTypeForManagement.Parent:
                return (
                    <ParentObjectsList
                        selectObject={this.props.selectRelatedObject}
                    />
                );
            default: throw new Error("Not implemented yet");
        }
    }
}