export interface FetchScheduledLessonsRequest {
    fromTk?: number;
    toTk?: number;
    onlyUpcoming: boolean;
    limitTo?: number;
}