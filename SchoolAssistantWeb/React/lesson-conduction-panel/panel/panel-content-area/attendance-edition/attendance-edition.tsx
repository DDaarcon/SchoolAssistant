import React from "react";
import server from "../../../../scheduled-lessons-list/server";
import ModCompBase from "../../../../shared/form-controls/mod-comp-base";
import { ResponseJson } from "../../../../shared/server-connection";
import PresenceStatus from "../../../enums/presence-status";
import ParticipatingStudentModel from "../../../interfaces/participating-student-model";
import SaveButtonService from "../../../services/save-button-service";
import StoreService from "../../../services/store-service";
import AttendanceEditModel from "./attendance-edit-model";
import './attendance-edition.css';
import AttendanceEntry from "./attendance-entry";

type AttendanceEditionProps = {}
type AttendanceEditionState = {
    data: AttendanceEditModel;
}

export default class AttendanceEdition extends ModCompBase<AttendanceEditModel, AttendanceEditionProps, AttendanceEditionState> {

    constructor(props) {
        super(props);

        this.state = {
            data: StoreService.attendanceSvc.model
        }

        SaveButtonService.setAction(this.submitAsync);
    }

    render() {
        return (
            <div className="attendance-edition-list">
                {StoreService.students.map((student, index) => {
                    let { presence, ...rest } = student;
                    presence = this.state.data.students.at(index)?.presence;

                    return (
                        <AttendanceEntry
                            key={student.id}
                            selectPresence={this.onPresenceSelected}
                            presence={presence}
                            {...rest}
                        />
                    )
                })}
            </div>
        )
    }

    componentWillUpdate() {
        StoreService.attendanceSvc.update(this.state.data);

        SaveButtonService.change(StoreService.attendanceSvc.anyChangesToMainModel());
    }

    private onPresenceSelected = (studentId: number, presence: PresenceStatus) => {
        this.setStateFnData(data => {

            const student = data.students.find(x => x.id == studentId);
            if (student) student.presence = presence;
        });
    }

    private submitAsync = async () => {


        const res = await server.postAsync<ResponseJson>("Attendance", {}, this.state.data);

        // TODO: handle server errors
        if (res.success) {
            StoreService.lessonDetailsSvc.applyToMain();
            SaveButtonService.hide();
        }
        else
            console.debug(res.message);
    }
}