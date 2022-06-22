import React from "react";
import MarkInput from "../../../marks/mark-input";

type GivingMarkPageProps = {}
type GivingMarkPageState = {}

export default class GivingMarkPage extends React.Component<GivingMarkPageProps, GivingMarkPageState> {

    render() {
        return (
            <div className="giving-mark-details">
                <MarkInput
                    prefix=""

                />
            </div>
        )
    }
}