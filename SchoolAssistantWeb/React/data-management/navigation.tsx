import React from "react";
import ClassesPage from "./classes/classes-page";
import Category from "./enums/category";
import RoomsPage from "./rooms/rooms-page";
import StaffPage from "./staff/staff-page";
import SubjectsPage from "./subjects/subjects-page";

type BarProps = {
    active?: Category;
    onSelect: (category: Category, pageComponent: new (props: {}) => React.Component<{}, {}>) => void;
}
type BarState = {

}

export default class DMNavigationBar extends React.Component<BarProps, BarState> {
    items;

    generateNavItems() {
        this.items = [
            this.createNavItem("Przedmioty", Category.Subjects, SubjectsPage),
            this.createNavItem("Personel", Category.Staff, StaffPage),
            this.createNavItem("Pomieszczenia", Category.Rooms, RoomsPage),
            this.createNavItem("Klasy", Category.Classes, ClassesPage),
        ]
    }

    createNavItem(label: string, category: Category, pageComponent: new (props: {}) => React.Component<{}>) {
        return {
            label: label,
            onClick: () => { this.props.onSelect(category, pageComponent); },
            active: this.props.active == category
        };
    }

    render() {
        this.generateNavItems();

        return (
            <div className="dm-navigation-bar">
                {this.items.map(item =>
                    <DMNavigationItem
                        key={item.label}
                        label={item.label}
                        onClick={item.onClick}
                        isActive={item.active}
                    />
                )}
            </div>
        )
    }
}

type BarItemProps = {
    label: string;
    isActive: boolean;
    onClick: () => void;
}
type BarItemState = {

}

class DMNavigationItem extends React.Component<BarItemProps, BarItemState> {
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