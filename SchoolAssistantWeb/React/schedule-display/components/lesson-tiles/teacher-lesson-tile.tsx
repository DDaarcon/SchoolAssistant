import React from "react";
import { displayTime } from "../../../schedule-shared/help/time-functions";
import { LabelValue } from "../../../shared/form-controls";
import LessonTile from "./lesson-tile";
import './teacher-lesson-tile.css';

export default class TeacherLessonTile extends LessonTile {
    protected getInnerComponents(): JSX.Element {

        const studentClassName = this.props.lesson.orgClass?.name ?? this.props.lesson.subjClass?.name;

        return (
            <div className="sched-teac-lesson-inner-container">

                <div className={"sched-teac-lesson-main-cnt"}>
                    {`${studentClassName} ${this.props.lesson.subject.name}`}
                </div>

                <div className={"sched-teac-lesson-expandable " + (this.state.hover ? "expanded" : "")}>
                    <LabelValue
                        label="czas"
                        value={displayTime(this.props.lesson.time)}
                        containerClassName="label-value-lessson-details-teac"
                        labelContainerClassName="lab-val-lab-lesson-details-teac"
                    />
                    <LabelValue
                        label="miejsce"
                        value={this.props.lesson.room.name}
                        containerClassName="label-value-lessson-details-teac"
                        labelContainerClassName="lab-val-lab-lesson-details-teac"
                    />
                </div>
            </div>
        )
    }
}