export const getEnumValues = (enumClass: object) => {
    const entries = Object.keys(enumClass);
    const names: number[] = [];
    for (let i = entries.length / 2; i < entries.length; i++) {
        names.push(enumClass[entries[i]]);
    }
    return names;
}

export const getEnumNames = (enumClass: object) => {
    const entries = Object.keys(enumClass);
    const values: string[] = [];
    for (let i = 0; i < entries.length / 2; i++) {
        values.push(enumClass[entries[i]]);
    }
    return values;
}

export const getEnumValuesAndNames = (enumClass: object) => {
    const names = getEnumNames(enumClass);
    const values = getEnumValues(enumClass);
    const namesValues: { name: string; value: number; }[] = [];

    for (let i = 0; i < names.length; i++) {
        namesValues.push({
            name: names[i],
            value: values[i]
        });
    }
    return namesValues;
}

export const isValidEnumValue = (enumClass: object, value: number) => {
    const values = getEnumValues(enumClass);

    return values.includes(value);
}