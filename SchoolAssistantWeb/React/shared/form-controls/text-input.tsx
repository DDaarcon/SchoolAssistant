import React from "react";
import Input from "./input";

type TextInputProps = {
    label: string;
    name: string;
    value?: string | number;
    onChange: (value: string) => void;
    hasErrors?: boolean;
    errorMessages?: string[];
    disabled?: boolean;
    containerClassName?: string;
    inputClassName?: string;
    placeholder?: string;
}

const TextInput = (props: TextInputProps) => {
    const { onChange, ...reducedProps } = props;
    return (
        <Input
            {...reducedProps}
            type="text"
            onChange={(ev) => props.onChange(ev.target.value)}
        />
    );
}
export default TextInput;