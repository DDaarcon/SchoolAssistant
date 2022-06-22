export default interface MarkModel {
    prefix?: MarkPrefix;
    value?: MarkValue;
}

export type MarkPrefix = '-' | '+';
export type MarkValue = 1 | 2 | 3 | 4 | 5 | 6;