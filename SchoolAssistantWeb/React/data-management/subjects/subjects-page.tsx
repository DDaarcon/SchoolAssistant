import React from "react";
import SubjectList from "./subject-list";

type SubjectsPageProps = {

};
type SubjectsPageState = {

}

export default class SubjectsPage extends React.Component<SubjectsPageProps, SubjectsPageState> {

    render() {
        return (
            <div className="dm-subjects-page">
                <SubjectList />
            </div>
        )
    }
}