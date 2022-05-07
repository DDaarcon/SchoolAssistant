import React from 'react';
import Select from "./select";

type MultiselectProps = {
    label: string;
    name: string;
    value: number[] | undefined;
    onChange: React.ChangeEventHandler<HTMLSelectElement>;
    options: React.ReactNode;
    hasErrors?: boolean;
    errorMessages: string[];
}
const Multiselect = (props: MultiselectProps) => {
    return (
        <Select
            label={props.label}
            name={props.name}
            value={props.value?.map(x => x.toString())}
            onChange={props.onChange}
            options={props.options}
            hasErrors={props.hasErrors}
            errorMessages={props.errorMessages}
            multiple={true}
        />
    )
}
export default Multiselect;