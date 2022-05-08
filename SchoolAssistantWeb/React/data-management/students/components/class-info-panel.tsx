import React from "react";

type ClassInfoPanelProps = {
    name: string;
    specialization?: string;
}
const ClassInfoPanel = (props: ClassInfoPanelProps) => {

    return (
        <div className="dm-students-class-info-panel">
            <div className="dm-cip-name">
                {props.name}
            </div>
            <div className="dm-cip-spec">
                {props.specialization}
            </div>
        </div>
    )
}
export default ClassInfoPanel;