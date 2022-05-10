import { modalController } from "../../../shared/modals";

const confirmCloseMod = async () => {
    return new Promise<boolean>(resolve => {
        modalController.addConfirmation({
            header: "Przerywanie edycji",
            text: "Zakończyć edycję? Wprowadzone zmiany zostaną utracone.",
            onConfirm: () => resolve(true),
            onDecline: () => resolve(false)
        });
    })
}
export default confirmCloseMod;