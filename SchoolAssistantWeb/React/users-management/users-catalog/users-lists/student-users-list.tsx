import ColumnSetting from "../../../shared/lists/interfaces/column-setting";
import server from "../../server";
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
        let response = await server.getAsync<StudentUserListEntry[]>("ClassEntries");
        return response;
    }

    render() {
        return super.render();
    }
}