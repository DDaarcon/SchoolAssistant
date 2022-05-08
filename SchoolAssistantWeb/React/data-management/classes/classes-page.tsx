import React from "react";
import Category from "../enums/category";
import { RedirectMethod } from "../main";
import StudentsPage, { StudentsPageProps } from "../students/students-page";
import ClassesList from "./class-list";

type ClassesPageProps = {
    onRedirect: RedirectMethod;
};
type ClassesPageState = {

};

export default class ClassesPage extends React.Component<ClassesPageProps, ClassesPageState> {

    moveToStudents = (studentsPageProps: StudentsPageProps) => {
        this.props.onRedirect(Category.Students, StudentsPage, studentsPageProps);
    }

    render() {
        return (
            <ClassesList
                onMoveToStudents={this.moveToStudents}
            />
        )
    }
}