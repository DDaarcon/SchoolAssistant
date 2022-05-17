export default interface ScheduleEvent {
    id: string;
    title: string;
    start: Date;
    end: Date;
    lecturer: string;
    room: string;
    class: string;
    subject: string;
}