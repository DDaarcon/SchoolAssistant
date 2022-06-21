import React from 'react';
import { CSSObjectWithLabel, MultiValue, OptionProps, Options } from 'react-select';
import Select, { OnChangeHandler, OnChangeIdHandler, Option } from "./select";

type MultiselectProps<TValue extends number | string, TOption extends Option<TValue>> = {
    label?: string;
    name: string;
    value?: MultiValue<TValue> | MultiValue<TOption>;
    onChange?: OnChangeHandler<TOption>;
    onChangeId?: OnChangeIdHandler<TValue>;
    options: Options<TOption>;
    hasErrors?: boolean;
    errorMessages?: string[];
    warningMessages?: string[];
    optionStyle?: <TInnerOption extends Option<TValue>>(props: OptionProps<TInnerOption>) => CSSObjectWithLabel;
}
const Multiselect = <TValue extends number | string, TOption extends Option<TValue>>(props: MultiselectProps<TValue, TOption>) => {
    return (
        <Select<TValue, TOption>
            label={props.label}
            name={props.name}
            value={props.value}
            onChange={props.onChange}
            onChangeId={props.onChangeId}
            options={props.options}
            hasErrors={props.hasErrors}
            errorMessages={props.errorMessages}
            warningMessages={props.warningMessages}
            multiple={true}
            optionStyle={props.optionStyle}
        />
    )
}
export default Multiselect;