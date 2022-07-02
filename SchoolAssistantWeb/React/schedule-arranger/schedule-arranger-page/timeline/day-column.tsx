import React from "react";
import DayColumnBase, { DayColumnBaseProps, DayColumnBaseState } from "../../../schedule-shared/components/day-column-base";
import DayOfWeek from "../../../schedule-shared/enums/day-of-week";
import LessonTimelineEntry from "../../../schedule-shared/interfaces/lesson-timeline-entry";
import Time from "../../../schedule-shared/interfaces/shared/time";
import ScheduleArrangerConfig from "../../interfaces/page-model-to-react/schedule-arranger-config";
import LessonEditModel from "./interfaces/lesson-edit-model";
import OccupiedRoomGroup from "./lesson-tiles/occupied-room-group";
import OccupiedTeacherGroup from "./lesson-tiles/occupied-teacher-group";
import PlacingShadow from "./lesson-tiles/placing-shadow";
import TouchPlacingConfirm from "./lesson-tiles/touch-placing-confirm";
import LessonsByDay from "./lessons-by-day";
import TimelineCell from "./timeline-cell";

type DayColumnProps = DayColumnBaseProps<ScheduleArrangerConfig, LessonTimelineEntry> & {
    teacherBusyLessons?: LessonTimelineEntry[];
    roomBusyLessons?: LessonTimelineEntry[];

    addLesson: (dayIndicator: DayOfWeek, time: Time) => void;
    editStoredLesson: (model: LessonEditModel) => void;
}
type DayColumnState = DayColumnBaseState & {
    shadowFor?: Time;
    confirmationFor?: Time;
}

export default class DayColumn extends DayColumnBase<DayColumnProps, DayColumnState, ScheduleArrangerConfig, LessonTimelineEntry> {

    private _iAmCallingHidePlacingHelpers = false;

    constructor(props) {
        super(props);

        addEventListener('hidePlacingHelpers', () => this.hidePlacingHelpers());
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
            onDragEnd: this.hidePlacingHelpers
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
                time={time}
                cellIndex={index}

                dropped={this.props.addLesson}
                mouseEntered={this.mouseEntered}
                touched={this.touched}
            />
        )
    }
    protected override getAdditionalComponents(): JSX.Element | JSX.Element[] {
        return [
            <OccupiedRoomGroup
                lessons={this.props.roomBusyLessons}
                key="rooms" />,
            <OccupiedTeacherGroup
                lessons={this.props.teacherBusyLessons}
                key="teachers" />,
            <PlacingShadow
                time={this.state.shadowFor}
                key="placingShadow" />,
            <TouchPlacingConfirm
                time={this.state.confirmationFor}
                key="touchPlacingConfirm"
                onConfirm={this.confirmedByTouch} />
        ]
    }


    private mouseEntered = (dayIndicator: DayOfWeek, time: Time) => {
        if (this.state.confirmationFor)
            return;

        this.callHidePlacingHelpersForOtherColumns();

        this.setState({ shadowFor: time });
    }

    private touched = (dayIndicator: DayOfWeek, time: Time) => {
        this.callHidePlacingHelpersForOtherColumns();

        this.setState({ confirmationFor: time });
    }

    private confirmedByTouch = () => {
        this.props.addLesson(this.props.dayIndicator, this.state.confirmationFor);
    }




    private callHidePlacingHelpersForOtherColumns() {
        this._iAmCallingHidePlacingHelpers = true;
        dispatchEvent(new Event('hidePlacingHelpers'));
        this._iAmCallingHidePlacingHelpers = false;
    }
    private hidePlacingHelpers = () => {
        if ((this.state.shadowFor || this.state.confirmationFor)
            && !this._iAmCallingHidePlacingHelpers) {

            this.setState({ shadowFor: undefined, confirmationFor: undefined });
        }
    }
}