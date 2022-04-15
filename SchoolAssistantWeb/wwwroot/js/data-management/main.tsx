type MainScreenProps = {

}
type MainScreenState = {
    active?: Category;
    pageComponent?: new (props: {}) => React.Component<{}>
}

const server = new ServerConnection("/DataManagement/DataManagement");

class DataManagementMainScreen extends React.Component<MainScreenProps, MainScreenState> {
    state: MainScreenState = {
        active: undefined,
        pageComponent: undefined
    }

    onBlockClick = (type: Category, pageComponent: new (props: {}) => React.Component) => {
        this.setState({
            active: type,
            pageComponent: pageComponent
        });
    }

    renderPageContent() {
        if (this.state?.pageComponent) {
            const PageComponent = this.state.pageComponent;
            return <PageComponent />
        }
        return <WelcomeScreen />
    }

    render() {
        return (
            <div className="data-management-main">
                <DMNavigationBar onSelect={this.onBlockClick} active={this.state.active} />

                <div className="dm-page-content">
                    {this.renderPageContent()}
                </div>
            </div>
        )
    }
}

const WelcomeScreen = () => {
    return (
        <h4>Zarządzanie danymi</h4>
    )
}

