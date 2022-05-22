import ColumnSetting from "../../../shared/lists/interfaces/column-setting";
import UserTypeForManagement from "../../enums/user-type-for-management";
import FetchUsersListRequest from "../interfaces/fetch-users-list-request";
import StudentUserListEntry from "../interfaces/student-user-list-entry";
import serverCatalog from "../server-catalog";
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
        let response = await serverCatalog.getAsync<StudentUserListEntry[]>("UserListEntries", params);
        return response;
    }

    render() {
        return super.render();
    }
}