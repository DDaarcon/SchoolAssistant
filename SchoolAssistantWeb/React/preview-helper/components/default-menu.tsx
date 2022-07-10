import React from "react"
import { ActionButton } from "../../shared/components"
import ServerConnection from "../../shared/server-connection";
import './default-menu.css';


const DefaultMenu = (props: {}) => {

    const resetAppData = () => {
        resetAppDataServer.getAsync<void>(null);
    }

    const createLessonNow = () => {

    }

    return (
        <div className="ph-default-menu">

            <ActionButton
                label="Zresetuj dane aplikacji"
                className="ph-default-menu-button"
                onClick={resetAppData}
            />
            <p>
                Zresetuj przedmioty, nauczycieli, pomieszczenia, klasy, uczniów, oceny oraz zajęcia do stanu
                stanu początkowego.
            </p>

            <ActionButton
                label="Utwórz zajęcia na teraz"
                className="ph-default-menu-button"
                onClick={createLessonNow}
            />
            <p>
                Utwórz zajęcia dla przykładowego nauczyciela. Aby je poprowadzić zaloguj się na to konto i wybierz
                'Poprowadź zajęcia' z listy zajęć.
            </p>

        </div>
    )
}
export default DefaultMenu;

const resetAppDataServer = new ServerConnection("/ResetDatabsePreview");
const createLessonNow = new ServerConnection("/CreateLessonNowPreview");