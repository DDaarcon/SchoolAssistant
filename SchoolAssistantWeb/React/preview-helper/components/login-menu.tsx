import React from "react";
import { LabelValue } from "../../shared/form-controls";
import './login-menu.css';

const LoginMenu = (props: {}) => {
    return (
        <div className="ph-login-menu">
            <p className="ph-login-menu-header">Dane logowania administratora</p>
            <LabelValue
                label="Nazwa użytkownika"
                value="SuperAdmin"
                width={260}
            />
            <LabelValue
                label="Hasło"
                value="#ndusian22123"
                width={260}
            />

            <p className="ph-login-menu-header">Dane logowania przykładowego ucznia</p>
            <LabelValue
                label="Nazwa użytkownika"
                value="Uczeńńń"
                width={260}
            />
            <LabelValue
                label="Hasło"
                value="HasłoUcznia"
                width={260}
            />

            <p className="ph-login-menu-header">Dane logowania przykładowego nauczyciela</p>
            <LabelValue
                label="Nazwa użytkownika"
                value="NauczycielLala"
                width={260}
            />
            <LabelValue
                label="Hasło"
                value="TajneHasłoNuczyciela"
                width={260}
            />
        </div>
    )
}
export default LoginMenu;