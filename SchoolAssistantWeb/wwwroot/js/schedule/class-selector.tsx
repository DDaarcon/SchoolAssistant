type ScheduleClassSelectorPageProps = {
    entries: ScheduleClassSelectorEntry[];
}
type ScheduleClassSelectorPageState = {

}
class ScheduleClassSelectorPage extends React.Component<ScheduleClassSelectorPageProps, ScheduleClassSelectorPageState> {

    render() {
        return (
            <div>
                <h2>Wybierz klasę</h2>
                {this.props.entries.map(entry =>
                    <ClassEntry
                        {...entry }
                    />
                )}
            </div>
        )
    }
}

type ClassEntryProps = ScheduleClassSelectorEntry & {

}
const ClassEntry = (props: ClassEntryProps) => {
    const selectClass = () =>
        scheduleServer.getAsync<ScheduleClassLessons>("ClassLessons", { classId: props.id })
        .then((result) => {
            scheduleChangePageScreen(
                <ScheduleArrangerPage
                    classData={result}
                />
            );
        });


    return (
        <div className="sa-selector-class-entry"
            onClick={selectClass}
        >
            <span className="sa-selector-class-name">
                {props.name}
            </span>
            <span className="sa-selector-class-spec">
                {props.specialization}
            </span>
        </div>
    )
}