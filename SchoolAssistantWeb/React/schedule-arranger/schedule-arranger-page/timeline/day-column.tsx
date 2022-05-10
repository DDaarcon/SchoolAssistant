import React from "react";
import { DayOfWeek } from "../../enums/day-of-week";
import { LessonTimelineEntry } from "../../interfaces/lesson-timeline-entry";
import { Time } from "../../interfaces/shared";
import { scheduleArrangerConfig } from "../../main";
import DayLabel from "./day-label";
import LessonPlacingShadow from "./lesson-placing-shadow";
import LessonsByDay from "./lessons-by-day";
import RoomBusyLessons from "./room-busy-lessons";
import TeacherBusyLessons from "./teacher-busy-lessons";
import TimelineCell from "./timeline-cell";

type DayColumnProps = {
    dayIndicator: DayOfWeek;
    lessons: LessonTimelineEntry[];

    teacherBusyLessons?: LessonTimelineEntry[];
    roomBusyLessons?: LessonTimelineEntry[];

    dropped: (dayIndicator: DayOfWeek, cellIndex: number, time: Time, data: DataTransfer) => void;
}
type DayColumnState = {
    shadowFor?: Time;
}
export default class DayColumn extends React.Component<DayColumnProps, DayColumnState> {
    private _cells: JSX.Element[];
    private _iAmCallingHideShadow = false;

    constructor(props) {
        super(props);

        this.state = {};

        addEventListener('hideLessonShadow', () => this.hideLessonShadow());

        this.instantiateCells();
    }


    instantiateCells = () => {
        const cellsPerHour = 60 / scheduleArrangerConfig.cellDuration;
        const count = (scheduleArrangerConfig.endHour - scheduleArrangerConfig.startHour) * cellsPerHour;

        const cellTimes = Array.from({ length: count }, (_, i): Time => {
            const minutesFromMidnight = (scheduleArrangerConfig.startHour * 60) + scheduleArrangerConfig.cellDuration * i;
            return {
                hour: Math.floor(minutesFromMidnight / 60),
                minutes: minutesFromMidnight % 60
            };
        })

        this._cells = cellTimes.map((cellTime, i) =>
            <TimelineCell
                key={i}
                dayIndicator={this.props.dayIndicator}
                cellIndex={i}
                dropped={this.props.dropped}
                entered={this.onEntered}
                time={cellTime}
            />);
    }



    dropped = (dayIndicator: DayOfWeek, cellIndex: number, time: Time, data: DataTransfer) => {
        this.hideLessonShadow();
        this.props.dropped(dayIndicator, cellIndex, time, data);
    }

    onEntered = (dayIndicator: DayOfWeek, cellIndex: number, time: Time) => {
        this._iAmCallingHideShadow = true;
        dispatchEvent(new Event('hideLessonShadow'));
        this._iAmCallingHideShadow = false;

        this.setState({ shadowFor: time });
    }



    hideLessonShadow = () => {
        if (this.state.shadowFor && !this._iAmCallingHideShadow)
            this.setState({ shadowFor: undefined });
    }



    render() {
        return (
            <div className="sa-schedule-day-column"
                onDragEnd={this.hideLessonShadow}
            >
                <DayLabel day={this.props.dayIndicator} />
                <RoomBusyLessons lessons={this.props.roomBusyLessons} />
                <TeacherBusyLessons lessons={this.props.teacherBusyLessons} />
                <LessonPlacingShadow time={this.state.shadowFor} />
                <LessonsByDay lessons={this.props.lessons} />
                {this._cells}
            </div>
        )
    }
}