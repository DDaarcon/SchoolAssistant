import React from "react";
import ClassInfoPanel from "./components/class-info-panel";
import StudentsList from "./student-list";
import './students.css';

export type StudentsPageProps = {
    classId: number;
    className: string;
    classSpecialization?: string;
}
type StudentsPageState = {

}
export default class StudentsPage extends React.Component<StudentsPageProps, StudentsPageState> {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <>
                <ClassInfoPanel
                    name={this.props.className}
                    specialization={this.props.classSpecialization}
                />
                <StudentsList
                    classId={this.props.classId}
                />
            </>
        )
    }
}