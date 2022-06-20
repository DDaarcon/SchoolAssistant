import React from "react";
import { ClockColon, ClockDigit } from "./clock-components";
import './clock.css';

type ClockProps = {
    startTime: Date;
    duration: number;
}

export default class Clock extends React.Component<ClockProps> {

    constructor(props) {
        super(props);

        let endTime = new Date(props.startTime.getTime());
        endTime.setMinutes(endTime.getMinutes() + props.duration);

        this._endTimeSeconds = endTime.getTime() / 1000;

        this._fullDurationSeconds = this.props.duration * 60;
    }

    render() {
        this._digitKeyCounter = 0;

        return (
            <div className="lcp-clock">
                {this.toClockDigits(this._seconds)}
                <ClockColon />
                {this.toClockDigits(this._minutes)}
            </div>
        )
    }

    componentDidMount() {
        setInterval(() => {
            this.calculateLeftSeconds();
            this.forceUpdate();
        }, 1000);
    }

    private _leftSeconds: number;
    private _endTimeSeconds: number;
    private _fullDurationSeconds: number;
    private _digitKeyCounter: number;

    private get _minutes() {
        return Math.floor(this._leftSeconds / 60);
    }

    private get _seconds() {
        return this._leftSeconds % 60;
    }

    private calculateLeftSeconds() {
        this._leftSeconds = Math.floor(this._endTimeSeconds - new Date().getTime() / 1000);
        if (this._leftSeconds > this._fullDurationSeconds)
            this._leftSeconds = this._fullDurationSeconds;
    }


    private toClockDigits(num: number) {
        const digits = this.toDigitsIt(num);
        const comps: JSX.Element[] = [];
        for (const digit of digits)
            comps.push(
                <ClockDigit
                    key={this._digitKeyCounter++}
                    digit={digit}
                />
            );
        return comps;
    }

    private * toDigitsIt(num: number) {
        while (num >= 1) {
            yield num % 10;
            num = Math.floor(num / 10);
        }
    }

}