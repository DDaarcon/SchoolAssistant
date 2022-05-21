import React from "react";
import DayColumnBase, { DayColumnBaseProps, DayColumnBaseState } from "../../../schedule-shared/components/day-column-base";
import DayOfWeek from "../../../schedule-shared/enums/day-of-week";
import LessonTimelineEntry from "../../../schedule-shared/interfaces/lesson-timeline-entry";
import Time from "../../../schedule-shared/interfaces/shared/time";
import ScheduleArrangerConfig from "../../interfaces/page-model-to-react/schedule-arranger-config";
import LessonEditModel from "./interfaces/lesson-edit-model";
import LessonPlacingShadow from "./lesson-placing-shadow";
import LessonsByDay from "./lessons-by-day";
import RoomBusyLessons from "./room-busy-lessons";
import TeacherBusyLessons from "./teacher-busy-lessons";
import TimelineCell from "./timeline-cell";

type DayColumnProps = DayColumnBaseProps<ScheduleArrangerConfig, LessonTimelineEntry> & {
    teacherBusyLessons?: LessonTimelineEntry[];
    roomBusyLessons?: LessonTimelineEntry[];

    addLesson: (dayIndicator: DayOfWeek, cellIndex: number, time: Time, data: DataTransfer) => void;
    editStoredLesson: (model: LessonEditModel) => void;
}
type DayColumnState = DayColumnBaseState & {
    shadowFor?: Time;
}

export default class DayColumn extends DayColumnBase<DayColumnProps, DayColumnState, ScheduleArrangerConfig, LessonTimelineEntry> {

    private _iAmCallingHideShadow = false;

    constructor(props) {
        super(props);

        addEventListener('hideLessonShadow', () => this.hideLessonShadow());
        this.instantiateCells();
    }

    private instantiateCells() {
        if (!this.getTimelineCellComponent) throw new Error("Overriding method `getTimelineCellComponent` is required for calling `instantiateCells`");

        const cellsPerHour = 60 / this.props.config.cellDuration;
        const count = (this.props.config.endHour - this.props.config.startHour) * cellsPerHour;

        const cellTimes = Array.from({ length: count }, (_, i): Time => {
            const minutesFromMidnight = (this.props.config.startHour * 60) + this.props.config.cellDuration * i;
            return {
                hour: Math.floor(minutesFromMidnight / 60),
                minutes: minutesFromMidnight % 60
            };
        })

        this._cells = cellTimes.map((cellTime, i) => this.getTimelineCellComponent(cellTime, i));
    }

    protected override getContainerProps(): React.HTMLAttributes<HTMLDivElement> {
        return {
            onDragEnd: this.hideLessonShadow
        }
    }
    protected override getLessonsDisplayComponent(): JSX.Element {
        return (
            <LessonsByDay
                lessons={this.props.lessons}
                day={this.props.dayIndicator}
                editStoredLesson={this.props.editStoredLesson}
            />
        )
    }
    protected getTimelineCellComponent(time: Time, index: number): JSX.Element {
        return (
            <TimelineCell
                key={index}
                height={this.props.config.cellHeight}
                dayIndicator={this.props.dayIndicator}
                cellIndex={index}
                dropped={this.props.addLesson}
                entered={this.onEntered}
                time={time}
            />
        )
    }
    protected override getAdditionalComponents(): JSX.Element | JSX.Element[] {
        return [
            <RoomBusyLessons lessons={this.props.roomBusyLessons} key="rooms" />,
            <TeacherBusyLessons lessons={this.props.teacherBusyLessons} key="teachers" />,
            <LessonPlacingShadow time={this.state.shadowFor} key="thisClass" />
        ]
    }


    addLesson = (dayIndicator: DayOfWeek, cellIndex: number, time: Time, data: DataTransfer) => {
        this.hideLessonShadow();
        this.props.addLesson(dayIndicator, cellIndex, time, data);
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
}