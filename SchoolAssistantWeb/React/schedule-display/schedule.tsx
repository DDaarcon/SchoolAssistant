import React from "react";
import DayLessons from '../schedule-shared/interfaces/day-lessons';
import { Loader, LoaderSize, LoaderType } from "../shared/loader";
import ScheduleDisplayTimeline from "./components/schedule-display-timeline";
import ScheduleConfig from "./interfaces/schedule-config";
import './schedule.css';

type ScheduleProps = {
    config: ScheduleConfig;
    lessons: DayLessons[];
}
type ScheduleState = {
    showLoader: boolean;
}

export default class Schedule extends React.Component<ScheduleProps, ScheduleState> {
    constructor(props) {
        super(props);

        this.state = {
            showLoader: true
        }
    }

    render() {
        return (
            <div className="schedule-display-conainer">
                <ScheduleDisplayTimeline
                    config={this.props.config}
                    data={this.props.lessons}
                />
                {this._loader}
            </div>
        )
    }

    private _loaderRef: Loader;

    private get _loader() {
        return this.state.showLoader ?
            <Loader
                enable
                type={LoaderType.Absolute}
                size={LoaderSize.Medium}
                className="schedule-display-loader"
                ref={ref => this._loaderRef = ref}
            />
            : <></>
    }


    componentDidMount() {
        this._loaderRef.containerRef.classList.add('schedule-display-loader-hide');

        setTimeout(() => {
            this.setState({ showLoader: false });
        }, 1500);
    }
}