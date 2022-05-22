import { getEnumValues } from "../../shared/enum-help"
import UserTypeForManagement from "../enums/user-type-for-management";
import SETTINGS from "../settings";

export const getEnabledUserTypes = () => {
    const all = getEnumValues(UserTypeForManagement);
    return all.filter(x => SETTINGS.DisabledUserTypes.includes(x));
}


export const getLabelForUserType = (type: UserTypeForManagement) => {
    switch (type) {
        case UserTypeForManagement.Teacher: return "Nauczyciele";
        case UserTypeForManagement.Student: return "Uczniowie";
        case UserTypeForManagement.Parent: return "Rodzice";
        default: return "Label missing";
    }
}