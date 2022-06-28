import React from "react";
import { displayTime } from "../../../schedule-shared/help/time-functions";
import { LabelValue } from "../../../shared/form-controls";
import LessonTile from "./lesson-tile";
import './teacher-lesson-tile.css';

export default class TeacherLessonTile extends LessonTile {
    protected getInnerComponents(): JSX.Element {
        const labelValueStyle: React.CSSProperties = {
            width: '100%',
            fontSize: '0.8em',
            marginBottom: 0
        };

        const labelStyle: React.CSSProperties = {
            width: '20%',
        };

        const studentClassName = this.props.lesson.orgClass?.name ?? this.props.lesson.subjClass?.name;

        return (
            <div className="sched-teac-lesson-inner-container">

                <div className={"sched-teac-lesson-main-cnt"}>
                    {`${studentClassName} ${this.props.lesson.subject.name}`}
                </div>

                <div className={"sched-teac-lesson-expandable " + (this.state.hover ? "expanded" : "")}>
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
                </div>
            </div>
        )
    }
}