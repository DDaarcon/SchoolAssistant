import StudentDetails from "./student-details";
import StudentRegisterRecordListEntry from "./student-reg-rec-list-entry";

export default interface StudentModificationData {
    data: StudentDetails;
    registerRecords: StudentRegisterRecordListEntry[];
}