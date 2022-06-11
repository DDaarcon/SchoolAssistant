
interface State {
    minutesBeforeLessonIsSoon?: number;
    incomingAt?: Date;
    entryHeight: number;

    tableClassName: string;
    theadClassName: string;
    theadTrClassName: string;
    tbodyClassName: string;
    tbodyTrClassName: string;
}

const ScheduledLessonsListState: State = {
    entryHeight: 45,

    tableClassName: "",
    theadClassName: "",
    theadTrClassName: "",
    tbodyClassName: "",
    tbodyTrClassName: ""
}
export default ScheduledLessonsListState;

export function assignToState(values: any) {
    const props = Object.keys(values);

    for (const prop of props) {
        if (values[prop])
            ScheduledLessonsListState[prop] = values[prop];
    }
}