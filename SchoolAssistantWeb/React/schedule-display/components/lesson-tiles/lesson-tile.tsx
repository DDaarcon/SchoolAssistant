﻿import React from "react";
import Lesson from "../../../schedule-shared/interfaces/lesson";
import ScheduleConfig from "../../interfaces/schedule-config";
import SETTINGS from "../../settings";
import './lesson-tile.css';

export type LessonTileProps = {
    lesson: Lesson;
    config: ScheduleConfig;
    cellHeight: number;
}
export type LessonTileState = {}

export default abstract class LessonTile extends React.Component<LessonTileProps, LessonTileState> {

    private get _cellDuration() { return 60 / SETTINGS.CellsPerHour; }

    private calcTopOffset() {
        const minutes = (this.props.lesson.time.hour - this.props.config.startHour) * 60 + this.props.lesson.time.minutes;
        const cells = minutes / this._cellDuration;
        return cells * this.props.cellHeight;
    }

    private calcHeight() {
        const duration = this.props.lesson.customDuration ?? this.props.config.defaultLessonDuration;
        const cells = duration / this._cellDuration;
        return cells * this.props.cellHeight;
    }

    protected abstract getInnerComponents(): JSX.Element;

    render() {
        let style: React.CSSProperties = {
            top: this.calcTopOffset(),
            height: this.calcHeight()
        }

        return (
            <div className="sched-disp-lesson-tile"
                style={style}
            >
                {this.getInnerComponents()}
            </div>
        )
    }
}