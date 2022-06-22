import React from 'react';
import defaultErrorMessage from './default-error-msg';

type InputProps = {
    label?: string;
    name: string;
    type: React.HTMLInputTypeAttribute;
    value?: string | number;
    checked?: boolean;
    onChange?: React.ChangeEventHandler<HTMLInputElement>;
    onChangeV?: (value: string) => void;

    hasErrors?: boolean;
    errorMessages?: string[];
    warningMessages?: string[];

    disabled?: boolean;
    containerClassName?: string;
    inputClassName?: string;
    placeholder?: string;
}
const Input = (props: InputProps) => {
    const hasErrors = props.hasErrors ?? props.errorMessages?.length ?? false;
    const hasWarnings = props.warningMessages?.length;

    const label = props.label?.length
        ? <label htmlFor={props.name}>{props.label}</label>
        : <></>;

    const onChange: React.ChangeEventHandler<HTMLInputElement>
        = props.onChange ?? (event => props.onChangeV?.(event.target.value));

    const input = (
        <input
            className={"form-control" + (hasErrors ? ' is-invalid' : '') + " " + (props.inputClassName ?? '')}
            type={props.type}
            name={props.name}
            value={props.value ?? ""}
            onChange={onChange}
            disabled={props.disabled}
            checked={props.checked}
            placeholder={props.placeholder}
        />);


    return (
        <div className={"form-group my-form-group " + (props.containerClassName ?? '')}>
            {props.type != 'checkbox'
                ? <>{label}{input}</>
                : <>{input}{label}</>
            }

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
export default Input;