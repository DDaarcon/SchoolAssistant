import React from "react";
import { LessonTimelineEntry } from "../../interfaces/lesson-timeline-entry";
import GenericLessonTile from "./generic-lesson-tile";

type TeacherBusyLessonsProps = {
    lessons?: LessonTimelineEntry[];
}
const TeacherBusyLessons = (props: TeacherBusyLessonsProps) => {
    if (!props.lessons) return <></>;
    return (
        <>
            {props.lessons.map(x => (
                <GenericLessonTile className="sa-lessons-teacher-busy"
                    key={x.id}
                    time={x.time}
                >
                    <div>
                        {x.lecturer.name}
                    </div>
                </GenericLessonTile>
            ))}
        </>
    )
}
export default TeacherBusyLessons;