import React from "react";
import { displayTime } from "../../../../schedule-shared/help/time-functions";
import LessonTimelineEntry from "../../../../schedule-shared/interfaces/lesson-timeline-entry";
import GenericLessonTile from "./generic-lesson-tile";
import './placed-lesson.css';

type PlacedLessonProps = {
    lesson: LessonTimelineEntry;
    openModificationComponent: (lesson: LessonTimelineEntry) => void;
}

export default class PlacedLesson extends React.Component<PlacedLessonProps> {

    render() {
        return (
            <GenericLessonTile
                className="sa-placed-lesson"
                time={this._lesson.time}
                customDuration={this._lesson.customDuration}
                onPress={() => this.props.openModificationComponent(this._lesson)}
                onTouch={() => { }}
            >
                <div className="sa-lesson-time">
                    {displayTime(this._lesson.time)}
                </div>
                <div className="sa-lesson-subject">
                    {this._lesson.subject.name}
                </div>
                <div className="sa-lesson-lecturer">
                    {this._lesson.lecturer.name}
                </div>
                <div className="sa-lesson-room">
                    {this._lesson.room?.name}
                </div>
                <button className="sa-placed-lesson-edit-mobile"
                    onClick={() => this.props.openModificationComponent(this._lesson)}
                >
                    Edytuj
                </button>
            </GenericLessonTile>
        )
    }

    private get _lesson() { return this.props.lesson; }
}