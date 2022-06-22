﻿import React from "react";
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
                description: ""
            }
        };

        this._validator.setRules({
            description: {
                notNull: true, notEmpty: true
            },
            mark: {
                notNull: true,
                other: (model, on) => {
                    // TODO: validate mark

                    return undefined;
                }
            },
            studentId: {
                notNull: true
            },
            weight: {
                other: (model, on) => {
                    // TODO: validate weight
                    return undefined;
                }
            }
        });
    }

    render() {
        return (
            <div className="giving-mark-details">
                <MarkInput
                    mark={this.state.data.mark}
                    onChange={mark => this.setStateFnData(data => data.mark = mark)}
                    errorMessages={this._validator.getErrorMsgsFor('mark')}
                    warningMessages={this._validator.getWarningMsgsFor('mark')}
                />
            </div>
        )
    }
}