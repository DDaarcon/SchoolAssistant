import StudentRegisterRecordDetails from "./student-reg-rec-details";

export default interface StudentDetails {
    id?: number;

    registerRecordId?: number;
    registerRecord?: StudentRegisterRecordDetails;

    organizationalClassId?: number;

    numberInJournal?: number;
}