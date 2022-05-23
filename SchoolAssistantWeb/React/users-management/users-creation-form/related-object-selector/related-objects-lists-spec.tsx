import React from "react";
import UserTypeForManagement from "../../enums/user-type-for-management";
import SimpleRelatedObject from "../interfaces/simple-related-object"
import StudentRelatedObject from "../interfaces/student-related-object"
import RelatedObjectsList from "./related-objects-list"

type ObjectsListProps = {
    selectObject: (obj: SimpleRelatedObject) => void;
}

export const StudentObjectsList = (props: ObjectsListProps) => (
    <RelatedObjectsList<StudentRelatedObject>
        objectToFields={obj => ([obj.lastName, obj.firstName, obj.email, obj.orgClass])}
        fieldClassNames={['', '', '', '']}
        selectObject={props.selectObject}
        type={UserTypeForManagement.Student}
    />
);

export const TeacherObjectsList = (props: ObjectsListProps) => (
    <RelatedObjectsList<SimpleRelatedObject>
        objectToFields={obj => ([obj.lastName, obj.firstName, obj.email ])}
        fieldClassNames={['', '', '']}
        selectObject={props.selectObject}
        type={UserTypeForManagement.Teacher}
    />
);

export const ParentObjectsList = (props: ObjectsListProps) => (
    <RelatedObjectsList<SimpleRelatedObject>
        objectToFields={obj => ([obj.lastName, obj.firstName, obj.email ])}
        fieldClassNames={['', '', '']}
        selectObject={props.selectObject}
        type={UserTypeForManagement.Parent}
    />
);