import IdName from "../../schedule-shared/interfaces/shared/id-name";

export default interface LessonPrefab {
    subject: IdName;
    lecturer: IdName;
    room?: IdName;
}