import React from 'react';
import defaultErrorMessage from './default-error-msg';

type InputProps = {
    label: string;
    name: string;
    type: React.HTMLInputTypeAttribute;
    value?: string | number;
    checked?: boolean;
    onChange: React.ChangeEventHandler<HTMLInputElement>;
    hasErrors?: boolean;
    errorMessages?: string[];
    disabled?: boolean;
    className?: string;
    placeholder?: string;
}
const Input = (props: InputProps) => {
    const hasErrors = props.hasErrors ?? props.errorMessages?.length ?? false;
    return (
        <div className="form-group">
            <label htmlFor={props.name}>{props.label}</label>
            <input
                className={"form-select" + (hasErrors ? ' is-invalid' : '') + " " + props.className}
                type={props.type}
                name={props.name}
                value={props.value}
                onChange={props.onChange}
                disabled={props.disabled}
                checked={props.checked}
                placeholder={props.placeholder}
            />
            {hasErrors ? (
                <div className="invalid-feedback">
                    {props.errorMessages?.map(x => defaultErrorMessage(x)).join(' ')}
                </div>
            ) : undefined}
        </div>
    )
}
export default Input;