import FullCalendar from "@fullcalendar/react"
import timeGridPlugin from '@fullcalendar/timegrid';
import React from "react";
import DayOfWeek from "./enums/day-of-week";
import './schedule.css';

interface ScheduleConfig {
    locale?: string;
    hiddenDays: DayOfWeek[];
    startTime: string;
    endTime: string;
}

type ScheduleProps = {
    config: ScheduleConfig;
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
                slotMinTime={this.props.config.startTime}
                slotMaxTime={this.props.config.endTime}
            />
        )
    }
}