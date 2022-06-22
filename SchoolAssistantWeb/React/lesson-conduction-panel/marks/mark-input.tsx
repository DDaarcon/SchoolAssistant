import React from "react";
import { Option, Select, TextInput } from "../../shared/form-controls";
import MarkModel, { MarkPrefix, MarkValue } from "./mark-model";

type MarkInputProps = {
    mark?: MarkModel;
    onChange: (mark?: MarkModel) => void;
    errorMessages?: string[];
    warningMessages?: string[];
}

export default class MarkInput extends React.Component<MarkInputProps> {

    render() {
        return (
            <>
                <TextInput
                    label="Ocena"
                    errorMessages={this.props.errorMessages}
                    warningMessages={this.props.warningMessages}
                    
                    name="mark-input"
                    containerClassName="mark-input-container"
                    inputClassName="mark-input"
                    value={this._value}
                    onChange={this.change}
                />
            </>
        )
    }

    private get _value() {
        if (!this.props.mark)
            return "";

        let value = this.props.mark.value?.toString(),
            prefix = this.props.mark.prefix as string;

        value ??= "";
        prefix ??= "";

        return prefix + value;
    }

    private change = (value: string) => {
        if (!value?.length)
            this.props.onChange(undefined);

        let mark: MarkModel | undefined = {};

        if (value.length >= 1) {
            const prefix = this.tryConvertMarkPrefix(value[0]);

            if (prefix) {
                mark.prefix = prefix;

                value = value.substring(1);
            }
        }

        if (value.length >= 1) {
            const markValue = this.tryConvertMarkValue(value[0]);

            if (markValue) {
                mark.value = markValue;
            }
        }

        this.props.onChange(mark);
    }

    private tryConvertMarkPrefix(prefixText: string): MarkPrefix | undefined {

        if (['-', '+'].includes(prefixText)) {
            return prefixText as MarkPrefix;
        }

        return undefined;
    }

    private tryConvertMarkValue(markText: string): MarkValue | undefined {
        const mark = parseInt(markText);

        if ([1, 2, 3, 4, 5, 6].includes(mark)) {
            return mark as MarkValue;
        }

        return undefined;
    }
}