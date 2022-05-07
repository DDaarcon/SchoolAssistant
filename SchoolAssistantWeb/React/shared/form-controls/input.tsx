import React from 'react';
import defaultErrorMessage from './default-error-msg';

type InputProps = {
    label: string;
    name: string;
    type: React.HTMLInputTypeAttribute;
    value: string | number;
    onChange: React.ChangeEventHandler<HTMLInputElement>;
    hasErrors?: boolean;
    errorMessages: string[];
}
const Input = (props: InputProps) => {
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
export default Input;