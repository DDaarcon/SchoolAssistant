import LessonPrefab from "../../interfaces/lesson-prefab";

class PlacingAssistantServiceImplementation {

    private _currentyPlaced?: LessonPrefab;

    private _method?: PlacingMethod;

    public handlers: Handlers = { }



    public startWithDrag(prefab: LessonPrefab) {
        this.clear();

        this._currentyPlaced = prefab;
        this._method = PlacingMethod.Dragging;

        this.callRequiredHandler('showOtherLessons');
    }


    public startWithSelect(prefab: LessonPrefab, deselect: () => void) {
        if (prefab == this._currentyPlaced) {
            this.dismiss();
            return;
        }
        this.clear();

        this._currentyPlaced = prefab;
        this._method = PlacingMethod.Selecting;
        this.handlers.deselectPrefabTile = deselect;

        this.callRequiredHandler('showOtherLessons');
    }


    public dismiss() {
        if (!this.isPlacing)
            return;

        this.clear();

        dispatchEvent(new Event("hideLessonShadow"));
        this.callRequiredHandler('hideOtherLessons');
    }




    public get isPlacing() {
        return this._method != undefined
            && this._currentyPlaced != undefined;
    }
    public get isPlacingByDrag() {
        return this.isPlacing && this._method == PlacingMethod.Dragging;
    }
    public get isPlacingBySelection() {
        return this.isPlacing && this._method == PlacingMethod.Selecting;
    }


    public get prefab() { return this._currentyPlaced; }



    public getPrefabAndDismiss() {
        const prefab = this._currentyPlaced;
        this.dismiss();
        return prefab;
    }





    private callRequiredHandler(prop: keyof Handlers) {
        if (!this.handlers[prop])
            throw new Error(`Handler ${prop} is required`);

        this.handlers[prop]();
    }




    private clear() {
        if (!this.isPlacing)
            return;

        if (this.isPlacingBySelection) {
            this.handlers.deselectPrefabTile?.();
            this.handlers.deselectPrefabTile = undefined;
        }

        this._currentyPlaced = undefined;
        this._method = undefined
    }
}

const PlacingAssistantService = new PlacingAssistantServiceImplementation;
export default PlacingAssistantService;





interface Handlers {
    showOtherLessons?: () => void;
    hideOtherLessons?: () => void;
    deselectPrefabTile?: () => void;
}





enum PlacingMethod {
    Dragging,
    Selecting
}