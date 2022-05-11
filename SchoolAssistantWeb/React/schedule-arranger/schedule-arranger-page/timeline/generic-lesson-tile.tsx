import React from "react";
import { Time } from "../../interfaces/shared";
import { scheduleArrangerConfig } from "../../main";
import './lesson-tiles.css';
import './other-lesson-tiles.css';

type GenericLessonTileProps = {
    time: Time;
    customDuration?: number;
    className?: string;
    children: React.ReactNode;
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
            <div className={`sa-lesson-tile ${this.props.className}`}
                style={style}
            >
                {this.props.children}
            </div>
        )
    }
}