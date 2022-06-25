import LessonConductionPanelModel from "../../interfaces/lesson-conduction-panel-model";
import LessonDetailsEditModel from "../../panel/panel-content-area/lesson-details-edition/lesson-details-edit-model";

export default class LessonDetailsStoreService {

    constructor(
        private _getMainModel: () => LessonConductionPanelModel)
    {

    }

    private get _mainModel() { return this._getMainModel(); }

    private _model: LessonDetailsEditModel | undefined;

    public get model() {
        this._model ??= this._defaultModel;
        return this._model;
    }

    private get _defaultModel() {
        const main = this._mainModel;
        return {
            id: main.lessonId,
            topic: main.topic ?? ""
        }
    }

    public anyChangesToMainModel() {
        const main = this._mainModel;
        if (!main || !this._model)
            return false;

        let diff = main.topic != this._model.topic;

        return diff;
    }

    public update(model: LessonDetailsEditModel) {
        this._model = model;
    }

    public applyToMain() {
        const main = this._mainModel;
        if (!main || !this._model)
            return;

        this._mainModel.topic = this._model.topic;
        this._model = undefined;
    }

    public abortChanges() {
        this._model = undefined;
    }
}