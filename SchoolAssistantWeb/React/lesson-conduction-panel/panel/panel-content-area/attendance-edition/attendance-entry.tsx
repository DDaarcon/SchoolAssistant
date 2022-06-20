import React from "react";
import PresenceStatus from "../../../enums/presence-status";
import ParticipatingStudentModel from "../../../interfaces/participating-student-model";
import './attendance-entry.css';

export type AttendanceEntryProps = ParticipatingStudentModel & {
    selectPresence: (studentId: number, presence: PresenceStatus) => void;
}
type AttendanceEntryState = {}

export default class AttendanceEntry extends React.Component<AttendanceEntryProps, AttendanceEntryState> {


    render() {
        return (
            <div className="attendance-entry">
                <span>
                    {this.props.numberInJournal}
                </span>
                <span>
                    {this.props.lastName} {this.props.firstName}
                </span>
                <div className="presence-options">
                    <a role="button"
                        className={this.getPresenceClassFor(PresenceStatus.Present)}
                        onClick={() => this.props.selectPresence(this.props.id, PresenceStatus.Present)}
                    >
                        Obecny
                    </a>
                    <a role="button"
                        className={this.getPresenceClassFor(PresenceStatus.Absent)}
                        onClick={() => this.props.selectPresence(this.props.id, PresenceStatus.Absent)}
                    >
                        Nieobecny
                    </a>
                    <a role="button"
                        className={this.getPresenceClassFor(PresenceStatus.Late)}
                        onClick={() => this.props.selectPresence(this.props.id, PresenceStatus.Late)}
                    >
                        Spóźniony
                    </a>
                </div>
            </div>
        )
    }

    private readonly SELECTED_PRESENCE = "selected-presence";
    private readonly NOT_SELECTED_PRESENCE = "not-selected-presence";
    private readonly NONE_PRESENCE_SELECTED = "none-presence-selected";

    private getPresenceClassFor(presence: PresenceStatus) {
        if (this.props.presence == undefined
            || this.props.presence == null)
            return this.NONE_PRESENCE_SELECTED;

        if (this.props.presence == presence)
            return this.SELECTED_PRESENCE;

        return this.NOT_SELECTED_PRESENCE;
    }

}