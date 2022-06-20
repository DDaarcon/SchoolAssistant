import React from "react";
import ParticipatingStudentModel from "../../../interfaces/participating-student-model";
import StoreAndSaveService from "../../../services/store-and-save-service";
import './students-list.css';

type StudentsListProps<TEntryProps> = {
    entryComponent: React.ReactNode;
    studentToEntryProps: (student: ParticipatingStudentModel) => TEntryProps;
}
type StudentsListState = {}

export default class StudentsList<TEntryProps> extends React.Component<StudentsListProps<TEntryProps>, StudentsListState> {

    render() {
        return (
            <>
                {StoreAndSaveService.students.map(student => {
                    return React.Children.map(this.props.entryComponent, child => {

                        if (React.isValidElement(child))
                            return React.cloneElement(child, { ...this.props.studentToEntryProps(student) });

                        return child;
                    })
                })}
            </>
        )
    }
}