import React from "react";
import Time from "../../../../schedule-shared/interfaces/shared/time";
import { scheduleArrangerConfig } from "../../../main";
import PlacingAssistantService from "../../services/placing-assistant-service";
import './lesson-tiles.css';
import './other-lesson-tiles.css';

type GenericLessonTileProps = {
    time: Time;
    customDuration?: number;
    className?: string;
    children: React.ReactNode;
    forceAbove?: boolean;
    onPress?: () => void;
    onTouch?: () => void;
}
type GenericLessonTileState = {

}
export default class GenericLessonTile extends React.Component<GenericLessonTileProps, GenericLessonTileState> {

    render() {
        let style: React.CSSProperties = {
            top: this.calcTopOffset(),
            height: this.calcHeight()
        }

        return (
            <div className={`sa-lesson-tile ${this._showBehind ? 'sa-lesson-tile-behind' : ''} ${this.props.className}`}
                style={style}
                onClick={this.clicked}
                onTouchStart={this.touched}
            >
                {this.props.children}
            </div>
        )
    }

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

    private get _showBehind() {
        return !this.props.forceAbove && PlacingAssistantService.isPlacing;
    }


    private _preventClick = false;
    private clicked = () => {
        if (this._preventClick) {
            this._preventClick = false;
            return;
        }
        this.props.onPress?.();
    }
    private touched = () => {
        if (this.props.onTouch) {
            this._preventClick = true;
            this.props.onTouch();
        }
        else {
            this.props.onPress?.();
        }
    }
}