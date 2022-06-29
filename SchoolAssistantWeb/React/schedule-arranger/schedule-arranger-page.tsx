import * as React from "react";
import ScheduleArrangerSelector from "./schedule-arranger-page/selector";
import ClassLessons from "./interfaces/class-lessons";
import './schedule-arranger-page/schedule-arranger-page.css';
import { scheduleArrangerConfig } from "./main";
import ScheduleArrangerTimeline from "./schedule-arranger-page/schedule-arranger-timeline";

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
                    config={scheduleArrangerConfig}
                    data={this.props.classData.data}
                />
            </div>
        )
    }
}