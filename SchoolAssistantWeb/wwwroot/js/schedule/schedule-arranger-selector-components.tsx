type ScheduleLessonPrefabTileProps = {
    data: ScheduleLessonPrefab;
}
type ScheduleLessonPrefabTileState = {

}
class ScheduleLessonPrefabTile extends React.Component<ScheduleLessonPrefabTileProps, ScheduleLessonPrefabTileState> {

    onDrag: React.DragEventHandler<HTMLDivElement> = (event) => {
        event.dataTransfer.setData("subject", this.props.data.subject.id.toString());
        event.dataTransfer.setData("lecturer", this.props.data.subject.id.toString());
        event.dataTransfer.setData("subject", this.props.data.subject.id.toString());
    }

    onEnd: React.DragEventHandler<HTMLDivElement> = (event) => {
        dispatchEvent(new Event("hideLessonShadow"));
    }

    render() {
        return (
            <div className="sa-lesson-prefab"
                draggable
                onDrag={this.onDrag}
                onDragEnd={this.onEnd}
            >
                <h4 className="sa-lesson-prefab-subject">
                    {this.props.data.subject.name}
                </h4>
                <div className="sa-lesson-prefab-bottom">
                    <div className="sa-lesson-prefab-lecturer">
                        {this.props.data.lecturer.name}
                    </div>
                    <div className="sa-lesson-prefab-room">
                        {this.props.data.room?.name}
                    </div>
                </div>
            </div>
        )
    }
}