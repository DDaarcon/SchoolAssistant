import React from "react"
import { displayTime } from "../../../help-functions";
import { Lesson } from "../../../interfaces/lesson";

type OverlappingLessonPadProps = {
    lesson: Lesson;
}
type OverlappingLessonPadState = {}
export default class OverlappingLessonPad extends React.Component<OverlappingLessonPadProps, OverlappingLessonPadState> {

    render() {
        return (
            <div className="lmc-overlap-pad">
                <div className="lmc-op-class">
                    {this.props.lesson.orgClass.name}
                </div>
                <div className="lmc-op-subject">
                    {this.props.lesson.subject.name}
                </div>
                <div className="lmc-op-time">
                    {displayTime(this.props.lesson.time)}
                </div>
                <div className="lmc-op-lecturer">
                    {this.props.lesson.lecturer.name}
                </div>
                <div className="lmc-op-room">
                    {this.props.lesson.room.name}
                </div>
                <button className="lmc-op-btn">
                    <i className="fa-solid fa-ellipsis-vertical"></i>
                </button>
            </div>
        )
    }
}