import React from "react";
import TopBar from "../../../shared/top-bar";
import UserTypeForManagement from "../../enums/user-type-for-management";
import FetchRelatedObjectsRequest from "../interfaces/fetch-related-objects-request";
import SimpleRelatedObject from "../interfaces/simple-related-object";
import serverCreationForm from "../server-creation-form";

type RelatedObjectsListProps<TRelated extends SimpleRelatedObject> = {
    objectToFields: (obj: TRelated) => string[];
    fieldClassNames: string[];
    type: UserTypeForManagement;
    selectObject: (obj?: TRelated) => void;
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
            <button key={`${this.props.type}-${obj.id}`}
                className="related-object-entry tiled-btn"
                onClick={() => this.props.selectObject(obj)}
            >
                {elements}
            </button>
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