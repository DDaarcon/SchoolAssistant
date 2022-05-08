export default interface StaffPersonDetails {
    id?: number;
    firstName: string;
    secondName?: string;
    lastName: string;

    mainSubjectsIds?: number[];
    additionalSubjectsIds?: number[];
}