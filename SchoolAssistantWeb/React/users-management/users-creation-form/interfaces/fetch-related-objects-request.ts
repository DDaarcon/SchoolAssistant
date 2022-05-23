import UserTypeForManagement from "../../enums/user-type-for-management";

export default interface FetchRelatedObjectsRequest {
    skip?: number;
    take?: number;
    ofType: UserTypeForManagement;
}