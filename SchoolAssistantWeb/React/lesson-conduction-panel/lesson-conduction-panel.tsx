import React from "react";
import BackgroundArea from "./components/background-area";
import LessonConductionPanelModel from "./interfaces/lesson-conduction-panel-model";
import './lesson-conduction-panel.css';
import Panel from "./panel/panel";
import StoreAndSaveService from "./services/store-and-save-service";

type LessonConductionPanelProps = {
    model: LessonConductionPanelModel;
}

export default class LessonConductionPanel extends React.Component<LessonConductionPanelProps> {

    constructor(props) {
        super(props);

        StoreAndSaveService.assignModel(props.model);
    }

    render() {
        return (
            <>
                <BackgroundArea />

                <Panel />
            </>
        )
    }
}