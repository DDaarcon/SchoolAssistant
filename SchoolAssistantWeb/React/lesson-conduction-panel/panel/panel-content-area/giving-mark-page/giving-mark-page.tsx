import React from "react";
import ModCompBase from "../../../../shared/form-controls/mod-comp-base";
import MarkInput from "../../../marks/mark-input";
import GiveMarkModel from "./give-mark-model";

type GivingMarkPageProps = {}
type GivingMarkPageState = {
    data: GiveMarkModel;
}

export default class GivingMarkPage extends ModCompBase<GiveMarkModel, GivingMarkPageProps, GivingMarkPageState> {

    constructor(props) {
        super(props);

        this.state = {
            data: {
            }
        };
    }

    render() {
        return (
            <div className="giving-mark-details">
                <MarkInput
                    mark={this.state.data.mark}
                    onChange={mark => this.setStateFnData(data => data.mark = mark)}
                />
            </div>
        )
    }
}