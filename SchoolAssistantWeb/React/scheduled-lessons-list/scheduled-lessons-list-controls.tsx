import React from "react";
import { LoadNewerLessonsButton, LoadOlderLessonsButton } from "./components-controls/load-lessons-buttons";
import './scheduled-lessons-list-controls.css';

type ScheduledLessonsListControlsProps = {

}
type ScheduledLessonsListControlsState = {

}

export default class ScheduledLessonsListControls extends React.Component<ScheduledLessonsListControlsProps, ScheduledLessonsListControlsState> {

    render() {
        return (
            <div className="sll-controls-layout">
                <LoadOlderLessonsButton />

                <div className="sll-filters-panel">

                </div>

                <LoadNewerLessonsButton />
            </div>
        )
    }

}