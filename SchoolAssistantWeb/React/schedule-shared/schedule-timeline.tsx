import React from 'react';
import { getEnumValues } from '../shared/enum-help';
import DayOfWeek from './enums/day-of-week';
import ScheduleTimelineConfig from './interfaces/props-models/schedule-timeline-config';
import './schedule-timeline.css';


type ScheduleTimelineProps= {
    config: ScheduleTimelineConfig;
    dayColumnFactory: (day: DayOfWeek) => React.ReactNode;
    timeColumn?: React.ReactNode;
    getReferenceOnMount?: (ref: HTMLDivElement) => void;
    className?: string;
}
type ScheduleTimelineState = {}

export default class ScheduleTimeline extends React.Component<ScheduleTimelineProps, ScheduleTimelineState>
{

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className={this._fullClassName}
                ref={ref => this._containerElement = ref}
            >

                {this.props.timeColumn ?? <></>}

                {this._daysOfWeek.map(day => this.props.dayColumnFactory!(day))}
            </div>
        )
    }

    protected _containerElement: HTMLDivElement;

    componentDidMount() {
        if (this._containerElement && this.props.getReferenceOnMount)
            this.props.getReferenceOnMount(this._containerElement)
    }

    private get _fullClassName() {
        let className = "schedule-timeline";

        if (this.props.className)
            className += " " + this.props.className;

        return className;
    }


    protected get _daysOfWeek() {
        const except = this.props.config.daysToHide ?? [];

        return getEnumValues(DayOfWeek)
            .filter(x => !except.includes(x)) as DayOfWeek[];
    }

}