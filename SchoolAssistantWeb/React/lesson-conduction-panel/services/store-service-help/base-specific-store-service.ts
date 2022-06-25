import LessonConductionPanelModel from "../../interfaces/lesson-conduction-panel-model";

export default abstract class BaseSpecificStoreService<TModel> {

    constructor(
        protected _getMainModel: () => LessonConductionPanelModel) {

    }

    public abstract anyChangesToMainModel(): boolean;
    protected abstract get _defaultModel(): TModel;
    public abstract applyToMain(): void;

    public get model() {
        this._model ??= this._defaultModel;
        return this._model;
    }



    public update(model: TModel) {
        this._model = model;
    }


    public abortChanges() {
        this._model = undefined;
    }


    protected areMainAndSpecificModelPresent() {
        const main = this._mainModel;

        return main != undefined && this._model != undefined;
    }



    protected get _mainModel() { return this._getMainModel(); }

    protected _model: TModel | undefined;
}