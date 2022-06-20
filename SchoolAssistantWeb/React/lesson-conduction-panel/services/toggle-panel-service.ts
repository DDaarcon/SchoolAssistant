import BackgroundArea from "../components/background-area";
import Panel from "../panel/panel";

class TogglePanelServiceImplementation {

    private _backgroundArea?: BackgroundArea;
    public registerBackgoundArea(component: BackgroundArea) {
        this.registerComponent(component, "_backgroundArea");
    }

    private _panel?: Panel;
    public registerPanel(component: Panel) {
        this.registerComponent(component, "_panel");
    }













    private registerComponent<TComponent>(component: TComponent, fieldName: string) {
        if (this[fieldName])
            throw new Error(`Component ${fieldName} is already registered and can not be registered twice`);
        this[fieldName] = component;
    }
}

const TogglePanelService = new TogglePanelServiceImplementation;
export default TogglePanelService;