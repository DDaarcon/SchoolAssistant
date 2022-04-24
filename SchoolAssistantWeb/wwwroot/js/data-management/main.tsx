type MainScreenProps = {

}
type MainScreenState = {
    active?: Category;
    pageComponent?: new (props: any) => React.Component<any>;
    props?: any;
}

type RedirectMethod = (type: Category, pageComponent: new (props: any) => React.Component, props?: any) => void;

const server = new ServerConnection("/DataManagement/DataManagement");

class DataManagementMainScreen extends React.Component<MainScreenProps, MainScreenState> {
    state: MainScreenState = {
        active: undefined,
        pageComponent: undefined
    }

    redirect: RedirectMethod = (type: Category, pageComponent: new (props: any) => React.Component, props?: any) => {
        this.setState({
            active: type,
            pageComponent: pageComponent,
            props: props
        });
    }

    renderPageContent() {
        if (this.state?.pageComponent) {
            const props = this.state.props;
            const PageComponent = this.state.pageComponent;
            return (
                <PageComponent
                    onRedirect={this.redirect}
                    {...props}
                />
            )
        }
        return <WelcomeScreen />
    }

    render() {
        return (
            <div className="data-management-main">
                <DMNavigationBar onSelect={this.redirect} active={this.state.active} />

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

