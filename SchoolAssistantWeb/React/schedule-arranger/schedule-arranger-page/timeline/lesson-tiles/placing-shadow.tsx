import React from "react";
import { displayTime, sumTimeAndMinutes } from "../../../../schedule-shared/help/time-functions";
import Time from "../../../../schedule-shared/interfaces/shared/time";
import { scheduleArrangerConfig } from "../../../main";
import GenericLessonTile from "./generic-lesson-tile";

type PlacingShadowProps = {
    time?: Time;
}
type PlacingShadowState = {}
export default class PlacingShadow extends React.Component<PlacingShadowProps, PlacingShadowState> {

    render() {
        if (!this.props.time) return <></>

        return (
            <GenericLessonTile className="sa-lesson-placing-shadow"
                time={this.props.time}
            >
                <h4>{displayTime(this.props.time)} - {displayTime(sumTimeAndMinutes(this.props.time, scheduleArrangerConfig.defaultLessonDuration))}</h4>
            </GenericLessonTile>
        )
    }
}