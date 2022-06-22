import React from "react";
import { Option, Select, TextInput } from "../../shared/form-controls";

type MarkInputProps = {
    prefix: '' | '-' | '+';

}

export default class MarkInput extends React.Component<MarkInputProps> {

    render() {
        return (
            <>
                <Select
                    name="mark-prefix-input"
                    options={this._prefixOptions}
                    containerClassName="mark-prefix-input-container"
                    inputClassName="mark-prefix-input"
                    onChangeId={ }
                />
                <TextInput
                    name="mark-input"
                    containerClassName="mark-input-container"
                    inputClassName="mark-input"
                    value={ }
                    onChange={ }
                />
            </>
        )
    }

    private _prefixOptions: Option<string>[] = [
        { label: '', value: '' },
        { label: '-', value: '-' },
        { label: '+', value: '+' }
    ];
}