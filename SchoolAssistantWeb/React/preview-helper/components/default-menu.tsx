import React from "react"
import { ActionButton } from "../../shared/components"
import { Loader } from "../../shared/loader";
import PageBlockingLoader from "../../shared/loader/page-blocking-loader";
import { modalController } from "../../shared/modals";
import ServerConnection from "../../shared/server-connection";
import './default-menu.css';


const DefaultMenu = (props: {}) => {

    const loaderRef = React.createRef<Loader>();

    const resetAppDataAsync = () => {

        modalController.addConfirmation({
            header: "Resetowanie wszystkich danych",
            text: "Potwierdzenie wiąże się z nieodwracalnym napisaniem wszystkich danych aplikacji",
            onConfirm: async () => {
                loaderRef.current.show();
                var res = await resetAppDataServer.getResponseAsync(null);
                loaderRef.current.hide();

                if (res.ok)
                    window.location.reload();
            }
        });

    }

    const createLessonNow = () => {

    }

    return (
        <div className="ph-default-menu">

            <ActionButton
                label="Zresetuj dane aplikacji"
                className="ph-default-menu-button"
                onClick={resetAppDataAsync}
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


            <PageBlockingLoader
                ref={loaderRef}
            />
        </div>
    )
}
export default DefaultMenu;

const resetAppDataServer = new ServerConnection("/ResetDatabsePreview");
const createLessonNow = new ServerConnection("/CreateLessonNowPreview");