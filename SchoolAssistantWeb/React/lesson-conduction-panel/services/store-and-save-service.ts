import LessonConductionPanelModel from "../interfaces/lesson-conduction-panel-model";


class StoreAndSaveServiceImplementation {




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

}
const StoreAndSaveService = new StoreAndSaveServiceImplementation;
export default StoreAndSaveService;