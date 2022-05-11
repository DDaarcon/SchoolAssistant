import * as React from "react";
import ScheduleArrangerTimeline from './schedule-arranger-page/timeline'
import ScheduleArrangerSelector from "./schedule-arranger-page/selector";
import { ClassLessons } from "./interfaces/class-lessons";
import './schedule-arranger-page/schedule-arranger-page.css';

type ScheduleArrangerPageProps = {
    classData: ClassLessons;
}
type ScheduleArrangerPageState = {

}
export default class ScheduleArrangerPage extends React.Component<ScheduleArrangerPageProps, ScheduleArrangerPageState> {


    render() {
        return (
            <div className="schedule-arranger-page">
                <ScheduleArrangerSelector
                    data={this.props.classData.data}
                />

                <ScheduleArrangerTimeline
                    data={this.props.classData.data}
                />
            </div>
        )
    }
}