import React from "react";
import { LessonTimelineEntry } from "../../interfaces/lesson-timeline-entry";
import GenericLessonTile from "./generic-lesson-tile";

type RoomBusyLessonsProps = {
    lessons?: LessonTimelineEntry[];
}
const RoomBusyLessons = (props: RoomBusyLessonsProps) => {
    if (!props.lessons) return <></>;
    return (
        <>
            {props.lessons.map(x => (
                <GenericLessonTile className="sa-lessons-room-busy"
                    key={x.id}
                    time={x.time}
                >
                    <div>
                        {x.room.name}
                    </div>
                </GenericLessonTile>
            ))}
        </>
    )
}
export default RoomBusyLessons;