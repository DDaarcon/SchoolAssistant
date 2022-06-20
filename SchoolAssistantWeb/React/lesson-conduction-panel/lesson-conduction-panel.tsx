import React from "react";
import BackgroundArea from "./components/background-area";
import './lesson-conduction-panel.css';
import Panel from "./panel/panel";

type LessonConductionPanelProps = {

}

export default class LessonConductionPanel extends React.Component<LessonConductionPanelProps> {

    render() {
        return (
            <>
                <BackgroundArea />

                <Panel />
            </>
        )
    }
}