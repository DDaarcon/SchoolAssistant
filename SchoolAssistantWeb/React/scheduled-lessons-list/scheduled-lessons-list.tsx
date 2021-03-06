import React from "react";
import { fixDateFromServer, fixMilisecondsFromServer, prepareDateForServer, prepareMilisecondsForServer } from "../shared/dates-help";
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

    constructor(props) {
        super(props);

        this.fixDatesInEntries(this.props.entries.entries);

        this.state = {
            entries: this.props.entries.entries
        }

        assignToState(this.props.config);
        if (this.props.entries.incomingAtTk) {
            ScheduledLessonsListState.incomingAtTk = fixMilisecondsFromServer(this.props.entries.incomingAtTk);
        }

    }

    componentDidMount() {
        addEventListener("loadOlderLessons", (event: CustomEvent) => this.loadOlderLessonsAsync(event.detail.amount));
        addEventListener("loadNewerLessons", (event: CustomEvent) => this.loadNewerLessonsAsync(event.detail.amount));
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
        const toTk = earliest != null
            ? prepareMilisecondsForServer(earliest.startTimeTk - 1)
            : undefined;

        const req: FetchScheduledLessonsRequest = {
            onlyUpcoming: false,
            toTk,
            limitTo: amount
        };
        const res = await server.getAsync<ScheduledLessonListEntries>("Entries", req);

        if (!res)
            return;

        if (res.incomingAtTk && !ScheduledLessonsListState.incomingAtTk)
            ScheduledLessonsListState.incomingAtTk = fixMilisecondsFromServer(res.incomingAtTk);

        this.fixDatesInEntries(res.entries);

        this.setState({
            entries: [
                ...res.entries,
                ...this.state.entries
            ]
        });
    }

    private async loadNewerLessonsAsync(amount: number): Promise<void> {
        let fromTk;
        if (this.state.entries.length) {
            const latest = this.state.entries[this.state.entries.length - 1];
            const from = new Date(latest.startTimeTk);
            from.setMinutes(from.getMinutes() + latest.duration);

            fromTk = prepareMilisecondsForServer(from);
        }

        const req: FetchScheduledLessonsRequest = {
            onlyUpcoming: false,
            fromTk,
            limitTo: amount
        };

        const res = await server.getAsync<ScheduledLessonListEntries>("Entries", req);

        if (!res)
            return;

        if (res.incomingAtTk && !ScheduledLessonsListState.incomingAtTk)
            ScheduledLessonsListState.incomingAtTk = fixMilisecondsFromServer(res.incomingAtTk);

        this.fixDatesInEntries(res.entries);

        this.setState({
            entries: [
                ...this.state.entries,
                ...res.entries
            ]
        });
    }




    private fixDatesInEntries(entriesToFix: ScheduledLessonListEntry[]) {
        for (const entry of entriesToFix) {
            entry.startTime = fixDateFromServer(entry.startTimeTk);
            entry.startTimeTk = entry.startTime.getTime();
        }
    }
}