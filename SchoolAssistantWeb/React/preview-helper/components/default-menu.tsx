import React from "react"
import { ActionButton } from "../../shared/components"
import './default-menu.css';


const DefaultMenu = (props: {}) => {

    const resetAppData = () => {
        console.log("reset data");
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