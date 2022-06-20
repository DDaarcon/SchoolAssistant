import React from "react";
import { IconLabelButton } from "../../../shared/components";
import './controls.css';

type ControlsProps = {
    goToAttendanceEdit: () => void;
    goToGivingMark: () => void;
}

export default class Controls extends React.Component<ControlsProps> {

    render() {
        return (
            <div className="lcp-controls-container">

                <BtnWrapper>
                    <IconLabelButton
                        label="Sprawdź obecność"
                        faIcon="fa-solid fa-list-ol"
                        onClick={this.props.goToAttendanceEdit}
                    />
                </BtnWrapper>

                <BtnWrapper>
                    <IconLabelButton
                        label="Wpisz ocenę"
                        faIcon="fa-solid fa-highlighter"
                        onClick={this.props.goToGivingMark}
                    />
                </BtnWrapper>
            </div>
        )
    }
}

const BtnWrapper = ({ children }: { children: React.ReactNode }) => (
    <div className="btn-container">
        {children}
    </div>
)