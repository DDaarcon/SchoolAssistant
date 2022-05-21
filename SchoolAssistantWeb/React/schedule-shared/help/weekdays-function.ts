import DayOfWeek from "../enums/day-of-week";

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
