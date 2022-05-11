import React from "react";
import { LessonPrefab } from "../../interfaces/lesson-prefab";
import './lesson-prefab.css';

type ScheduleLessonPrefabTileProps = {
    data: LessonPrefab;
}
type ScheduleLessonPrefabTileState = {

}
export default class LessonPrefabTile extends React.Component<ScheduleLessonPrefabTileProps, ScheduleLessonPrefabTileState> {

    onStart: React.DragEventHandler<HTMLDivElement> = (event) => {
        event.dataTransfer.setData("prefab", JSON.stringify(this.props.data));
        dispatchEvent(new CustomEvent('dragBegan', {
            detail: this.props.data
        }));
    }

    onEnd: React.DragEventHandler<HTMLDivElement> = (event) => {
        dispatchEvent(new Event("hideLessonShadow"));
        dispatchEvent(new Event("clearOtherLessons"));
    }

    render() {
        return (
            <div className="sa-lesson-prefab"
                draggable
                onDragStart={this.onStart}
                onDragEnd={this.onEnd}
            >
                <span className="sa-lesson-prefab-subject">
                    {this.props.data.subject.name}
                </span>
                <div className="sa-lesson-prefab-bottom">
                    <div className="sa-lesson-prefab-lecturer">
                        {this.props.data.lecturer.name}
                    </div>
                    <div className="sa-lesson-prefab-room">
                        {this.props.data.room?.name}
                    </div>
                </div>
            </div>
        )
    }
}