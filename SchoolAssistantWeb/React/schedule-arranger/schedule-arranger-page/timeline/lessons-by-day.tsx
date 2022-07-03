import React from "react";
import DayOfWeek from "../../../schedule-shared/enums/day-of-week";
import LessonTimelineEntry from "../../../schedule-shared/interfaces/lesson-timeline-entry";
import { modalController } from "../../../shared/modals";
import LessonEditModel from "./interfaces/lesson-edit-model";
import LessonModComp from "./lesson-mod-comp/lesson-mod-comp";
import PlacedLesson from "./lesson-tiles/placed-lesson";

type LessonsByDayProps = {
    day: DayOfWeek;
    lessons: LessonTimelineEntry[];
    editStoredLesson: (model: LessonEditModel) => void;

}
type LessonsByDayState = {

}
export default class LessonsByDay extends React.Component<LessonsByDayProps, LessonsByDayState> {

    render() {
        return (
            <>
                {this.props.lessons.map(lesson =>
                    <PlacedLesson
                        key={`${lesson.time.hour}${lesson.time.minutes}`}
                        lesson={lesson}
                        openModificationComponent={this.openModificationComponent}
                    />
                )}
            </>
        )
    }


    private openModificationComponent = (lesson: LessonTimelineEntry) => {
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
}