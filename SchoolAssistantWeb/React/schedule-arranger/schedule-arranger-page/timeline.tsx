import React from 'react';
import DayOfWeek from '../../schedule-shared/enums/day-of-week';
import DayLessons from '../../schedule-shared/interfaces/day-lessons';
import Time from '../../schedule-shared/interfaces/shared/time';
import ScheduleTimelineBase, { ScheduleTimelineBaseProps, ScheduleTimelineBaseState } from '../../schedule-shared/timeline-base';
import { AddLessonResponse } from '../interfaces/add-lesson-response';
import LessonPrefab from '../interfaces/lesson-prefab';
import { scheduleArrangerConfig, server } from '../main';
import dataService from '../schedule-data-service';
import DayColumn from './timeline/day-column';
import LessonEditModel from './timeline/interfaces/lesson-edit-model';

type ScheduleArrangerTimelineProps = ScheduleTimelineBaseProps & {

}
type ScheduleArrangerTimelineState = ScheduleTimelineBaseState & {
    teacherBusyLessons?: DayLessons[];
    roomBusyLessons?: DayLessons[];
}
export default class ScheduleArrangerTimeline extends ScheduleTimelineBase<ScheduleArrangerTimelineProps, ScheduleArrangerTimelineState> {

    constructor(props) {
        super(props);

        dataService.assignDaysFromProps(this.props.data);

        addEventListener('dragBegan', (event: CustomEvent) => this.initiateShowingOtherLessonsShadows(event));
        addEventListener('clearOtherLessons', this.hideOtherLessonsShadows);
        addEventListener('timeline-lessons-rerender', this.rerender);

        this.className = "schedule-arranger-timeline";
    }

    protected getDayColumnComponent(day: DayOfWeek): JSX.Element {
        return (
            <DayColumn
                key={day}
                dayIndicator={day}
                config={this.props.config}
                lessons={dataService.lessons.find(x => x.dayIndicator == day)?.lessons ?? []}
                teacherBusyLessons={this.state.teacherBusyLessons?.find(x => x.dayIndicator == day)?.lessons}
                roomBusyLessons={this.state.roomBusyLessons?.find(x => x.dayIndicator == day)?.lessons}
                addLesson={this.addLesson}
                editStoredLesson={this.editLesson}
            />
        )
    }


    addLesson = async (dayIndicator: DayOfWeek, cellIndex: number, time: Time, data: DataTransfer) => {
        this.hideOtherLessonsShadows();

        const prefab: LessonPrefab | undefined = JSON.parse(data.getData("prefab"));

        const lessons = await dataService.getOverlappingLessonsAsync({
            day: dayIndicator,
            time,
            teacherId: prefab?.lecturer.id,
            roomId: prefab?.room.id
        });
        if (lessons.length) return;

        server.postAsync<AddLessonResponse>("Lesson", {}, {
            classId: scheduleArrangerConfig.classId,
            day: dayIndicator,
            time: time,
            customDuration: undefined,
            subjectId: prefab.subject.id,
            lecturerId: prefab.lecturer.id,
            roomId: prefab.room.id
        }).then(result => {

            if (result.success) {
                dataService.lessons[dayIndicator].lessons.push(result.lesson);

                this.rerender();
            }
        })
    }


    editLesson = (model: LessonEditModel) => {
        const dayAndLesson = dataService.getLessonById(model.id);
        if (!dayAndLesson)
            return;

        if (dayAndLesson.day != model.day) {
            const oldDayLessons = dataService.lessons.find(x => x.dayIndicator == dayAndLesson.day);
            oldDayLessons.lessons.splice(oldDayLessons.lessons.indexOf(dayAndLesson.lesson), 1);

            dataService.lessons.find(x => x.dayIndicator == model.day).lessons.push(dayAndLesson.lesson);
        }

        dayAndLesson.lesson.time = model.time;
        dayAndLesson.lesson.customDuration = model.customDuration;

        if (dayAndLesson.lesson.lecturer.id != model.lecturerId)
            dayAndLesson.lesson.lecturer = {
                id: model.lecturerId,
                name: dataService.teachers.find(x => x.id == model.lecturerId).shortName
            };
        if (dayAndLesson.lesson.room.id != model.roomId)
            dayAndLesson.lesson.room = {
                id: model.roomId,
                name: dataService.rooms.find(x => x.id == model.roomId).name
            };
        if (dayAndLesson.lesson.subject.id != model.subjectId)
            dayAndLesson.lesson.subject = {
                id: model.subjectId,
                name: dataService.subjects.find(x => x.id == model.subjectId).name
            };

        this.rerender();
    }




    initiateShowingOtherLessonsShadows = async (event: CustomEvent) => {
        const data: LessonPrefab = event.detail;

        await dataService.getTeacherAndRoomLessonsAsync(data.lecturer.id, data.room.id, this.displayOtherLessonsShadows);
    }


    displayOtherLessonsShadows = (teacher?: DayLessons[], room?: DayLessons[]) => {
        if (!teacher && !room) return;

        this.setState(prevState => {
            let { teacherBusyLessons, roomBusyLessons } = prevState;

            teacherBusyLessons ?? (teacherBusyLessons = teacher);
            roomBusyLessons ?? (roomBusyLessons = room);

            return { teacherBusyLessons, roomBusyLessons };
        });
    }


    hideOtherLessonsShadows = () => {
        this.setState({
            teacherBusyLessons: undefined,
            roomBusyLessons: undefined
        })
    }

    private rerender = () => this.forceUpdate();
}