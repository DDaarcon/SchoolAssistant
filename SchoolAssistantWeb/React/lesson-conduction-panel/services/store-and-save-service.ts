import LessonConductionPanelModel from "../interfaces/lesson-conduction-panel-model";
import LessonDetailsEditModel from "../panel/panel-content-area/lesson-details-edition/lesson-details-edit-model";


class StoreServiceImplementation {

    private _model?: LessonConductionPanelModel;
    public assignModel(model: LessonConductionPanelModel) {
        this._model = model;
    }


    public get students() { return this._model?.students; }

    private _startTimeBackingField?: Date;
    public get startTime() {
        this._startTimeBackingField ??= new Date(this._model?.startTimeTk);
        return this._startTimeBackingField;
    }


    public get lessonId() { return this._model?.lessonId; }

    public get duration() { return this._model?.duration; }

    public get topic() { return this._model?.topic; }

    public get className() { return this._model?.className; }

    public get subjectName() { return this._model?.subjectName; }

    public updateDetails(model: LessonDetailsEditModel) {
        if (!this._model) return;

        this._model.topic = model.topic;
    }
}
const StoreService = new StoreServiceImplementation;
export default StoreService;