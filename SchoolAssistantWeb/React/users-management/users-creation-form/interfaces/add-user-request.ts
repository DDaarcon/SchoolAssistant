import UserTypeForManagement from "../../enums/user-type-for-management";

export default interface AddUserRequest {
    userName: string;
    email: string;
    phoneNumber?: string;

    relatedType: UserTypeForManagement;
    relatedId: number;
}