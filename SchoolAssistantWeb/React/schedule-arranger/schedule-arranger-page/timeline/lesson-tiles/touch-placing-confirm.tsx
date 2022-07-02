import React from "react";
import { displayTime, sumTimeAndMinutes } from "../../../../schedule-shared/help/time-functions";
import Time from "../../../../schedule-shared/interfaces/shared/time";
import { IconButton } from "../../../../shared/components";
import { scheduleArrangerConfig } from "../../../main";
import GenericLessonTile from "./generic-lesson-tile";

type TouchPlacingConfirmProps = {
    time?: Time;
}

export default class TouchPlacingConfirm extends React.Component<TouchPlacingConfirmProps> {

    render() {
        if (!this.props.time) return <></>

        return (
            <GenericLessonTile
                time={this.props.time}
                className="sa-lesson-touch-placing-confirm"
                forceAbove
            >
                <h4>{displayTime(this.props.time)} - {displayTime(sumTimeAndMinutes(this.props.time, scheduleArrangerConfig.defaultLessonDuration))}</h4>
                <IconButton
                    label="Zatwierdź"
                    faIcon="fa-solid fa-check"
                    onClick={() => console.log("submit")}
                />
            </GenericLessonTile>
        )
    }
}