import React from "react";

type SubmitButtonProps = {
    value: string;
    containerClassName?: string;
    inputClassName?: string;
}

const SubmitButton = (props: SubmitButtonProps) => {


    return (
        <div className={"form-group " + (props.containerClassName?.length ? props.containerClassName : "")}>
            <input
                type="submit"
                value={props.value}
                className={"form-control " + (props.inputClassName?.length ? props.inputClassName : "")}
            />
        </div>
    )
}
export default SubmitButton;