import React from "react"
import { displayTime } from "../../../help-functions";
import { Lesson } from "../../../interfaces/lesson";
import dataService from "../../../schedule-data-service";

type OverlappingLessonPadProps = {
    lesson: Lesson;
    refreshAsync: () => Promise<void>;
}
type OverlappingLessonPadState = {}
export default class OverlappingLessonPad extends React.Component<OverlappingLessonPadProps, OverlappingLessonPadState> {

    confirmAndRemoveAsync = async () => {
        if (await dataService.removeLessonAndGetResultAsync(this.props.lesson.id))
            await this.props.refreshAsync();
    }

    render() {
        return (
            <div className="lmc-overlap-pad">
                <div className="lmc-op-class">
                    {this.props.lesson.orgClass.name}
                </div>

                <div className="lmc-op-subject">
                    {this.props.lesson.subject.name}
                </div>

                <div className="lmc-op-time">
                    {displayTime(this.props.lesson.time)}
                </div>

                <div className="lmc-op-lecturer">
                    {this.props.lesson.lecturer.name}
                </div>

                <div className="lmc-op-room">
                    {this.props.lesson.room.name}
                </div>
                <div className="btn-group dropend lmc-op-more">
                    <button type="button" className="lmc-op-more-btn" data-bs-toggle="dropdown" aria-expanded="false">
                        <i className="fa-solid fa-ellipsis-vertical"></i>
                    </button>

                    <ul className="dropdown-menu">
                        <li><a className="dropdown-item"
                            onClick={this.confirmAndRemoveAsync}
                        >
                            Usuń zajęcia
                        </a></li>

                        <li><a className="dropdown-item" href="#">Przejdź do klasy {this.props.lesson.orgClass.name}</a></li>
                    </ul>
                </div>
            </div>
        )
    }
}