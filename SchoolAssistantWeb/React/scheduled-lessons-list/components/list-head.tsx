import React from "react";
import ScheduledLessonsListState from "../scheduled-lessons-list-state";
import './list-head.css';

type ListHeadProps = {
}

export default class ListHead extends React.Component<ListHeadProps> {

    render() {
        return (
            <thead className={ScheduledLessonsListState.theadClassName + " scheduled-lessons-list-head"}>
                <tr className={ScheduledLessonsListState.theadTrClassName}>
                    <th>
                        <span>Czas</span>
                    </th>
                    <th>
                        <span>Klasa</span>
                    </th>
                    <th>
                        <span>Przedmiot</span>
                    </th>
                    <th>
                        <span>Temat zajęć</span>
                    </th>
                    <th>
                        <span>Frekwencja</span>
                    </th>
                    <th></th>
                </tr>
            </thead>
        )
    }
}