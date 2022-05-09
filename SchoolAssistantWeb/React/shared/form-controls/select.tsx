import * as React from "react";
import defaultErrorMessage from './default-error-msg';
import RSelect, { MultiValue, Options } from 'react-select';

export interface Option<TValue extends number | string>{
    value: TValue;
    label: string;
}

export type OnChangeHandler<TValue extends number | string> =
    (value: Option<TValue> | MultiValue<Option<TValue>>) => void;

export type OnChangeIdHandler<TValue extends number | string> =
    (value: TValue | MultiValue<TValue) => void;

type SelectProps<TValue extends number | string, TOption extends Option<TValue>> = {
    label: string;
    name: string;
    value?: TValue | TOption | MultiValue<TOption>;
    onChange?: OnChangeHandler<TValue>;
    onChangeId?: OnChangeIdHandler<TValue>;
    options: Options<TOption>;
    hasErrors?: boolean;
    errorMessages: string[];
    multiple?: boolean;
}
export default class Select<TValue extends number | string> extends React.Component<SelectProps<TValue, Option<TValue>>> {
    private get _hasErrors() { return this.props.hasErrors ?? this.props.errorMessages.length; };


    private prepareValue() {
        if (this.props.value instanceof Array) {
            return this.props.value.map((v) => this.prepareOneValue(v));
        }
        return this.prepareOneValue(this.props.value);
    }

    private prepareOneValue(value: TValue | Option<TValue>): Option<TValue>{
        const valueType = typeof value;
        if (valueType == 'number' || valueType == 'string') {
            return { value: value as TValue, label: "" };
        }
        return value as Option<TValue>;
    }

    private onChange: OnChangeHandler<TValue> = (values) => {
        this.props.onChange?.(values);
        if (values instanceof Array)
            this.props.onChangeId?.(values.map(x => x.value));
        else
            this.props.onChangeId?.(values.value);
    }

    render() {
        const value = this.prepareValue();

        return (
            <div className="form-group">
                <label htmlFor={this.props.name}>{this.props.label}</label>
                <RSelect<Option<TValue>>
                    className={"form-select" + (this._hasErrors ? ' is-invalid' : '')}
                    name={this.props.name}
                    value={value}
                    onChange={this.onChange}
                    options={this.props.options}
                    //@ts-ignore
                    isMulti={this.props.multiple}
                />
                {this._hasErrors ? (
                    <div className="invalid-feedback">
                        {this.props.errorMessages?.map(x => defaultErrorMessage(x)).join(' ')}
                    </div>
                ) : undefined}
            </div>
        )
    }
}