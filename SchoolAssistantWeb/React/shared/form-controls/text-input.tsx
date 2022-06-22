import React from "react";
import Input from "./input";

type TextInputProps = {
    label?: string;
    name: string;
    value?: string | number;
    onChange: (value: string) => void;
    hasErrors?: boolean;
    errorMessages?: string[];
    warningMessages?: string[];
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
            onChangeV={props.onChange}
        />
    );
}
export default TextInput;