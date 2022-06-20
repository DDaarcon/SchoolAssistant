import PresenceStatus from "../enums/presence-status";

export default interface ParticipatingStudentModel {
    id: number;
    numberInJournal: number;
    firstName: string;
    lastName: string;
    presence?: PresenceStatus;
}