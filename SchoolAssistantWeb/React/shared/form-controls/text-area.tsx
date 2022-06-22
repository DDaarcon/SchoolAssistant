import React from 'react';
import defaultErrorMessage from './default-error-msg';

type TextAreaProps = {
    label?: string;
    name: string;
    value?: string;
    onChange: (value: string) => void;

    hasErrors?: boolean;
    errorMessages?: string[];
    warningMessages?: string[];

    disabled?: boolean;
    containerClassName?: string;
    inputClassName?: string;
    placeholder?: string;
}
const TextArea = (props: TextAreaProps) => {
    const hasErrors = props.hasErrors ?? props.errorMessages?.length ?? false;
    const hasWarnings = props.warningMessages?.length;

    const label = props.label?.length
        ? <label htmlFor={props.name}>{props.label}</label>
        : <></>;


    return (
        <div className={"form-group my-form-group " + (props.containerClassName ?? '')}>
            {label}

            <textarea
                className={"form-control" + (hasErrors ? ' is-invalid' : '') + " " + (props.inputClassName ?? '')}
                name={props.name}
                value={props.value ?? ""}
                onChange={ev => props.onChange(ev.target.value)}
                disabled={props.disabled}
                placeholder={props.placeholder}
            />

            {hasErrors ? (
                <div className="invalid-feedback">
                    {props.errorMessages?.map(x => defaultErrorMessage(x)).join(' ')}
                </div>
            ) : hasWarnings ? (
                <div className="invalid-feedback warning-msg">
                    {props.warningMessages?.join(' ')}
                </div>
            ) : undefined}
        </div>
    )
}
export default TextArea;