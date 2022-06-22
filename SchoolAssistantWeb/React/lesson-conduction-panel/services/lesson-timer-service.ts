class LessonTimerServiceImplementation {

    private _isSetUp = false;

    private _endTimeSec?: number;
    private _durationSec?: number;

    private _leftSec?: number;

    private _listeners: Set<() => void> = new Set;

    private _intervalId?: NodeJS.Timer;



    public get isSetUp() { return this._isSetUp; }
    
    public get minutes() {
        return Math.floor((this._leftSec ?? 0) / 60);
    }

    public get seconds() {
        return (this._leftSec ?? 0) % 60;
    }



    public setUp(startTime: Date, durationMin: number) {
        let endTime = new Date(startTime.getTime());
        endTime.setMinutes(endTime.getMinutes() + durationMin);

        this._endTimeSec = endTime.getTime() / 1000;

        this._durationSec = durationMin * 60;

        this._isSetUp = true;

        this.setInterval();
    }

    private setInterval() {
        if (this._intervalId)
            clearInterval(this._intervalId);
        this._intervalId = setInterval(() => {
            this.update();
        }, 1000)
    }




    public onUpdate(listener: () => void) {
        if (!this._listeners.has(listener))
            this._listeners.add(listener);
    }



    private update() {
        this.calculateLeftSeconds();
        this.callListeners();
    }

    private calculateLeftSeconds() {
        this._leftSec = Math.floor(this._endTimeSec - new Date().getTime() / 1000);
        if (this._leftSec > this._durationSec)
            this._leftSec = this._durationSec;
        if (this._leftSec < 0)
            this._leftSec = 0;
    }

    private callListeners() {
        for (const listener of this._listeners)
            listener?.();
    }
}
const LessonTimerService = new LessonTimerServiceImplementation;
export default LessonTimerService;