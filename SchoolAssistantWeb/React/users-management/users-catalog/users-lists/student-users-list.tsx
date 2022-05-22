import ColumnSetting from "../../../shared/lists/interfaces/column-setting";
import UserTypeForManagement from "../../enums/user-type-for-management";
import server from "../../server";
import FetchUsersListRequest from "../interfaces/fetch-users-list-request";
import StudentUserListEntry from "../interfaces/student-user-list-entry";
import UsersListBase from "./users-list-base";


export default class StudentUsersList extends UsersListBase<StudentUserListEntry> {

    protected extraColumnsSettings?(): ColumnSetting<StudentUserListEntry>[] {
        return [
            {
                header: "Klasa",
                prop: "orgClass"
            }
        ]
    }

    protected loadAsync = async (): Promise<StudentUserListEntry[]> => {
        const params: FetchUsersListRequest = {
            ofType: UserTypeForManagement.Student
        };
        let response = await server.getAsync<StudentUserListEntry[]>("UserListEntries", params);
        return response;
    }

    render() {
        return super.render();
    }
}