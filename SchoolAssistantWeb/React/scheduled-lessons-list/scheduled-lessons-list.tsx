import React from "react";
import ListBody from "./components/list-body";
import ListHead from "./components/list-head";
import ScheduledLessonEntry from "./interfaces/scheduled-lesson-entry";
import ScheduledLessonsListState, { assignToState } from "./scheduled-lessons-list-state";
import './scheduled-lessons-list.css';

type ScheduledLessonsListProps = {
    entries: ScheduledLessonEntry[];

    minutesBeforeLessonIsSoon: number;
    entryHeight?: number;

    tableClassName?: string;
    theadClassName?: string;
    theadTrClassName?: string;
    tbodyClassName?: string;
    tbodyTrClassName?: string;
}

export default class ScheduledLessonsList extends React.Component<ScheduledLessonsListProps> {

    constructor(props) {
        super(props);

        assignToState(this.props);

        console.log(ScheduledLessonsListState.tableClassName);
    }

    render() {


        return (
            <table className={ScheduledLessonsListState.tableClassName + " scheduled-lessons-list"}>
                <ListHead />
                <ListBody
                    rows={this.props.entries}
                />
            </table>
        )
    }
}