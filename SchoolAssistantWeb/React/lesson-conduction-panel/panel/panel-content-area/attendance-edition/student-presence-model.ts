import PresenceStatus from "../../../enums/presence-status";

export default interface StudentPresenceModel {
    id: number;
    presence: PresenceStatus;
}