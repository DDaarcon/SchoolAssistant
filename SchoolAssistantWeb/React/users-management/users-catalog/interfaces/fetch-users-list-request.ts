import UserTypeForManagement from "../../enums/user-type-for-management";

export default interface FetchUsersListRequest {
    skip?: number;
    take?: number;
    ofType: UserTypeForManagement;
}