import UserTypeForManagement from "./enums/user-type-for-management";

const SETTINGS = {
    DisabledUserTypes: [
        UserTypeForManagement.Administration,
        UserTypeForManagement.Headmaster,
        UserTypeForManagement.Parent,
        UserTypeForManagement.SystemAdmin
    ]
}

export default SETTINGS;