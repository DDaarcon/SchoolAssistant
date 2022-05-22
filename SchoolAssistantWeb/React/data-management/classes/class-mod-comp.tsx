import React from "react";
import { Input } from "../../shared/form-controls";
import ModCompProps from "../../shared/lists/interfaces/shared-mod-comp-props";
import Loader, { LoaderSize, LoaderType } from "../../shared/loader";
import { ResponseJson } from "../../shared/server-connection";
import Validator from "../../shared/validator";
import { server } from "../main";
import ClassDetails from "./interfaces/class-details";
import ClassModificationData from "./interfaces/class-modification-data";

type ClassModCompProps = ModCompProps;
type ClassModCompState = {
    awaitingData: boolean;
    data: ClassDetails;
}
export default class ClassModComp extends React.Component<ClassModCompProps, ClassModCompState> {
    private _validator = new Validator<ClassDetails>();

    constructor(props) {
        super(props);

        this.state = {
            awaitingData: this.props.recordId > 0,
            data: {
                grade: 1,
                distinction: '',
                specialization: ''
            }
        }

        this._validator.forModelGetter(() => this.state.data);
        this._validator.setRules({
            grade: {
                notNull: true, other: (model, prop) => {
                    if (model.grade < 0) return {
                        error: "Wartość poniżej 0 jest nieprawidłowa",
                        on: prop
                    }
                }
            },
            distinction: {
                other: (model, prop) => {
                    if (model.distinction?.length > 4) return {
                        error: "Rozróżnienie klasy powinno być krótsze",
                        on: prop
                    }
                }
            }
        })

        if (this.props.recordId)
            this.fetchAsync();
    }

    private async fetchAsync() {
        let response = await server.getAsync<ClassModificationData>("ClassModificationData", {
            id: this.props.recordId
        });

        this.setState({ data: response.data, awaitingData: false });
    }

    createOnTextChangeHandler: (property: keyof ClassDetails) => React.ChangeEventHandler<HTMLInputElement> = (property) => {
        return (event) => {
            const value = event.target.value;

            this.setState(prevState => {
                const data: ClassDetails = { ...prevState.data };
                data[property] = (value as unknown) as never;
                return { data };
            });

            this.props.onMadeAnyChange();
        }
    }

    onSubmitAsync: React.FormEventHandler<HTMLFormElement> = async (event) => {
        event.preventDefault();

        if (!this._validator.validate()) {
            this.forceUpdate();
            return;
        }

        let response = await server.postAsync<ResponseJson>("ClassData", undefined, {
            ...this.state.data
        });

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
                        name="grade-input"
                        label="Numer klasy"
                        value={this.state.data.grade}
                        onChange={this.createOnTextChangeHandler('grade')}
                        errorMessages={this._validator.getErrorMsgsFor('grade')}
                        type="number"
                    />
                    <Input
                        name="distinction-input"
                        label="Dodatkowe rozróżnienie"
                        value={this.state.data.distinction}
                        onChange={this.createOnTextChangeHandler('distinction')}
                        errorMessages={this._validator.getErrorMsgsFor('distinction')}
                        type="text"
                    />
                    <Input
                        name="specialization-input"
                        label="Kierunek"
                        value={this.state.data.specialization}
                        onChange={this.createOnTextChangeHandler('specialization')}
                        errorMessages={this._validator.getErrorMsgsFor('specialization')}
                        type="text"
                    />

                    <div className="form-group">
                        <input
                            type="submit"
                            value="Zapisz"
                            className="form-control"
                        />
                    </div>
                </form>
            </div>
        )
    }
}