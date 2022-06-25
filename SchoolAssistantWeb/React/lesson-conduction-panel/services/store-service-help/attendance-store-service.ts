import LessonConductionPanelModel from "../../interfaces/lesson-conduction-panel-model";
import AttendanceEditModel from "../../panel/panel-content-area/attendance-edition/attendance-edit-model";
import BaseSpecificStoreService from "./base-specific-store-service";

export default class AttendanceStoreService extends BaseSpecificStoreService<AttendanceEditModel> {
    constructor(getMainModel: () => LessonConductionPanelModel) {
        super(getMainModel);
    }

    public anyChangesToMainModel(): boolean {
        if (!this.areMainAndSpecificModelPresent())
            return false;

        const main = this._getMainModel();

        let diff = main.students.some(x => {
            const inModel = this._model.students.find(y => y.id == x.id);
            return inModel?.presence != x.presence;
        });

        return diff;
    }

    public applyToMain(): void {
        const main = this._getMainModel();

        for (const student of main.students) {
            student.presence = this._model.students.find(x => x.id == student.id)?.presence;
        }
    }


    protected get _defaultModel(): AttendanceEditModel {
        const main = this._getMainModel();

        return {
            id: main.lessonId,
            students: main.students.map(x => ({
                id: x.id,
                presence: x.presence
            }))
        };
    }

}