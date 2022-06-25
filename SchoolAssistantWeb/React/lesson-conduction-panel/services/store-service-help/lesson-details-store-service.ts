import LessonConductionPanelModel from "../../interfaces/lesson-conduction-panel-model";
import LessonDetailsEditModel from "../../panel/panel-content-area/lesson-details-edition/lesson-details-edit-model";
import BaseSpecificStoreService from "./base-specific-store-service";

export default class LessonDetailsStoreService extends BaseSpecificStoreService<LessonDetailsEditModel> {

    constructor(
        getMainModel: () => LessonConductionPanelModel)
    {
        super(getMainModel)
    }


    protected get _defaultModel() {
        const main = this._mainModel;
        return {
            id: main.lessonId,
            topic: main.topic ?? ""
        }
    }

    public anyChangesToMainModel() {
        if (!this.areMainAndSpecificModelPresent())
            return false;

        const main = this._getMainModel();

        let diff = main.topic != this._model.topic;

        return diff;
    }

    public applyToMain() {
        if (!this.areMainAndSpecificModelPresent())
            return;

        this._mainModel.topic = this._model.topic;
        this._model = undefined;
    }
}