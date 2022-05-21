import React from "react";
import { displayTime } from "../../../schedule-shared/help/time-functions";
import LessonTile from "./lesson-tile";
import './teacher-lesson-tile.css';

export default class TeacherLessonTile extends LessonTile {
    protected getInnerComponents(): JSX.Element {
        return (
            <>
                <div className="sched-teac-lesson-time">
                    {displayTime(this.props.lesson.time)}
                </div>
                <div className="sched-teac-lesson-subject">
                    {this.props.lesson.subject.name}
                </div>
                <div className="sched-teac-lesson-lecturer">
                    {this.props.lesson.lecturer.name}
                </div>
                <div className="sched-teac-lesson-room">
                    {this.props.lesson.room.name}
                </div>
            </>
        )
    }
}