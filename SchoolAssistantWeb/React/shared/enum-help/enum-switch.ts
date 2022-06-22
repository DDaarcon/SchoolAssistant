import { getEnumValuesAndNames } from "../enum-help";

type EnumCases<TEnum> = {
    [index in keyof TEnum | "_"]?: () => void;
};

type EnumAssignCases<TEnum, TValue> = {
    [index in keyof TEnum | "_"]?: (() => TValue) | TValue;
}


export function enumAssignSwitch<TValue, TEnum = undefined>(_enum: TEnum, val: keyof TEnum | number, cases: EnumAssignCases<TEnum, TValue>): TValue | undefined {
    const _case = getCase<TEnum, TValue>(_enum, val, cases);

    if (_case instanceof Function)
        return _case();
    return _case;
}


export function enumSwitch<TEnum>(_enum: TEnum, val: keyof TEnum | number, cases: EnumCases<TEnum>): void {
    enumAssignSwitch<void, TEnum>(_enum, val, cases);
}









function getCase<TEnum, TValue>(_enum: TEnum, val: keyof TEnum | number, cases: EnumAssignCases<TEnum, TValue>): (() => TValue) | TValue | undefined {
    let name: keyof TEnum | undefined;

    if (typeof val == 'number') {
        const namesAndVals = getEnumValuesAndNames(_enum as unknown as object);

        name = namesAndVals.find(nameAndVal => nameAndVal.value == val)?.name as keyof TEnum | undefined;
    }
    else {
        name = val;
    }

    let _case = cases[name] as (() => TValue) | TValue | undefined;
    if (!_case)
        _case = cases['_'] as (() => TValue) | TValue | undefined;

    return _case;
}