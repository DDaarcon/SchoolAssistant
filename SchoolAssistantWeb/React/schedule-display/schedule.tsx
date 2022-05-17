import FullCalendar from "@fullcalendar/react"
import timeGridPlugin from '@fullcalendar/timegrid';
import React from "react";
import ScheduleConfig from "./interfaces/schedule-config";
import ScheduleEvent from "./interfaces/schedule-event";
import './schedule.css';

type ScheduleProps = {
    config: ScheduleConfig;
    events: ScheduleEvent[];
}
type ScheduleState = { }
export default class Schedule extends React.Component<ScheduleProps, ScheduleState> {
    constructor(props) {
        super(props);

        this.ref = React.createRef<FullCalendar>();
    }

    private ref: React.RefObject<FullCalendar>;

    render() {
        return (
            <FullCalendar
                ref={this.ref}
                plugins={[timeGridPlugin]}
                locale={this.props.config.locale}
                headerToolbar={false}
                initialView='timeGridWeek'
                dayHeaderFormat={{
                    weekday: 'short'
                }}
                weekNumbers={false}
                slotLabelFormat={{
                    hour: 'numeric',
                    omitZeroMinute: true
                }}
                hiddenDays={this.props.config.hiddenDays ?? []}
                slotMinTime={{ hour: this.props.config.startHour }}
                slotMaxTime={{ hour: this.props.config.endHour }}
                allDaySlot={false}
                events={this.props.events}
                eventDidMount={info => {
                    console.log(info.event.start);
                }}
                nowIndicator={true}
                contentHeight="auto"
                height="100%"
            />
        )
    }
}