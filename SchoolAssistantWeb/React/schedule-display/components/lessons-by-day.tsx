import React from "react";
import DayOfWeek from "../../schedule-shared/enums/day-of-week";
import Lesson from "../../schedule-shared/interfaces/lesson";
import ScheduleViewerType from "../enums/schedule-viewer-type";
import ScheduleConfig from "../interfaces/schedule-config";
import LessonTile, { LessonTileProps } from "./lesson-tiles/lesson-tile";
import StudentLessonTile from "./lesson-tiles/student-lesson-tile";
import TeacherLessonTile from "./lesson-tiles/teacher-lesson-tile";

type LessonsByDayProps = {
    day: DayOfWeek;
    lessons: Lesson[];
    config: ScheduleConfig;
}
type LessonsByDayState = {

}
export default class LessonsByDay extends React.Component<LessonsByDayProps, LessonsByDayState> {

    constructor(props) {
        super(props);

        this.setProperTileComponent();
    }

    render() {
        return (
            <>
                {this.props.lessons.map(lesson =>
                    <this._tileComponent
                        key={`${lesson.time.hour}${lesson.time.minutes}`}
                        config={this.props.config}
                        lesson={lesson}
                    />
                )}
            </>
        )
    }

    private _tileComponent: new (props: LessonTileProps) => LessonTile;

    private setProperTileComponent() {
        switch (this.props.config.for) {
            case ScheduleViewerType.Student:
                this._tileComponent = StudentLessonTile;
                break;
            case ScheduleViewerType.Teacher:
                this._tileComponent = TeacherLessonTile;
                break;
        }
    }
}