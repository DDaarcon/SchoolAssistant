import React from "react"
import UserTypeForManagement from "../enums/user-type-for-management";
import UsersTypeControls from "./controls/users-type-controls";

type ControlsProps = {
    changeUsersType: (type: UserTypeForManagement) => void;
}
type ControlsState = {

}

export default class Controls extends React.Component<ControlsProps, ControlsState> {


    render() {
        return (
            <div className="users-list-controls">
                <UsersTypeControls
                    changeUsersType={this.props.changeUsersType}
                />
            </div>
        )
    }
}