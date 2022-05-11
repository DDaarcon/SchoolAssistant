import React from "react"
import Validator from "../validator";

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

    protected setStateFn(
        modifyMethod: (state: TState) => void)
    {
        this.setState(prevState => {
            const state = { ...prevState };
            modifyMethod(state);
            return state;
        });
    }

    protected setStateFnData(
        modifyMethod: (data: TData) => void)
    {
        this.setStateFn(state => modifyMethod(state.data));
    }
}