import React from "react"
import Validator from "../validator";

export type ModifyMethod<TData> = (state: TData) => void;

type ModCompBaseProps = {

}
type ModCompBaseState<TData> = {
    data: TData;
}
export default abstract class ModCompBase<TData, TProps extends ModCompBaseProps, TState extends ModCompBaseState<TData>> extends React.Component<TProps, TState>
{
    protected _validator = new Validator<TData>();

    constructor(props) {
        super(props);

        this._validator.forModelGetter(() => this.state.data);
    }


    protected setStateFn(...modifyMethod: ModifyMethod<TState>[])
    {
        this.setState(prevState => {
            const state = { ...prevState };

            (modifyMethod as Array<ModifyMethod<TState>>)
                .forEach(method => method ? method(state) : undefined);

            return state;
        });
    }


    protected setStateFnData(
        modifyMethod: (data: TData) => void)
    {
        this.setStateFn(state => modifyMethod(state.data));
    }
}