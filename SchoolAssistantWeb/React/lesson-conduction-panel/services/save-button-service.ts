import SaveButton from "../panel/components/save-button";

class SaveButtonServiceImplementance {

    private _button: SaveButton;
    public registerButton(button: SaveButton) {
        this._button = button;
    }


    public show() {
        this._button?.setState({ shown: true });
    }

    public hide() {
        this._button?.setState({ shown: false });
    }

    public get isShown() {
        return this._button.state.shown;
    }


    public setAction(action: () => void) {
        this._button?.setState({ onClick: action });
    }

    public clearAction() {
        this._button?.setState({ onClick: undefined });
    }
}
const SaveButtonService = new SaveButtonServiceImplementance;
export default SaveButtonService;