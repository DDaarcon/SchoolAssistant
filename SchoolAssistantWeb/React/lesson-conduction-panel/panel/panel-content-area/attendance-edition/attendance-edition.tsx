import React from "react";
import PresenceStatus from "../../../enums/presence-status";
import ParticipatingStudentModel from "../../../interfaces/participating-student-model";
import StoreAndSaveService from "../../../services/store-and-save-service";
import './attendance-edition.css';
import AttendanceEntry from "./attendance-entry";

type AttendanceEditionProps = {}
type AttendanceEditionState = {
    data: ParticipatingStudentModel[];
}

export default class AttendanceEdition extends React.Component<AttendanceEditionProps, AttendanceEditionState> {

    constructor(props) {
        super(props);

        this.state = {
            data: StoreAndSaveService.students
        }
    }

    render() {
        return (
            <div className="content-with-students-container">
                {StoreAndSaveService.students.map(student => (
                    <AttendanceEntry
                        key={student.id}
                        selectPresence={this.onPresenceSelected}
                        {...student}
                    />
                ))}
            </div>
        )
    }

    private onPresenceSelected = (studentId: number, presence: PresenceStatus) => {
        this.setState(state => {
            const data = [...state.data];
            const student = data.find(x => x.id == studentId);

            if (student) student.presence = presence;
            return { data }
        })
    }
}