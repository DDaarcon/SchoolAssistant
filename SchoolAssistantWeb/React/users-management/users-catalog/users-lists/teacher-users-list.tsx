import server from "../../server";
import UserListEntry from "../interfaces/user-list-entry";
import UsersListBase from "./users-list-base";


export default class TeacherUsersList extends UsersListBase<UserListEntry> {

    protected loadAsync = async (): Promise<UserListEntry[]> => {
        let response = await server.getAsync<UserListEntry[]>("ClassEntries");
        return response;
    }

    render() {
        return super.render();
    }
}