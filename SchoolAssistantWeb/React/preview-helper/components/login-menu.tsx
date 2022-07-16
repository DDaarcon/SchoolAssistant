import React from "react";
import { IconButton } from "../../shared/components";
import { LabelValue } from "../../shared/form-controls";
import PreviewLoginsModel from "../interfaces/preview-logins-model";
import './login-menu.css';

const LoginMenu = (props: {
    logins: PreviewLoginsModel;
}) => {
    return (
        <div className="ph-login-menu">
            <DisplayCredentials
                header="Dane logowania administratora"
                userName={props.logins.administratorUserName}
                password={props.logins.administratorPassword}
            />

            <DisplayCredentials
                header="Dane logowania przykładowego nauczyciela"
                userName={props.logins.teacherUserName}
                password={props.logins.teacherPassword}
            />

            <DisplayCredentials
                header="Dane logowania przykładowego ucznia"
                userName={props.logins.studentUserName}
                password={props.logins.studentPassword}
            />
        </div>
    )
}
export default LoginMenu;



const DisplayCredentials = ({ header, userName, password }: {
    header: string;
    userName?: string;
    password?: string;
}) => {
    if (!userName || !password)
        return <></>

    return (
        <>
            <p className="ph-login-menu-header">{header}</p>
            <LabelValue
                label="Nazwa użytkownika"
                value={
                    <CopyableValue
                        value={userName}
                    />
                }
                width={300}
            />
            <LabelValue
                label="Hasło"
                value={
                    <CopyableValue
                        value={password}
                    />
                }
                width={300}
            />
        </>
    )
}



const CopyableValue = ({ value }: {
    value: string;
}) => {

    const copyToClipboard = () => {
        navigator.clipboard.writeText(value);
    }

    return (
        <>
            <span>
                {value}
            </span>
            <IconButton
                onClick={copyToClipboard}
                faIcon="fa-solid fa-copy"
                label="Kopiuj do schowka"
                className="ph-copy-to-clipboard-btn"
            />
        </>
    )
}