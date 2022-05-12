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

    onDropped = (dayIndicator: DayOfWeek, cellIndex: number, time: Time, data: DataTransfer) => {
        this.hideOtherLessonsShadows();

        const prefab: LessonPrefab | undefined = JSON.parse(data.getData("prefab"));

        const lessons = dataService.lessons[dayIndicator];
        if (!lessons) return;

        for (const lesson of lessons.lessons ?? []) {
            const overlaps = areTimesOverlappingByDuration(
                lesson.time,
                lesson.customDuration ?? scheduleArrangerConfig.defaultLessonDuration,
                time,
                scheduleArrangerConfig.defaultLessonDuration
            );

            if (overlaps) return;
        }

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
                        dropped={this.onDropped}
                    />
                ))}

            </div>
        )
    }
}