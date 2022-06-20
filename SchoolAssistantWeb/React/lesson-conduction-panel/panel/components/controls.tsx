import React from "react";
import { IconLabelButton } from "../../../shared/components";
import LessonCondPanelContent from "../enums/lesson-cond-panel-content";
import './controls.css';

type ControlsProps = {
    goTo: (content: LessonCondPanelContent) => void;
}

export default class Controls extends React.Component<ControlsProps> {

    render() {
        return (
            <div className="lcp-controls-container">

                <BtnWrapper>
                    <IconLabelButton
                        label="Uzupełnij informacje o lekcji"
                        faIcon="fa-solid fa-circle-info"
                        onClick={() => this.props.goTo(LessonCondPanelContent.LessonDetailsEdit)}
                    />
                </BtnWrapper>

                <BtnWrapper>
                    <IconLabelButton
                        label="Sprawdź obecność"
                        faIcon="fa-solid fa-list-ol"
                        onClick={() => this.props.goTo(LessonCondPanelContent.AttendanceEdit)}
                    />
                </BtnWrapper>

                <BtnWrapper>
                    <IconLabelButton
                        label="Wpisz ocenę"
                        faIcon="fa-solid fa-highlighter"
                        onClick={() => this.props.goTo(LessonCondPanelContent.GivingMark)}
                    />
                </BtnWrapper>

                <BtnWrapper>
                    <IconLabelButton
                        label="Wpisz oceny z pracy klasowej"
                        faIcon="fa-solid fa-arrows-down-to-people"
                        onClick={() => this.props.goTo(LessonCondPanelContent.GivingGroupMark)}
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