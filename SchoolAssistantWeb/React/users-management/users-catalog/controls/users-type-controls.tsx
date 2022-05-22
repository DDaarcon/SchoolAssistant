import React from "react"
import UserTypeForManagement from "../../enums/user-type-for-management";
import { getEnabledUserTypes, getLabelForUserType } from "../../help/user-type-functions";

type UsersTypeControlsProps = {
    changeUsersType: (type: UserTypeForManagement) => void;
}
type UsersTypeControlsState = {

}

export default class UsersTypeControls extends React.Component<UsersTypeControlsProps, UsersTypeControlsState> {

    private _enabledTypes: UserTypeForManagement[];

    constructor(props) {
        super(props);

        this._enabledTypes = getEnabledUserTypes();
    }

    render() {
        return (
            <div className="users-type-controls">
                {this._enabledTypes.map(type => 
                    <button
                        key={type}
                        className="user-type-btn"
                        onClick={() => this.props.changeUsersType(type)}
                    >
                        {getLabelForUserType(type)}
                    </button>
                )}
            </div>
        )
    }
}