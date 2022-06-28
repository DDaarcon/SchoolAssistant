import React from "react";
import { displayTime } from "../../../schedule-shared/help/time-functions";
import { LabelValue } from "../../../shared/form-controls";
import LessonTile from "./lesson-tile";
import './student-lesson-tile.css';

export default class StudentLessonTile extends LessonTile {
    protected getInnerComponents(): JSX.Element {

        return (
            <div className="sched-stud-lesson-inner-container">

                <div className={"sched-stud-lesson-subject"}>
                    {this.props.lesson.subject.name}
                </div>

                <div className={"sched-stud-lesson-expandable " + (this.state.hover ? "expanded" : "")}>
                    <LabelValue
                        label="czas"
                        value={displayTime(this.props.lesson.time)}
                        containerClassName="label-value-lesson-details-stud"
                        labelContainerClassName="lab-val-lab-lesson-details-stud"
                    />
                    <LabelValue
                        label="miejsce"
                        value={this.props.lesson.room.name}
                        containerClassName="label-value-lesson-details-stud"
                        labelContainerClassName="lab-val-lab-lesson-details-stud"
                    />
                    <LabelValue
                        label="wykł."
                        value={this.props.lesson.lecturer.name}
                        containerClassName="label-value-lesson-details-stud"
                        labelContainerClassName="lab-val-lab-lesson-details-stud"
                    />
                </div>
            </div>
        )
    }
}