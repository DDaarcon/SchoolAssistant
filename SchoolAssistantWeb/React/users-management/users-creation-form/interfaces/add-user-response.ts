import { ResponseJson } from "../../../shared/server-connection";

export default interface AddUserResponse extends ResponseJson {
    passwordDeformed?: string;
}