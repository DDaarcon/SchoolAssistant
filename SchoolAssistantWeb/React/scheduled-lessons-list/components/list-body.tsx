import React from "react";
import ScheduledLessonEntry from "../interfaces/scheduled-lesson-entry";
import Row from "./row";
import './list-body.css';
import ScheduledLessonsListState from "../scheduled-lessons-list-state";

type ListBodyProps = {
    rows: ScheduledLessonEntry[];
    incomingAtTk?: number;
}

export default class ListBody extends React.Component<ListBodyProps> {

    private _isFirstLoad = true;

    componentDidMount() {
        this._isFirstLoad = false;
    }

    render() {
        console.log("is first load: " + (this._isFirstLoad ? "true" : "false"))

        return (
            <tbody className={ScheduledLessonsListState.tbodyClassName + " scheduled-lessons-list-body"}>
                {this.props.rows.map((x, index) => (
                    <Row
                        key={x.startTimeTk}
                        entryIndex={index}
                        isIncoming={x.startTimeTk == this.props.incomingAtTk}
                        startTime={new Date(x.startTimeTk)}
                        duration={x.duration}
                        className={x.className}
                        subjectName={x.subjectName}
                        heldClasses={x.heldClasses}
                        isNew={!this._isFirstLoad}
                    />
                ))}
            </tbody>
        )
    }
}