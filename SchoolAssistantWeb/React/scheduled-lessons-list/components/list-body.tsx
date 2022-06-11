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

    render() {
        return (
            <tbody className={ScheduledLessonsListState.tbodyClassName + " scheduled-lessons-list-body"}>
                {this.props.rows.map(x => (
                    <Row
                        key={x.startTimeTk}
                        isIncoming={x.startTimeTk == this.props.incomingAtTk}
                        startTime={new Date(x.startTimeTk)}
                        duration={x.duration}
                        className={x.className}
                        subjectName={x.subjectName}
                        heldClasses={x.heldClasses}
                    />
                ))}
            </tbody>
        )
    }
}