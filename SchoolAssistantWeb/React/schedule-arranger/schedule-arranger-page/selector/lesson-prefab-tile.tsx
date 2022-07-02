import React from "react";
import LessonPrefab from "../../interfaces/lesson-prefab";
import PlacingAssistantService from "../services/placing-assistant-service";
import './lesson-prefab.css';

type ScheduleLessonPrefabTileProps = {
    data: LessonPrefab;
}
type ScheduleLessonPrefabTileState = {
    selected: boolean;
}
export default class LessonPrefabTile extends React.Component<ScheduleLessonPrefabTileProps, ScheduleLessonPrefabTileState> {

    constructor(props) {
        super(props);

        this.state = {
            selected: false
        }
    }

    render() {
        return (
            <div className={this._className}
                draggable
                onDragStart={this.dragStart}
                onDragEnd={this.dragEnd}
                onClick={this.clicked}
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

    private get _className() {
        let className = "sa-lesson-prefab";
        if (this.state.selected)
            className += " sa-lesson-prefab-selected";

        return className;
    }


    private dragStart: React.DragEventHandler<HTMLDivElement> = event => {
        PlacingAssistantService.startWithDrag(this.props.data);
    }

    private dragEnd: React.DragEventHandler<HTMLDivElement> = event => {
        PlacingAssistantService.dismiss();
    }

    private clicked: React.MouseEventHandler<HTMLDivElement> = event => {
        this.setState({ selected: true });

        PlacingAssistantService.startWithSelect(this.props.data, this.deselect);
    }


    private deselect = () => {
        this.setState({ selected: false });
    }
}