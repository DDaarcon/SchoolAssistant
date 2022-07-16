import React, { Children } from "react"
import { ActionButton } from "../../shared/components"
import { Loader } from "../../shared/loader";
import PageBlockingLoader from "../../shared/loader/page-blocking-loader";
import { modalController } from "../../shared/modals";
import ServerConnection from "../../shared/server-connection";
import './default-menu.css';


const DefaultMenu = (props: {
    children?: React.ReactNode;
}) => {

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

            {props.children}

            <PageBlockingLoader
                ref={loaderRef}
            />
        </div>
    )
}
export default DefaultMenu;

const resetAppDataServer = new ServerConnection("/ResetDatabsePreview");