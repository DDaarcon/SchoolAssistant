import React from "react";
import ScheduledLessonListEntry from "../interfaces/scheduled-lesson-entry";
import Row from "./row";
import './list-body.css';
import ScheduledLessonsListState from "../scheduled-lessons-list-state";

type ListBodyProps = {
    rows: ScheduledLessonListEntry[];
}

export default class ListBody extends React.Component<ListBodyProps> {
    private _laterRenders = false;

    componentDidMount() {
        this._laterRenders = true;
    }

    render() {
        // TODO: map to RowProps on receive from server
        const rows = this.props.rows.map((x, index) => {
            x.startTime ??= new Date(x.startTimeTk);

            return (
                <Row
                    key={x.startTimeTk}
                    entryIndex={index}
                    isIncoming={x.startTimeTk == ScheduledLessonsListState.incomingAtTk}
                    startTime={x.startTime}
                    duration={x.duration}
                    className={x.className}
                    subjectName={x.subjectName}
                    heldClasses={x.heldClasses}
                    isNew={this._laterRenders && x.alreadyAdded != true}
                />
            )
        });

        this.props.rows.forEach(entry => entry.alreadyAdded = true);

        return (
            <tbody className={ScheduledLessonsListState.tbodyClassName + " scheduled-lessons-list-body"}>
                {rows}
            </tbody>
        )
    }
}