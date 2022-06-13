import React from "react";
import ScheduledLessonListEntry from "../interfaces/scheduled-lesson-entry";
import Row from "./row";
import './list-body.css';
import ScheduledLessonsListState from "../scheduled-lessons-list-state";

type ListBodyProps = {
    rows: ScheduledLessonListEntry[];
    incomingAtTk?: number;
}

export default class ListBody extends React.Component<ListBodyProps> {

    render() {
        const rows = this.props.rows.map((x, index) => (
            <Row
                key={x.startTimeTk}
                entryIndex={index}
                isIncoming={x.startTimeTk == this.props.incomingAtTk}
                startTime={new Date(x.startTimeTk)}
                duration={x.duration}
                className={x.className}
                subjectName={x.subjectName}
                heldClasses={x.heldClasses}
                isNew={x.newlyAdded}
            />
        ));

        this.props.rows.forEach(entry => entry.newlyAdded = false);

        return (
            <tbody className={ScheduledLessonsListState.tbodyClassName + " scheduled-lessons-list-body"}>
                {rows}
            </tbody>
        )
    }
}