import React from "react";
import { Input, SubmitButton } from "../../shared/form-controls";
import ModCompProps from "../../shared/lists/interfaces/shared-mod-comp-props";
import { Loader, LoaderSize, LoaderType } from "../../shared/loader";
import { ResponseJson } from "../../shared/server-connection";
import Validator from "../../shared/validator";
import { server } from "../main";
import RoomDetails from "./interfaces/room-details";
import RoomModificationData from "./interfaces/room-modification-data";

type RoomModCompProps = ModCompProps;
type RoomModCompState = {
    awaitingData: boolean;
    data: RoomDetails;
    defaultName?: string;
}

export default class RoomModComp extends React.Component<RoomModCompProps, RoomModCompState> {
    private _validator = new Validator<RoomDetails>()

    constructor(props) {
        super(props);

        this.state = {
            awaitingData: true,
            data: {
                name: '',
                floor: 0
            }
        }

        this._validator.forModelGetter(() => this.state.data);
        this._validator.setRules({
            floor: {
                notNull: true,
                other: (model, prop) => model[prop] < 0
                    ? {
                        error: 'Piętro musi mieć wartość większą niż 0',
                        on: prop
                    } : undefined
            },
            name: { notNull: true, notEmpty: 'Pole nie może być puste' }
        })

        if (this.props.recordId)
            this.fetchAsync();
        else
            this.fetchDefaultNameAsync();
    }

    private async fetchAsync() {
        let res = await server.getAsync<RoomModificationData>("RoomModificationData", {
            id: this.props.recordId
        });

        this.setState({ data: res.data, defaultName: res.defaultName, awaitingData: false });
    }

    private async fetchDefaultNameAsync() {
        const name = await server.getAsync<string>("RoomDefaultName");

        this.setState({ defaultName: name, awaitingData: false });
    }

    createOnChangeHandler: (property: keyof RoomDetails) => React.ChangeEventHandler<HTMLInputElement> = (property) => {
        return (event) => {
            const value = event.target.value;

            this.setState(prevState => {
                const data: RoomDetails = { ...prevState.data };
                data[property] = (value as unknown) as never;
                return { data };
            });

            this.props.onMadeAnyChange();
        }
    }

    onSubmitAsync: React.FormEventHandler<HTMLFormElement> = async (event) => {
        event.preventDefault();

        if (this.state.data.name == "") this.state.data.name = this.state.defaultName;

        if (!this._validator.validate()) {
            this.forceUpdate();
            return;
        }

        let response = await server.postAsync<ResponseJson>("RoomData", undefined, this.state.data);

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
                        placeholder={this.state.defaultName}
                        onChange={this.createOnChangeHandler('name')}
                        errorMessages={this._validator.getErrorMsgsFor('name')}
                        type="text"
                    />
                    <Input
                        name="number-input"
                        label="Numer"
                        value={this.state.data.number}
                        onChange={this.createOnChangeHandler('number')}
                        errorMessages={this._validator.getErrorMsgsFor('number')}
                        type="number"
                    />
                    <Input
                        name="floor-input"
                        label="Piętro"
                        value={this.state.data.floor}
                        onChange={this.createOnChangeHandler('floor')}
                        errorMessages={this._validator.getErrorMsgsFor('floor')}
                        type="number"
                    />

                    <SubmitButton
                        value="Zapisz"
                    />
                </form>
            </div>
        )
    }
}