import Time from "../interfaces/shared/time";

export const areTimesOverlappingByDuration = (timeAStart: Time, durationA: number, timeBStart: Time, durationB: number) => {
    const aStart = toMinutes(timeAStart);
    const aEnd = aStart + durationA;
    const bStart = toMinutes(timeBStart);
    const bEnd = bStart + durationB;

    const left = aStart > bStart && aStart < bEnd;
    const right = aEnd > bStart && aEnd < bEnd;
    const over = aStart <= bStart && aEnd >= bEnd;

    return left || right || over;
}

export const toMinutes = (time: Time) => time.hour * 60 + time.minutes;

export const sumTimes = (timeA: Time, timeB: Time): Time => {
    const summedMinutes = timeA.minutes + timeB.minutes;
    return {
        hour: timeA.hour + timeB.hour + Math.floor(summedMinutes / 60),
        minutes: summedMinutes % 60
    };
}

export const displayTime = (time: Time) => `${time.hour}:${displayMinutes(time.minutes)}`;
export const displayMinutes = (minutes: number) => minutes < 10 ? `0${minutes}` : minutes.toString();