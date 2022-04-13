type MainScreenProps = {

}
type MainScreenState = {
    active: Category;
}

const baseUrl = "/DataManagement/DataManagement";

class DataManagementMainScreen extends React.Component<MainScreenProps, MainScreenState> {
    state = {
        active: Category.Subjects
    }

    onBlockClick = (type: Category) => {
        this.setState({ active: type });
    }

    renderPageContent() {
        switch (this.state.active) {
            case Category.Subjects:
                return <SubjectsPage />;
            case Category.Classes:
                return <ClassesPage />
            default:
                return undefined;
        }
    }

    render() {
        return (
            <div className="data-management-main">
                <DMNavigationBar onClick={this.onBlockClick} active={this.state.active} />

                <div className="dm-page-content">
                    {this.renderPageContent()}
                </div>
            </div>
        )
    }
}

