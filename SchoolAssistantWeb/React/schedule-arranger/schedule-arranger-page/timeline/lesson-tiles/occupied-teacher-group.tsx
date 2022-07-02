import React from "react";
import LessonTimelineEntry from "../../../../schedule-shared/interfaces/lesson-timeline-entry";
import GenericLessonTile from "./generic-lesson-tile";

type OccupiedTeacherGroupProps = {
    lessons?: LessonTimelineEntry[];
}
const OccupiedTeacherGroup = (props: OccupiedTeacherGroupProps) => {
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
export default OccupiedTeacherGroup;