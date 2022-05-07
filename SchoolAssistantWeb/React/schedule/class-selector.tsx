import React = require("react");
import { scheduleArrangerConfig, scheduleChangePageScreen, scheduleServer } from "./main";
import ScheduleArrangerPage from "./schedule-arranger-page";
import { ScheduleClassLessons, ScheduleClassSelectorEntry } from "./schedule-types";

type ScheduleClassSelectorPageProps = {
    entries: ScheduleClassSelectorEntry[];
}
type ScheduleClassSelectorPageState = {

}
export default class ScheduleClassSelectorPage extends React.Component<ScheduleClassSelectorPageProps, ScheduleClassSelectorPageState> {

    render() {
        return (
            <div className="sa-selector-classes">
                <h2>Wybierz klasę</h2>
                {this.props.entries.map(entry =>
                    <ClassEntry
                        key={entry.id}
                        {...entry }
                    />
                )}
            </div>
        )
    }
}

type ClassEntryProps = ScheduleClassSelectorEntry & {

}
const ClassEntry = (props: ClassEntryProps) => {
    const selectClass = () =>
        scheduleServer.getAsync<ScheduleClassLessons>("ClassLessons", { classId: props.id })
            .then((result) => {
                if (result == null) return;

                scheduleArrangerConfig.classId = props.id;
                scheduleChangePageScreen(
                    <ScheduleArrangerPage
                        classData={result}
                    />
                );
            });


    return (
        <div className="sa-selector-class-entry"
            onClick={selectClass}
        >
            <span className="sa-selector-class-name">
                {props.name}
            </span>
            <span className="sa-selector-class-spec">
                {props.specialization}
            </span>
        </div>
    )
}