import React from "react";
import ParticipatingStudentModel from "../../../interfaces/participating-student-model";
import './student-selection-entry.css';

type StudentSelectionEntryProps = ParticipatingStudentModel & {
    selected: boolean;
    select: (studentId: number) => void;
}
type StudentSelectionEntryState = {}

export default class StudentSelectionEntry extends React.Component<StudentSelectionEntryProps, StudentSelectionEntryState> {

    render() {
        return (
            <button className={"student-selection-entry " + (this.props.selected ? "student-selection-entry-selected" : "")}
                type="button"
                onClick={() => this.props.select(this.props.id)}
            >
                <span>
                    {this.props.numberInJournal}
                </span>
                <span>
                    {this.props.lastName} {this.props.firstName}
                </span>
            </button>
        )
    }
}