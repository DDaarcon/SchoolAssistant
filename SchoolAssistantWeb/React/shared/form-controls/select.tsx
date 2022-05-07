import * as React from "react";
import defaultErrorMessage from './default-error-msg';

type SelectProps = {
    label: string;
    name: string;
    value: number | string[] | undefined;
    onChange: React.ChangeEventHandler<HTMLSelectElement>;
    options: React.ReactNode;
    hasErrors?: boolean;
    errorMessages: string[];
    multiple?: boolean;
}
const Select = (props: SelectProps) => {
    const hasErrors = props.hasErrors ?? props.errorMessages.length;
    return (
        <div className="form-group">
            <label htmlFor={props.name}>{props.label}</label>
            <select
                className={"form-select" + (hasErrors ? ' is-invalid' : '')}
                name={props.name}
                value={props.value}
                onChange={props.onChange}
                multiple={props.multiple ?? false}
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
export default Select;