import React from "react";
import UserTypeForManagement from "../enums/user-type-for-management";
import { getEnabledUserTypes, getLabelForUserType } from "../help/user-type-functions";
import './user-type-selector.css';

type UserTypeSelectorProps = {
    selectType: (type: UserTypeForManagement) => void;
}
type UserTypeSelectorState = {}

export default class UserTypeSelector extends React.Component<UserTypeSelectorProps, UserTypeSelectorState> {

    private _enabledTypes: UserTypeForManagement[];

    constructor(props) {
        super(props);

        this._enabledTypes = getEnabledUserTypes();
    }

    render() {
        return (
            <div className="user-type-selector">

                {this._enabledTypes.map(type => 
                    <button
                        key={type}
                        className="user-type-btn tiled-btn"
                        onClick={() => this.props.selectType(type)}
                    >
                        {getLabelForUserType(type)}
                    </button>
                )}

            </div>
        )
    }
}