import React from "react";
import { Input, SubmitButton } from "../../shared/form-controls";
import ModCompProps from "../../shared/lists/interfaces/shared-mod-comp-props";
import Loader, { LoaderSize, LoaderType } from "../../shared/loader";
import { ResponseJson } from "../../shared/server-connection";
import Validator from "../../shared/validator";
import { server } from "../main";
import SubjectListEntry from "./interfaces/subject-list-entry";

type SubjectModCompProps = ModCompProps;
type SubjectModCompState = {
    awaitingData: boolean;
    data: SubjectListEntry;
}

export default class SubjectModComp extends React.Component<SubjectModCompProps, SubjectModCompState> {
    private _validator = new Validator<SubjectListEntry>();

    constructor(props) {
        super(props);

        this.state = {
            awaitingData: this.props.recordId > 0,
            data: {
                name: ''
            }
        }

        this._validator.forModelGetter(() => this.state.data);
        this._validator.setRules({
            name: { notNull: true, notEmpty: "Nazwa nie może być pusta." }
        })

        if (this.props.recordId)
            this.fetchAsync();
    }

    private async fetchAsync() {
        let data = await server.getAsync<SubjectListEntry>("SubjectDetails", {
            id: this.props.recordId
        });

        this.setState({ data, awaitingData: false });
    }


    onNameChange: React.ChangeEventHandler<HTMLInputElement> = (event) => {
        const value = event.target.value;

        this.setState(prevState => {
            const data: SubjectListEntry = { ...prevState.data };
            data.name = value;
            return { data };
        });

        this.props.onMadeAnyChange();
    }

    onSubmitAsync: React.FormEventHandler<HTMLFormElement> = async (event) => {
        event.preventDefault();

        if (!this._validator.validate()) {
            this.forceUpdate();
            return;
        }

        let response = await server.postAsync<ResponseJson>("SubjectData", undefined, this.state.data);

        if (response.success)
            await this.props.reloadAsync();
        else
            console.debug(response);
    }

    render() {
        if (this.state.awaitingData)
            return (
                <Loader
                    enable={true}
                    size={LoaderSize.Medium}
                    type={LoaderType.DivWholeSpace}
                />
            )

        return (
            <div>
                <form onSubmit={this.onSubmitAsync}>
                    <Input
                        name="name-input"
                        label="Nazwa"
                        value={this.state.data.name}
                        onChange={this.onNameChange}
                        errorMessages={this._validator.getErrorMsgsFor('name')}
                        type="text"
                    />

                    <SubmitButton
                        value="Zapisz"
                    />
                </form>
            </div>
        )
    }
}