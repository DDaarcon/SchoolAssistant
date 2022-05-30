import React from "react";
import DayLessons from '../schedule-shared/interfaces/day-lessons';
import ScheduleDisplayTimeline from './components/timeline';
import ScheduleConfig from "./interfaces/schedule-config";
import './schedule.css';

type ScheduleProps = {
    config: ScheduleConfig;
    lessons: DayLessons[];
}
type ScheduleState = { }
export default class Schedule extends React.Component<ScheduleProps, ScheduleState> {
    constructor(props) {
        super(props);

    }

    render() {
        return (
            <ScheduleDisplayTimeline
                config={this.props.config}
                data={this.props.lessons}
            />
        )
    }
}