import ParentRegisterSubecordDetails from "./parent-reg-subrec-details";

export default interface StudentRegisterRecordDetails {
    id?: number;

    firstName: string;
    secondName?: string;
    lastName: string;

    dateOfBirth: string;
    placeOfBirth: string;

    personalId: string;
    address: string;

    firstParent: ParentRegisterSubecordDetails;
    secondParent?: ParentRegisterSubecordDetails;
}