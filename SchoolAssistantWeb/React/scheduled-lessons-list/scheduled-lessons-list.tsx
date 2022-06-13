import React from "react";
import ServerConnection from "../shared/server-connection";
import ListBody from "./components/list-body";
import ListHead from "./components/list-head";
import { FetchScheduledLessonsRequest } from "./interfaces/fetch-scheduled-lessons-list";
import ScheduledLessonListEntries from "./interfaces/scheduled-lesson-entries";
import ScheduledLessonListEntry from "./interfaces/scheduled-lesson-entry";
import ScheduledLessonListConfig from "./interfaces/scheduled-lessons-list-config";
import ScheduledLessonsListState, { assignToState } from "./scheduled-lessons-list-state";
import './scheduled-lessons-list.css';
import server from "./server";

type ScheduledLessonsListProps = {
    entries: ScheduledLessonListEntries;
    config: ScheduledLessonListConfig;
}
type ScheduledLessonsListState = {
    entries: ScheduledLessonListEntry[];
}

export default class ScheduledLessonsList extends React.Component<ScheduledLessonsListProps, ScheduledLessonsListState> {

    private _counter = 0;

    constructor(props) {
        super(props);

        this.state = {
            entries: this.props.entries.entries
        }

        assignToState(this.props.config);
        if (this.props.entries.incomingAtTk)
            ScheduledLessonsListState.incomingAt = new Date(this.props.entries.incomingAtTk);

    }

    componentDidMount() {

        addEventListener("loadOlderLessons", (event: CustomEvent) => this.loadOlderLessonsAsync(event.detail.amount));

        setTimeout(() => {
            this.setState({
                entries: [
                    {
                        startTimeTk: this._counter++,
                        className: "test",
                        duration: 45,
                        subjectName: "test",
                        newlyAdded: true
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

    private async loadOlderLessonsAsync(amount: number): Promise<void> {
        const earliest = this.state.entries.length > 0 ? this.state.entries[0] : null;
        const toTk = earliest != null ? (earliest?.startTimeTk - 1) : undefined;

        const req: FetchScheduledLessonsRequest = {
            onlyUpcoming: false,
            toTk,
            limitTo: amount
        }
        const res = await server.getAsync("OlderLessons", req);


    }
}