import React from "react";
import { displayTime, sumTimes } from "../../help-functions";
import { Time } from "../../interfaces/shared";
import { scheduleArrangerConfig } from "../../main";
import GenericLessonTile from "./generic-lesson-tile";

type LessonPlacingShadowProps = {
    time?: Time;
}
type LessonPlacingShadowState = {}
export default class LessonPlacingShadow extends React.Component<LessonPlacingShadowProps, LessonPlacingShadowState> {

    render() {
        if (!this.props.time) return <></>

        return (
            <GenericLessonTile className="sa-lesson-placing-shadow"
                time={this.props.time}
            >
                <h4>{displayTime(this.props.time)} - {displayTime(sumTimes(this.props.time, { hour: 0, minutes: scheduleArrangerConfig.defaultLessonDuration }))}</h4>
            </GenericLessonTile>
        )
    }
}