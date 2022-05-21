import React from "react";
import Lesson from "../../../schedule-shared/interfaces/lesson";
import ScheduleTimelineConfig from "../../../schedule-shared/interfaces/props-models/schedule-timeline-config";
import './lesson-tile.css';

export type LessonTileProps = {
    lesson: Lesson;
    config: ScheduleTimelineConfig;
}
export type LessonTileState = {}

export default abstract class LessonTile extends React.Component<LessonTileProps, LessonTileState> {

    private calcTopOffset() {
        const minutes = (this.props.lesson.time.hour - this.props.config.startHour) * 60 + this.props.lesson.time.minutes;
        const cells = minutes / this.props.config.cellDuration;
        return cells * this.props.config.cellHeight;
    }

    private calcHeight() {
        const duration = this.props.lesson.customDuration ?? this.props.config.defaultLessonDuration;
        const cells = duration / this.props.config.cellDuration;
        return cells * this.props.config.cellHeight;
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