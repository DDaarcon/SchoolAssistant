import React from "react";
import DayOfWeek from "../../../schedule-shared/enums/day-of-week";
import { displayTime } from "../../../schedule-shared/help/time-functions";
import LessonTimelineEntry from "../../../schedule-shared/interfaces/lesson-timeline-entry";
import { modalController } from "../../../shared/modals";
import GenericLessonTile from "./generic-lesson-tile";
import LessonEditModel from "./interfaces/lesson-edit-model";
import LessonModComp from "./lesson-mod-comp/lesson-mod-comp";

type LessonsByDayProps = {
    day: DayOfWeek;
    lessons: LessonTimelineEntry[];
    editStoredLesson: (model: LessonEditModel) => void;
}
type LessonsByDayState = {

}
export default class LessonsByDay extends React.Component<LessonsByDayProps, LessonsByDayState> {

    openModificationComponent = (lesson: LessonTimelineEntry) => {
        modalController.addCustomComponent({
            modificationComponent: LessonModComp,
            modificationComponentProps: {
                lesson,
                day: this.props.day,
                editStoredLesson: this.props.editStoredLesson
            },
            style: {
                width: '800px'
            }
        })
    }

    render() {
        return (
            <>
                {this.props.lessons.map(lesson =>
                    <GenericLessonTile className="sa-placed-lesson"
                        key={`${lesson.time.hour}${lesson.time.minutes}`}
                        time={lesson.time}
                        customDuration={lesson.customDuration}
                        onPress={() => this.openModificationComponent(lesson)}
                    >
                        <div className="sa-lesson-time">
                            {displayTime(lesson.time)}
                        </div>
                        <div className="sa-lesson-subject">
                            {lesson.subject.name}
                        </div>
                        <div className="sa-lesson-lecturer">
                            {lesson.lecturer.name}
                        </div>
                        <div className="sa-lesson-room">
                            {lesson.room?.name}
                        </div>
                    </GenericLessonTile>
                )}
            </>
        )
    }
}