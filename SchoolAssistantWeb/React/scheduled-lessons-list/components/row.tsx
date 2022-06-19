import React from "react";
import HeldClasses from "../interfaces/held-classes";
import ScheduledLessonsListState from "../scheduled-lessons-list-state";
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
        let closeOrOngoing = this.isSoon() && this.isBeforeEnd();


        if (closeOrOngoing) {
            return <button
                className="conduct-btn"
            >
                <span>Poprowadź zajęcia</span>
            </button>
        }
        else if (this.props.heldClasses)
        {
            return <button
                className="see-past-details-btn"
            >
                <span>Szczegóły</span>
            </button>
        }
        else if (this.props.startTime < new Date()) {
            return <button
                className="see-omitted-btn"
            >
                <span>Uzupełnij</span>
            </button>
        }
        else {
            return <button
                className="see-upcomming-btn"
            >
                <span>Szczegóły nadchodzących</span>
            </button>
        }
    }
}

