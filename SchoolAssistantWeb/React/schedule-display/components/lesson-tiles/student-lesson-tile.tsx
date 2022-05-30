import React from "react";
import { displayTime } from "../../../schedule-shared/help/time-functions";
import { LabelValue } from "../../../shared/form-controls";
import LessonTile from "./lesson-tile";
import './student-lesson-tile.css';

export default class StudentLessonTile extends LessonTile {
    protected getInnerComponents(): JSX.Element {
        const labelValueStyle: React.CSSProperties = {
            width: '100%',
            fontSize: '0.8em',
            marginBottom: 0
        };

        const labelStyle: React.CSSProperties = {
            width: '20%',
        };


        return (
            <div className="sched-stud-lesson-inner-container">

                <div className={"sched-stud-lesson-subject"}>
                    {this.props.lesson.subject.name}
                </div>

                <div className={"sched-stud-lesson-expandable " + (this.state.hover ? "expanded" : "")}>
                    <LabelValue
                        label="czas"
                        valueComp={displayTime(this.props.lesson.time)}
                        outerStyle={labelValueStyle}
                        labelOuterStyle={labelStyle}
                    />
                    <LabelValue
                        label="miejsce"
                        valueComp={this.props.lesson.room.name}
                        outerStyle={labelValueStyle}
                        labelOuterStyle={labelStyle}
                    />
                    <LabelValue
                        label="wykł."
                        valueComp={this.props.lesson.lecturer.name}
                        outerStyle={labelValueStyle}
                        labelOuterStyle={labelStyle}
                    />
                </div>
            </div>
        )
    }
}