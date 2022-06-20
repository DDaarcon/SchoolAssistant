﻿import LessonConductionPanelModel from "../interfaces/lesson-conduction-panel-model";


class StoreAndSaveServiceImplementation {




    private _model?: LessonConductionPanelModel;
    public assignModel(model: LessonConductionPanelModel) {
        this._model = model;
    }


    public get students() { return this._model.students; }

    private _startTimeBackingField?: Date;
    public get startTime() {
        this._startTimeBackingField ??= new Date(this._model.startTimeTk);
        return this._startTimeBackingField;
    }

    public get duration() { return this._model.duration; }

    public get topic() { return this._model.topic; }


}
const StoreAndSaveService = new StoreAndSaveServiceImplementation;
export default StoreAndSaveService;