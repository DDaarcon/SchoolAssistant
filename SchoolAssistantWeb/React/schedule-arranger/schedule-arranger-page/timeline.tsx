import React from 'react';
import { DayOfWeek } from '../enums/day-of-week';
import { areTimesOverlappingByDuration } from '../help-functions';
import { AddLessonResponse } from '../interfaces/add-lesson-response';
import { DayLessons } from '../interfaces/day-lessons';
import { LessonPrefab } from '../interfaces/lesson-prefab';
import { Time } from '../interfaces/shared';
import { scheduleArrangerConfig, server } from '../main';
import dataService from '../schedule-data-service';
import DayColumn from './timeline/day-column';
import LessonEditModel from './timeline/interfaces/lesson-edit-model';
import TimeColumn from './timeline/time-column';

type ScheduleArrangerTimelineProps = {
    data: DayLessons[];

}
type ScheduleArrangerTimelineState = {
    teacherBusyLessons?: DayLessons[];
    roomBusyLessons?: DayLessons[];
}
export default class ScheduleArrangerTimeline extends React.Component<ScheduleArrangerTimelineProps, ScheduleArrangerTimelineState> {
    
    constructor(props) {
        super(props);

        this.state = {};

        dataService.assignDaysFromProps(this.props.data);

        addEventListener('dragBegan', (event: CustomEvent) => this.initiateShowingOtherLessonsShadows(event));
        addEventListener('clearOtherLessons', this.hideOtherLessonsShadows);
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
            console.log(result);
            if (result.success) {
                dataService.lessons[dayIndicator].lessons.push(result.lesson);
                this.forceUpdate();
            }
        })
    }


    editLesson = (model: LessonEditModel) => {
        const { day, lesson } = dataService.getLessonById(model.id);
        if (!lesson)
            return;

        if (day != model.day) {
            const oldDayLessons = dataService.lessons.find(x => x.dayIndicator == day);
            oldDayLessons.lessons.splice(oldDayLessons.lessons.indexOf(lesson), 1);

            dataService.lessons.find(x => x.dayIndicator == model.day).lessons.push(lesson);
        }

        lesson.time = model.time;
        lesson.customDuration = model.customDuration;

        if (lesson.lecturer.id != model.lecturerId)
            lesson.lecturer = {
                id: model.lecturerId,
                name: dataService.teachers.find(x => x.id == model.lecturerId).shortName
            };
        if (lesson.room.id != model.roomId)
            lesson.room = {
                id: model.roomId,
                name: dataService.rooms.find(x => x.id == model.roomId).name
            };
        if (lesson.subject.id != model.subjectId)
            lesson.subject = {
                id: model.subjectId,
                name: dataService.subjects.find(x => x.id == model.subjectId).name
            };

        this.forceUpdate();
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



    private getDaysOfWeekIterable = (with6th: boolean = false, with0th: boolean = false) =>
        Object.values(DayOfWeek).map(x => parseInt(x as unknown as string)).filter(x =>
            !isNaN(x) && (with0th || x != 0) && (with6th || x != 6)) as DayOfWeek[];

    render() {
        return (
            <div className="schedule-arranger-timeline">

                <TimeColumn />

                {this.getDaysOfWeekIterable().map(day => (
                    <DayColumn
                        key={day}
                        dayIndicator={day}
                        lessons={dataService.lessons.find(x => x.dayIndicator == day)?.lessons ?? []}
                        teacherBusyLessons={this.state.teacherBusyLessons?.find(x => x.dayIndicator == day)?.lessons}
                        roomBusyLessons={this.state.roomBusyLessons?.find(x => x.dayIndicator == day)?.lessons}
                        addLesson={this.addLesson}
                        editStoredLesson={this.editLesson}
                    />
                ))}

            </div>
        )
    }
}