import React from "react";
import UserTypeForManagement from "../../enums/user-type-for-management";
import SimpleRelatedObject from "../interfaces/simple-related-object"
import StudentRelatedObject from "../interfaces/student-related-object"
import RelatedObjectsList from "./related-objects-list"
import './related-object-entry-fields.css';

type ObjectsListProps = {
    selectObject: (obj: SimpleRelatedObject) => void;
}

export const StudentObjectsList = (props: ObjectsListProps) => (
    <RelatedObjectsList<StudentRelatedObject>
        objectToFields={obj => ([obj.lastName, obj.firstName, obj.email, obj.orgClass])}
        fieldClassNames={['rel-obj-ent-last-name', 'rel-obj-ent-first-name', 'rel-obj-ent-email', 'stu-obj-ent-org-class']}
        selectObject={props.selectObject}
        type={UserTypeForManagement.Student}
    />
);

export const TeacherObjectsList = (props: ObjectsListProps) => (
    <RelatedObjectsList<SimpleRelatedObject>
        objectToFields={obj => ([obj.lastName, obj.firstName, obj.email ])}
        fieldClassNames={['rel-obj-ent-last-name', 'rel-obj-ent-first-name', 'rel-obj-ent-email']}
        selectObject={props.selectObject}
        type={UserTypeForManagement.Teacher}
    />
);

export const ParentObjectsList = (props: ObjectsListProps) => (
    <RelatedObjectsList<SimpleRelatedObject>
        objectToFields={obj => ([obj.lastName, obj.firstName, obj.email ])}
        fieldClassNames={['rel-obj-ent-last-name', 'rel-obj-ent-first-name', 'rel-obj-ent-email']}
        selectObject={props.selectObject}
        type={UserTypeForManagement.Parent}
    />
);