import React from "react";
import UserTypeForManagement from "../../enums/user-type-for-management";
import FetchRelatedObjectsRequest from "../interfaces/fetch-related-objects-request";
import SimpleRelatedObject from "../interfaces/simple-related-object";
import serverCreationForm from "../server-creation-form";

type RelatedObjectsListProps<TRelated extends SimpleRelatedObject> = {
    objectToFields: (obj: TRelated) => string[];
    fieldClassNames: string[];
    type: UserTypeForManagement;
    selectObject: (obj: TRelated) => void;
}
type RelatedObjectsListState<TRelated extends SimpleRelatedObject> = {
    objects?: TRelated[];
}

export default class RelatedObjectsList
    <TRelated extends SimpleRelatedObject> extends React.Component<RelatedObjectsListProps<TRelated>, RelatedObjectsListState<TRelated>> {

    constructor(props) {
        super(props);

        this.fetchObjectsAsync();
    }

    render() {
        return (
            <>
                {this.state?.objects?.map(this.objectDisplayComponent)}
            </>
        )
    }

    private objectDisplayComponent: (obj: TRelated) => JSX.Element = (obj) => {
        const fields = this.props.objectToFields(obj);

        const elements: JSX.Element[] = [];
        for (let i = 0; i < Math.min(fields.length, this.props.fieldClassNames.length); i++)
            elements.push(
                <div key={i}
                    className={`related-object-entry-field ${this.props.fieldClassNames[i]}`}
                >
                    {fields[i]}
                </div>
            )

        return (
            <div key={`${this.props.type}-${obj.id}`}
                className="related-object-entry"
                onClick={() => this.props.selectObject(obj)}
            >
                {elements}
            </div>
        )
    }

    private async fetchObjectsAsync() {
        const params: FetchRelatedObjectsRequest = {
            ofType: this.props.type
        };
        const res = await serverCreationForm.getAsync<TRelated[]>('RelatedObjects', params);

        this.setState({ objects: res });
    }
}