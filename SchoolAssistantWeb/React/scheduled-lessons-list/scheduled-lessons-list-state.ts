import ScheduledLessonListConfig from "./interfaces/scheduled-lessons-list-config";

interface State extends ScheduledLessonListConfig {
    incomingAt?: Date;
}

const ScheduledLessonsListState: State = {
    tableClassName: "",
    theadClassName: "",
    theadTrClassName: "",
    tbodyClassName: "",
    tbodyTrClassName: ""
}
export default ScheduledLessonsListState;

export function assignToState(values: ScheduledLessonListConfig) {
    const props = Object.keys(values);

    for (const prop of props) {
        if (values[prop])
            ScheduledLessonsListState[prop] = values[prop];
    }
}