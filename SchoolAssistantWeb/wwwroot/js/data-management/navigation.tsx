type BarProps = {
    active: Category;
    onClick: (category: Category) => void;
}
type BarState = {

}

class DMNavigationBar extends React.Component<BarProps, BarState> {
    items;

    generateNavItems() {
        this.items = [
            this.createNavItem("Przedmioty", Category.Subjects),
            this.createNavItem("Klasy", Category.Classes),
        ]
    }

    createNavItem(label: string, category: Category) {
        return {
            label: label,
            onClick: () => { this.props.onClick(category); },
            active: this.props.active == category
        };
    }

    render() {
        this.generateNavItems();

        return (
            <div className="dm-navigation-bar">
                {this.items.map(item =>
                    <DMNavigationItem key={item.label} label={item.label} onClick={item.onClick} isActive={item.active} />
                )}
            </div>
        )
    }
}

class DMNavigationItem extends React.Component {
    render() {
        return (
            <div className={`dm-navigation-item ${this.props.isActive ? "dm-navigation-item-active" : ""}`}
                onClick={() => this.props.onClick()}
            >
                {this.props.label}
            </div>
        )
    }
}