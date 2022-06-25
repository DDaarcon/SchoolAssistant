import LessonConductionPanelModel from "../interfaces/lesson-conduction-panel-model";
import AttendanceStoreService from "./store-service-help/attendance-store-service";
import LessonDetailsStoreService from "./store-service-help/lesson-details-store-service";


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


    private _lessonDetailsSvc: LessonDetailsStoreService = new LessonDetailsStoreService(() => this._model);
    public get lessonDetailsSvc() { return this._lessonDetailsSvc; }

    private _attendanceSvc: AttendanceStoreService = new AttendanceStoreService(() => this._model);
    public get attendanceSvc() { return this._attendanceSvc; }

}
const StoreService = new StoreServiceImplementation;
export default StoreService;