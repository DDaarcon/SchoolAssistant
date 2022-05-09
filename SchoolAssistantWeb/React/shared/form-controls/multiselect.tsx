import React from 'react';
import { MultiValue, Options } from 'react-select';
import Select, { OnChangeHandler, OnChangeIdHandler, Option } from "./select";

type MultiselectProps<TValue extends number | string, TOption extends Option<TValue>> = {
    label: string;
    name: string;
    value?: MultiValue<TOption>;
    onChange?: OnChangeHandler<TValue>;
    onChangeId?: OnChangeIdHandler<TValue>;
    options: Options<TOption>;
    hasErrors?: boolean;
    errorMessages: string[];
}
const Multiselect = <TValue extends number | string>(props: MultiselectProps<TValue, Option<TValue>>) => {
    return (
        <Select
            label={props.label}
            name={props.name}
            value={props.value}
            onChange={props.onChange}
            onChangeId={props.onChangeId}
            options={props.options}
            hasErrors={props.hasErrors}
            errorMessages={props.errorMessages}
            multiple={true}
        />
    )
}
export default Multiselect;