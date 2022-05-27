import React from "react";
import { displayTime } from "../../../schedule-shared/help/time-functions";
import LessonTile from "./lesson-tile";
import './student-lesson-tile.css';

export default class StudentLessonTile extends LessonTile {
    protected getInnerComponents(): JSX.Element {
        return (
            <div className="sched-stud-lesson-inner-container">
                <div className={"sched-stud-lesson-time " + (this.state.hover ? "expanded" : "")}>
                    {displayTime(this.props.lesson.time)}
                </div>
                <div className={"sched-stud-lesson-subject"}>
                    {this.props.lesson.subject.name}
                </div>
                <div className={"sched-stud-lesson-lecturer " + (this.state.hover ? "expanded" : "")}>
                    {this.props.lesson.lecturer.name}
                </div>
                <div className={"sched-stud-lesson-room " + (this.state.hover ? "expanded" : "")}>
                    {this.props.lesson.room.name}
                </div>
            </div>
        )
    }
}