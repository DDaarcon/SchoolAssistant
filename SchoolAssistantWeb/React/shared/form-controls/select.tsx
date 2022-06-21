import * as React from "react";
import defaultErrorMessage from './default-error-msg';
import RSelect, { CSSObjectWithLabel, MultiValue, OptionProps, Options, StylesConfig } from 'react-select';

export interface Option<TValue extends number | string>{
    value: TValue;
    label: string;
}

export type OnChangeHandler<TOption extends Option<number | string>> =
    (value: TOption | MultiValue<TOption>) => void;

export type OnChangeIdHandler<TValue extends number | string> =
    (value: TValue | MultiValue<TValue>) => void;

type SelectProps<TValue extends number | string, TOption extends Option<TValue>> = {
    label?: string;
    name: string;
    value?: TValue | TOption | MultiValue<TValue> | MultiValue<TOption>;
    onChange?: OnChangeHandler<TOption>;
    onChangeId?: OnChangeIdHandler<TValue>;
    options: Options<TOption>;
    hasErrors?: boolean;
    errorMessages?: string[];
    warningMessages?: string[];
    multiple?: boolean;

    containerClassName?: string;
    inputClassName?: string;
    optionStyle?: (props: OptionProps<TOption>) => CSSObjectWithLabel;
}
export default class Select<TValue extends number | string, TOption extends Option<TValue>> extends React.Component<SelectProps<TValue, TOption>> {

    render() {
        const value = this.prepareValue();

        return (
            <div className={"form-group my-form-group " + (this.props.containerClassName ?? '')}>
                {this.renderLabel()}

                <RSelect<TOption>
                    className={(this._hasErrors || this._hasWarnings ? ' is-invalid' : '') + (this.props.containerClassName ?? '')}
                    name={this.props.name}
                    value={value}
                    onChange={this.onChange}
                    options={this.props.options}
                    //@ts-ignore
                    isMulti={this.props.multiple}
                    styles={this._styles}
                />


                {this._hasErrors ? (
                    <div className="invalid-feedback">
                        {this.props.errorMessages?.map(x => defaultErrorMessage(x)).join(' ')}
                    </div>
                ) : this._hasWarnings ? (
                    <div className="invalid-feedback warning-msg">
                        {this.props.warningMessages?.join(' ')}
                    </div>
                ) : undefined}
            </div>
        )
    }

    private _styles: StylesConfig<TOption> = {
        option: (provided, props) => ({
            ...provided,
            ...(this.props.optionStyle?.(props) ?? {})
        })
    }

    private get _hasErrors() { return this.props.hasErrors ?? this.props.errorMessages?.length; };
    private get _hasWarnings() { return this.props.warningMessages?.length; }


    private prepareValue() {
        if (this.props.value == null || this.props.value == undefined)
            return null;

        if (this.props.value instanceof Array)
            return this.props.value.map((v) => this.prepareOneValue(v));

        return this.prepareOneValue(this.props.value);
    }

    private prepareOneValue(value: TValue | Option<TValue>): TOption {
        const valueType = typeof value;
        if (valueType == 'number' || valueType == 'string') {
            return this.props.options.find(x => x.value == value);
        }
        return value as TOption;
    }

    private onChange: OnChangeHandler<TOption> = (values) => {
        this.props.onChange?.(values);
        if (values instanceof Array)
            this.props.onChangeId?.(values.map(x => x.value));
        else
            this.props.onChangeId?.(values.value);
    }

    private renderLabel() {
        if (this.props.label?.length)
            return <label htmlFor={this.props.name}>{this.props.label}</label>
        return <></>
    }
}