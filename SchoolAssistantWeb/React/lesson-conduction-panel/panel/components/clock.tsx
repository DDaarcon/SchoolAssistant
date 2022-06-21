import React from "react";
import LessonTimerService from "../../services/lesson-timer-service";
import { ClockColon, ClockDigit } from "./clock-components";
import './clock.css';

type ClockProps = {
    startTime: Date;
    duration: number;
}

export default class Clock extends React.Component<ClockProps> {

    render() {
        if (!LessonTimerService.isSetUp)
            return <></>;

        this._digitKeyCounter = 0;

        return (
            <div className="lcp-clock">
                {this.toClockDigits(LessonTimerService.seconds, 2)}
                <ClockColon
                    key="colon"
                />
                {this.toClockDigits(LessonTimerService.minutes)}
            </div>
        )
    }

    componentDidMount() {
        if (!LessonTimerService.isSetUp)
            LessonTimerService.setUp(this.props.startTime, this.props.duration);

        LessonTimerService.onUpdate(() => this.forceUpdate());
    }

    private _digitKeyCounter: number;



    private toClockDigits(num: number, minDigits?: number) {
        const digits = this.toDigitsIt(num);
        const comps: JSX.Element[] = [];

        for (const digit of digits)
            comps.push(this.getClockDigit(digit));

        while (comps.length < minDigits)
            comps.push(this.getClockDigit(0));

        return comps;
    }

    private getClockDigit(digit: number) {
        return (
            <ClockDigit
                key={this._digitKeyCounter++}
                digit={digit}
            />
        )
    }

    private * toDigitsIt(num: number) {
        while (num >= 1) {
            yield num % 10;
            num = Math.floor(num / 10);
        }
    }

}