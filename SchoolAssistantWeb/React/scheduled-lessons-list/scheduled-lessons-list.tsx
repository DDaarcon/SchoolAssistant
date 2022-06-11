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
type ScheduledLessonsListState = {
    entries: ScheduledLessonEntry[];
}

export default class ScheduledLessonsList extends React.Component<ScheduledLessonsListProps, ScheduledLessonsListState> {

    private _counter = 0;

    constructor(props) {
        super(props);

        this.state = {
            entries: this.props.entries
        }
        assignToState(this.props);
    }

    componentDidMount() {
        setTimeout(() => {
            this.setState({
                entries: [
                    {
                        startTimeTk: this._counter++,
                        className: "test",
                        duration: 45,
                        subjectName: "test",
                    },
                    ...this.state.entries
                ]
            });
        }, 3000);
    }

    render() {


        return (
            <table className={ScheduledLessonsListState.tableClassName + " scheduled-lessons-list"}>
                <ListHead />
                <ListBody
                    rows={this.state.entries}
                />
            </table>
        )
    }
}