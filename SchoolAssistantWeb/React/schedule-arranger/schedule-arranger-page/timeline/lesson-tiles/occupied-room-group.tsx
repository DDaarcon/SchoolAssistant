import React from "react";
import LessonTimelineEntry from "../../../../schedule-shared/interfaces/lesson-timeline-entry";
import GenericLessonTile from "./generic-lesson-tile";

type OccupiedRoomGroupProps = {
    lessons?: LessonTimelineEntry[];
}
const OccupiedRoomGroup = (props: OccupiedRoomGroupProps) => {
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
export default OccupiedRoomGroup;