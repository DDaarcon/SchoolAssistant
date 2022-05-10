import React from "react";

type ScheduleAddPrefabTileProps = {
    onClick: () => void;
}
const AddLessonPrefabTile = (props: ScheduleAddPrefabTileProps) => {
    return (
        <div className="sa-add-lesson-prefab"
            onClick={props.onClick}
        >
            <i className="fa-solid fa-plus"></i>
        </div>
    )
}
export default AddLessonPrefabTile;