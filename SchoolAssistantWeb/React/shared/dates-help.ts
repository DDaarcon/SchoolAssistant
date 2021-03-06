
const timezoneOffsetMs = new Date().getTimezoneOffset() * 60000;


export const fixMilisecondsFromServer = (ms: number): number => {
    return ms + timezoneOffsetMs;
}

export const fixDateFromServer = (date: number | Date): Date | undefined => {
    if (date instanceof Date)
        date = date.getTime();

    if (typeof date != 'number')
        return undefined;

    return new Date(fixMilisecondsFromServer(date));
}



export const prepareMilisecondsForServer = (date: number | Date): number | undefined => {
    if (date instanceof Date)
        date = date.getTime();
    if (typeof date != 'number')
        return undefined;

    return date - timezoneOffsetMs;
}

export const prepareDateForServer = (date: number | Date): Date | undefined => {
    const ms = prepareMilisecondsForServer(date);
    if (ms)
        return new Date(ms);
    return undefined;
}