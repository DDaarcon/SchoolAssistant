import React from "react";
import LessonTimerService from "../../services/lesson-timer-service";
import StoreAndSaveService from "../../services/store-and-save-service";
import TogglePanelService from "../../services/toggle-panel-service";
import './anchor.css';

type AnchorProps = {}
type AnchorState = {}

export default class Anchor extends React.Component<AnchorProps, AnchorState> {


    render() {
        return (
            <button className="lcp-anchor"
                onClick={this.click}
            >
                <div>
                    <span>Zajęcia</span>
                    {this.timeComponent()}
                </div>
            </button>
        )
    }

    private timeComponent() {
        if (LessonTimerService.isSetUp)
            return (
                <span>
                    {LessonTimerService.minutes}:{this.displaySeconds(LessonTimerService.seconds)}
                </span>
            );
        return <></>
    }

    private displaySeconds(seconds: number) {
        if (seconds < 10)
            return '0' + seconds.toString();
        return seconds.toString();
    }

    componentDidMount() {
        if (!LessonTimerService.isSetUp)
            LessonTimerService.setUp(
                StoreAndSaveService.startTime,
                StoreAndSaveService.duration
            );

        LessonTimerService.onUpdate(() => this.forceUpdate());
    }

    private click = () => {
        TogglePanelService.toggle();
    }
}