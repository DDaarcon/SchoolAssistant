import React from 'react';
import TimeColumn from '../../schedule-shared/components/time-column';
import DayOfWeek from '../../schedule-shared/enums/day-of-week';
import TimeColumnVariant from '../../schedule-shared/enums/time-column-variant';
import DayLessons from '../../schedule-shared/interfaces/day-lessons';
import LessonTimelineEntry from '../../schedule-shared/interfaces/lesson-timeline-entry';
import Time from '../../schedule-shared/interfaces/shared/time';
import ScheduleTimeline from '../../schedule-shared/schedule-timeline';
import { AddLessonResponse } from '../interfaces/add-lesson-response';
import ScheduleArrangerConfig from '../interfaces/page-model-to-react/schedule-arranger-config';
import { scheduleArrangerConfig, server } from '../main';
import dataService from '../schedule-data-service';
import PlacingAssistantService from './services/placing-assistant-service';
import DayColumn from './timeline/day-column';
import LessonEditModel from './timeline/interfaces/lesson-edit-model';

type ScheduleArrangerTimelineProps = {
    config: ScheduleArrangerConfig;
    data: DayLessons<LessonTimelineEntry>[];
}
type ScheduleArrangerTimelineState = {
    teacherBusyLessons?: DayLessons[];
    roomBusyLessons?: DayLessons[];
}
export default class ScheduleArrangerTimeline extends React.Component<ScheduleArrangerTimelineProps, ScheduleArrangerTimelineState> {

    constructor(props) {
        super(props);

        dataService.assignDaysFromProps(this.props.data);

        PlacingAssistantService.handlers.hideOtherLessons = this.hideOtherLessonsShadows;
        PlacingAssistantService.handlers.showOtherLessons = this.initiateShowingOtherLessonsShadowsAsync;

        addEventListener('timeline-lessons-rerender', this.rerender);

        this.state = {}
    }

    render() {
        return (
            <ScheduleTimeline
                config={this.props.config}
                className="schedule-arranger-timeline"
                dayColumnFactory={this.dayColumnFactory}
                timeColumn={
                    <TimeColumn
                        {...this.props.config}
                        variant={TimeColumnVariant.WholeHoursByCellSpec}
                    />
                }
            />
        )
    }

    private dayColumnFactory = (day: DayOfWeek): JSX.Element => {
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





    private addLesson = async (dayIndicator: DayOfWeek, time: Time) => {

        const prefab = PlacingAssistantService.getPrefabAndDismiss();

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


    private editLesson = (model: LessonEditModel) => {
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




    private initiateShowingOtherLessonsShadowsAsync = async () => {
        const data = PlacingAssistantService.prefab;

        await dataService.getTeacherAndRoomLessonsAsync(data.lecturer.id, data.room.id, this.displayOtherLessonsShadows);
    }


    private displayOtherLessonsShadows = (teacher?: DayLessons[], room?: DayLessons[]) => {
        if (!teacher && !room) return;

        this.setState(prevState => {
            let { teacherBusyLessons, roomBusyLessons } = prevState;

            if (teacher)
                teacherBusyLessons = teacher;
            if (room)
                roomBusyLessons = room;

            return { teacherBusyLessons, roomBusyLessons };
        });
    }


    private hideOtherLessonsShadows = () => {
        this.setState({
            teacherBusyLessons: undefined,
            roomBusyLessons: undefined
        })
    }

    private rerender = () => this.forceUpdate();
}