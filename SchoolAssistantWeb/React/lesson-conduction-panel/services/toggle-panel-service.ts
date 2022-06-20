import BackgroundArea from "../components/background-area";
import Panel from "../panel/panel";

class TogglePanelServiceImplementation {

    private _backgroundArea?: BackgroundArea;
    public registerBackgoundArea(component: BackgroundArea) {
        this._backgroundArea = component;
    }

    private _panel?: Panel;
    public registerPanel(component: Panel) {
        this._panel = component;
    }


    public toggle() {
        if (this._panel.isShown) {
            this._panel.hide();
            this._backgroundArea.hide();
        }
        else {
            this._panel.show();
            this._backgroundArea.show();
        }
    }







}

const TogglePanelService = new TogglePanelServiceImplementation;
export default TogglePanelService;