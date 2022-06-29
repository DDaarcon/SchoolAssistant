import React from "react";
import Time from "../../../schedule-shared/interfaces/shared/time";
import { scheduleArrangerConfig } from "../../main";
import PlacingAssistantService from "../services/placing-assistant-service";
import './lesson-tiles.css';
import './other-lesson-tiles.css';

type GenericLessonTileProps = {
    time: Time;
    customDuration?: number;
    className?: string;
    children: React.ReactNode;
    onPress?: () => void;
}
type GenericLessonTileState = {

}
export default class GenericLessonTile extends React.Component<GenericLessonTileProps, GenericLessonTileState> {

    private calcTopOffset() {
        const minutes = (this.props.time.hour - scheduleArrangerConfig.startHour) * 60 + this.props.time.minutes;
        const cells = minutes / scheduleArrangerConfig.cellDuration;
        return cells * scheduleArrangerConfig.cellHeight;
    }

    private calcHeight() {
        const duration = this.props.customDuration ?? scheduleArrangerConfig.defaultLessonDuration;
        const cells = duration / scheduleArrangerConfig.cellDuration;
        return cells * scheduleArrangerConfig.cellHeight;
    }

    render() {
        let style: React.CSSProperties = {
            top: this.calcTopOffset(),
            height: this.calcHeight()
        }

        return (
            <button className={`sa-lesson-tile ${PlacingAssistantService.isPlacing ? 'sa-lesson-tile-behind' : ''} ${this.props.className}`}
                style={style}
                onClick={this.props.onPress}
            >
                {this.props.children}
            </button>
        )
    }
}