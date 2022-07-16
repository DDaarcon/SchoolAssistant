export default interface ScheduledLessonListConfig {
    minutesBeforeLessonIsSoon?: number;
    entryHeight?: number;
    topicLengthLimit?: number;

    tableClassName: string;
    theadClassName: string;
    theadTrClassName: string;
    tbodyClassName: string;
    tbodyTrClassName: string;
}