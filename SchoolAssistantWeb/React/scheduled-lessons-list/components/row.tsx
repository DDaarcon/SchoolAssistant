import React from "react";
import { prepareMilisecondsForServer } from "../../shared/dates-help";
import { ResponseJson } from "../../shared/server-connection";
import HeldClasses from "../interfaces/held-classes";
import OpenPanelRequest from "../interfaces/open-panel-request";
import ScheduledLessonsListState from "../scheduled-lessons-list-state";
import server from "../server";
import RowButton, { RowButtonProps } from "./row-button";
import './row.css';


type RowProps = {
    isIncoming: boolean;
    startTime: Date;
    duration: number;
    className: string;
    subjectName: string;
    heldClasses?: HeldClasses;

    entryIndex: number;
    isNew?: boolean;
}
type RowState = {

}

export default class Row extends React.Component<RowProps, RowState> {

    private _rowEl?: HTMLTableRowElement;

    constructor(props) {
        super(props);

    }

    componentDidMount() {
        if (this.props.isNew) {
            setTimeout(() => this._rowEl.classList.remove('squeezed'));
        }
    }

    render() {
        return (
            <tr id={(this.props.isIncoming ? "incoming-lesson" : "")}
                className={ScheduledLessonsListState.tbodyTrClassName + " " + (this.props.isNew ? "squeezed" : "")}
                ref={ref => this._rowEl = ref}
                {...ScheduledLessonsListState.entryHeight != undefined
                    ? { height: ScheduledLessonsListState.entryHeight }
                    : {}
                }
            >
                <td>
                    <span>{this.props.startTime.toLocaleString('pl-PL', this._dateFormat)}</span>
                </td>
                <td>
                    <span>{this.props.className}</span>
                </td>
                <td>
                    <span>{this.props.subjectName}</span>
                </td>
                {this.props.heldClasses == undefined ? (
                    <>
                        <td></td>
                        <td></td>
                    </>
                ) : (
                        <>
                            <td>
                                <span>{this.props.heldClasses.topic}</span>
                            </td>
                            <td>
                                <span>{this.props.heldClasses.amountOfPresentStudents} / {this.props.heldClasses.amountOfAllStudents}</span>
                            </td>
                        </>
                )}
                <td className="sll-entry-button-cell">
                    {this.renderButton()}
                </td>
            </tr>
        )
    }

    private _dateFormat: Intl.DateTimeFormatOptions = {
        day: "numeric",
        weekday: 'short',
        month: "short",
        hour: "numeric",
        minute: "2-digit"
    };

    private isSoon(): boolean {
        const closeTime = new Date(this.props.startTime.getTime());
        closeTime.setMinutes(closeTime.getMinutes() - ScheduledLessonsListState.minutesBeforeLessonIsSoon);

        const now = new Date();
        return closeTime <= now;
    }

    private isBeforeEnd(): boolean {
        const endTime = new Date(this.props.startTime.getTime());
        endTime.setMinutes(endTime.getMinutes() + this.props.duration);

        const now = new Date();
        return endTime >= now; 
    }


    private renderButton(): JSX.Element {
        const buttonProps: RowButtonProps = {
            onClickAsync: this.openPanelAsync,
            text: '',
        }

        let closeOrOngoing = this.isSoon() && this.isBeforeEnd();


        if (closeOrOngoing) {
            buttonProps.text = "Poprowadź zajęcia";
            buttonProps.className = "conduct-btn";
        }
        else if (this.props.heldClasses) {
            buttonProps.text = "Szczegóły";
            buttonProps.className = "see-past-details-btn";
        }
        else if (this.props.startTime < new Date()) {
            buttonProps.text = "Uzupełnij";
            buttonProps.className = "see-omitted-btn";
        }
        else {
            buttonProps.text = "Szczegóły nadchodzących";
            buttonProps.className = "see-upcomming-btn";
        }

        return <RowButton
            {...buttonProps}
        />
    }

    private openPanelAsync = async () => {
        const params: OpenPanelRequest = {
            scheduledTimeTk: prepareMilisecondsForServer(this.props.startTime)
        }

        const res = await server.getAsync<ResponseJson>("OpenPanel", params);

        if (res?.success)
            location.reload();
    }
}

