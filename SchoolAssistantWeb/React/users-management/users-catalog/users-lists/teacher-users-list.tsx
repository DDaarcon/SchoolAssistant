import UserTypeForManagement from "../../enums/user-type-for-management";
import server from "../../server";
import FetchUsersListRequest from "../interfaces/fetch-users-list-request";
import UserListEntry from "../interfaces/user-list-entry";
import UsersListBase from "./users-list-base";


export default class TeacherUsersList extends UsersListBase<UserListEntry> {

    protected loadAsync = async (): Promise<UserListEntry[]> => {
        const params: FetchUsersListRequest = {
            ofType: UserTypeForManagement.Teacher
        };
        let response = await server.getAsync<UserListEntry[]>("UserListEntries", params);
        return response;
    }

    render() {
        return super.render();
    }
}