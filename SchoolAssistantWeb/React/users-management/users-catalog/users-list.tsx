import React from "react";
import UserTypeForManagement from "../enums/user-type-for-management";
import StudentUsersList from "./users-lists/student-users-list";
import TeacherUsersList from "./users-lists/teacher-users-list";

type UsersListProps = {
    type: UserTypeForManagement;
}
type UsersListState = {

}

export default class UsersList extends React.Component<UsersListProps, UsersListState> {

    render() {
        switch (this.props.type) {
            case UserTypeForManagement.Student:
                return <StudentUsersList />;
            case UserTypeForManagement.Teacher:
                return <TeacherUsersList />;
            default:
                return <></>
        }
    }
}