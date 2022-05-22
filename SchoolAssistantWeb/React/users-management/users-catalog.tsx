import React from "react"
import UserTypeForManagement from "./enums/user-type-for-management"
import Controls from "./users-catalog/controls"
import UsersList from "./users-catalog/users-list"

type UsersCatalogProps = {
    initialType?: UserTypeForManagement;
}
type UsersCatalogState = {
    type: UserTypeForManagement;
}

export default class UsersCatalog extends React.Component<UsersCatalogProps, UsersCatalogState> {

    constructor(props) {
        super(props);

        this.state = {
            type: this.props.initialType ?? UserTypeForManagement.Teacher
        }
    }

    render() {
        return (
            <div className="whole-page">
                <Controls
                    changeUsersType={this.changeUserType}
                />

                <UsersList
                    type={this.state.type}
                />
            </div>
        )
    }


    private changeUserType = (type: UserTypeForManagement) => {
        if (type != this.state.type)
            this.setState({ type });
    }
}