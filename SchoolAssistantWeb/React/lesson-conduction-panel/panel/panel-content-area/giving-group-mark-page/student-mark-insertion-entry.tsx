import React from "react";
import ParticipatingStudentModel from "../../../interfaces/participating-student-model";
import MarkInput from "../../../marks/mark-input";
import MarkModel from "../../../marks/mark-model";
import './student-mark-insertion-entry.css';

type StudentMarkInsertionEntryProps = ParticipatingStudentModel & {
    mark?: MarkModel;
    onChange: (mark?: MarkModel) => void;
}
type StudentMarkInsertionEntryState = {}

export default class StudentMarkInsertionEntry extends React.Component<StudentMarkInsertionEntryProps, StudentMarkInsertionEntryState> {

    render() {
        return (
            <div className={"student-mark-insertion-entry " + (this._insertedMark ? "student-mark-insertion-entry-inserted" : "")}
            >
                <span>
                    {this.props.numberInJournal}
                </span>

                <span>
                    {this.props.lastName} {this.props.firstName}
                </span>

                <MarkInput
                    mark={this.props.mark}
                    onChange={this.props.onChange}
                    containerClassName="student-mark-insertion-input-container"
                    inputClassName="student-mark-insertion-input"
                />
            </div>
        )
    }

    private get _insertedMark() { return this.props.mark?.value != undefined; }
}