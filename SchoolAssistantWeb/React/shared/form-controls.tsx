import React from "react";

type FormMultiselectProps = {
    label: string;
    name: string;
    value: number[];
    onChange: React.ChangeEventHandler<HTMLSelectElement>;
    options: React.ReactNode;
    hasErrors?: boolean;
    errorMessages: string[];
}
export const FormMultiselect = (props: FormMultiselectProps) => {
    return (
        <FormSelect
            label={props.label}
            name={props.name}
            value={props.value.map(x => x.toString())}
            onChange={props.onChange}
            options={props.options}
            hasErrors={props.hasErrors}
            errorMessages={props.errorMessages}
        />
    )
}






type FormSelectProps = {
    label: string;
    name: string;
    value: number | string[];
    onChange: React.ChangeEventHandler<HTMLSelectElement>;
    options: React.ReactNode;
    hasErrors?: boolean;
    errorMessages: string[];
}
export const FormSelect = (props: FormSelectProps) => {
    const hasErrors = props.hasErrors ?? props.errorMessages.length;
    return (
        <div className="form-group">
            <label htmlFor={props.name}>{props.label}</label>
            <select
                className={"form-select" + (hasErrors ? ' is-invalid' : '')}
                name={props.name}
                value={props.value}
                onChange={props.onChange}
            >
                {props.options}
            </select>
            {hasErrors ? (
                <div className="invalid-feedback">
                    {props.errorMessages?.map(x => defaultErrorMessage(x)).join(' ')}
                </div>
            ) : undefined}
        </div>
    )
}





type FormInputProps = {
    label: string;
    name: string;
    type: React.HTMLInputTypeAttribute;
    value: string | number;
    onChange: React.ChangeEventHandler<HTMLInputElement>;
    hasErrors?: boolean;
    errorMessages: string[];
}
export const FormInput = (props: FormInputProps) => {
    const hasErrors = props.hasErrors ?? props.errorMessages.length;
    return (
        <div className="form-group">
            <label htmlFor={props.name}>{props.label}</label>
            <input
                className={"form-select" + (hasErrors ? ' is-invalid' : '')}
                type={props.type}
                name={props.name}
                value={props.value}
                onChange={props.onChange}
            />
            {hasErrors ? (
                <div className="invalid-feedback">
                    {props.errorMessages?.map(x => defaultErrorMessage(x)).join(' ')}
                </div>
            ) : undefined}
        </div>
    )
}





function defaultErrorMessage(error: string) {
    switch (error) {
        case 'null': return "Należy uzupełnić to pole.";
        case 'empty': return "Należy wybrać wartości.";
        case 'invalidDate': return "Nieprawidłowa data.";
        default: return error;
    }
}