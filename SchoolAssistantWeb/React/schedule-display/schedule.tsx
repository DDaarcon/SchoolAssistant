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

    render() {
        return (
            <FullCalendar
                plugins={[timeGridPlugin]}
                locale={this.props.config.locale ?? 'pl'}
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
                events={this.props.events}
                eventDidMount={info => {
                    console.log(info.event.start);
                }}
            />
        )
    }
}