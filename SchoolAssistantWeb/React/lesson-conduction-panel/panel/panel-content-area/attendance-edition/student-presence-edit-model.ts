import PresenceStatus from "../../../enums/presence-status";

export default interface StudentPresenceEditModel {
    id: number;
    presence: PresenceStatus;
}