import { DayOfWeek } from "./enums/day-of-week";
import { Time } from "./interfaces/shared";


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


export const nameForDayOfWeek = (day: DayOfWeek) => {
    switch (day) {
        case DayOfWeek.Monday: return "Poniedziałek";
        case DayOfWeek.Tuesday: return "Wtorek";
        case DayOfWeek.Wednesday: return "Środa";
        case DayOfWeek.Thursday: return "Czwartek";
        case DayOfWeek.Friday: return "Piątek";
        case DayOfWeek.Saturday: return "Sobota";
        case DayOfWeek.Sunday: return "Niedziela";
        default: return '';
    }
}