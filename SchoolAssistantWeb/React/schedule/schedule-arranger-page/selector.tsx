﻿import React from "react";
import { modalController } from "../../shared/modals";
import { DayLessons } from "../interfaces/day-lessons";
import { LessonModificationData } from "../interfaces/lesson-modification-data";
import { LessonPrefab } from "../interfaces/lesson-prefab";
import { LessonTimelineEntry } from "../interfaces/lesson-timeline-entry";
import scheduleDataService from "../schedule-data-service";
import AddLessonPrefabTile from "./selector/add-lesson-prefab-tile";
import LessonModificationComponent from "./selector/lesson-modification-component";
import LessonPrefabTile from "./selector/lesson-prefab-tile";

type ScheduleArrangerSelectorProps = {
    data: DayLessons[];
}
type ScheduleArrangerSelectorState = {
}
export default class ScheduleArrangerSelector extends React.Component<ScheduleArrangerSelectorProps, ScheduleArrangerSelectorState> {
    private _addPrefabModalId?: number;

    constructor(props) {
        super(props);

        this.preparePrefabs();

        addEventListener('newPrefab', (_) => this.forceUpdate());
    }

    private preparePrefabs() {
        const prefabs = this.props.data.flatMap(dayLessons => dayLessons.lessons).map(this.lessonToPrefab);
        const validPrefabs: LessonPrefab[] = [];

        for (let prefab of prefabs) {
            if (validPrefabs.some(x =>
                x.subject.id == prefab.subject.id
                && x.lecturer.id == prefab.lecturer.id
                && x.room.id == prefab.room.id)) continue;

            validPrefabs.push(prefab);
        }

        scheduleDataService.prefabs = validPrefabs;
    }

    private lessonToPrefab = (lesson: LessonTimelineEntry): LessonPrefab => ({
        subject: lesson.subject,
        lecturer: lesson.lecturer,
        room: lesson.room
    })

    openAddPrefabModal = () => {
        this._addPrefabModalId = modalController.add({
            children: (
                <LessonModificationComponent
                    submit={this.addPrefab}
                />
            )
        })
    }

    private addPrefab = (info: LessonModificationData) => {
        modalController.closeById(this._addPrefabModalId);

        scheduleDataService.addPrefab({
            subject: { id: info.subjectId, name: scheduleDataService.getSubjectName(info.subjectId) },
            lecturer: { id: info.teacherId, name: scheduleDataService.getTeacherName(info.teacherId) },
            room: { id: info.roomId, name: scheduleDataService.getRoomName(info.roomId) }
        });
    }

    render() {
        return (
            <div className="schedule-arranger-selector">
                {scheduleDataService.prefabs.map((prefab, index) => (
                    <LessonPrefabTile
                        key={index}
                        data={prefab}
                    />
                ))}
                <AddLessonPrefabTile
                    onClick={this.openAddPrefabModal}
                />
            </div>
        )
    }
}