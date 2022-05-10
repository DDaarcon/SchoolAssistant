﻿import React from "react";
import { displayTime } from "../../help-functions";
import { LessonTimelineEntry } from "../../interfaces/lesson-timeline-entry";
import GenericLessonTile from "./generic-lesson-tile";

type LessonsByDayProps = {
    lessons: LessonTimelineEntry[];

}
type LessonsByDayState = {

}
export default class LessonsByDay extends React.Component<LessonsByDayProps, LessonsByDayState> {

    render() {
        return (
            <>
                {this.props.lessons.map(lesson =>
                    <GenericLessonTile className="sa-placed-lesson"
                        key={`${lesson.time.hour}${lesson.time.minutes}`}
                        time={lesson.time}
                        customDuration={lesson.customDuration}
                    >
                        <div className="sa-lesson-time">
                            {displayTime(lesson.time)}
                        </div>
                        <div className="sa-lesson-subject">
                            {lesson.subject.name}
                        </div>
                        <div className="sa-lesson-lecturer">
                            {lesson.lecturer.name}
                        </div>
                        <div className="sa-lesson-room">
                            {lesson.room?.name}
                        </div>
                    </GenericLessonTile>
                )}
            </>
        )
    }
}