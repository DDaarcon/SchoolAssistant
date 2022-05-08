import React from "react";
import Loader, { LoaderSize, LoaderType } from "../../../shared/loader";
import ColumnSetting from "../interfaces/column-setting";
import GroupListEntry from "../interfaces/group-list-entry";
import ListEntry from "../interfaces/list-entry";
import { SharedGroupModCompProps } from "../interfaces/shared-group-mod-comp-props";
import ModCompProps from "../interfaces/shared-mod-comp-props";
import EntryInfoCmoponent, { EntryInfoProps } from "./entry-info-component";
import ListEntryComponent, { ListEntryProps } from "./list-entry-coponent";
import ListEntryInnerComponent, { ListEntryInnerProps } from "./list-entry-inner-component";

export type SharedListProps<
    TEntry extends ListEntry,
    TModificationComponentProps extends ModCompProps | SharedGroupModCompProps,
    TStoredData extends TEntry | GroupListEntry<TEntry>
    > = {
        modificationComponent: new (props: TModificationComponentProps) => React.Component<TModificationComponentProps>;
        customListEntryComponent?: new (props: ListEntryProps<TEntry, TModificationComponentProps>) => React.Component<ListEntryProps<TEntry, TModificationComponentProps>>;
        customListEntryInnerComponent?: new (props: ListEntryInnerProps) => React.Component<ListEntryInnerProps>;
        customEntryInfoComponent?: (props: EntryInfoProps<TEntry>) => JSX.Element;
        columnsSetting: ColumnSetting<TEntry>[];
        loadDataAsync: () => Promise<TStoredData[]>;
    }
export type SharedListState<
    TEntry extends ListEntry,
    TStoredData extends TEntry | GroupListEntry<TEntry>
    > = {
        data?: TStoredData[];
        loading: boolean;
    }

export default abstract class SharedTable<
    TData extends ListEntry,
    TModificationComponentProps extends ModCompProps | SharedGroupModCompProps,
    TStoredData extends TData | GroupListEntry<TData>,
    TProps extends SharedListProps<TData, TModificationComponentProps, TStoredData>,
    TState extends SharedListState<TData, TStoredData>
    >
    extends React.Component<TProps, TState> {

    protected _madeAnyChange: boolean = false;

    protected get ListEntryComponent() { return this.props.customListEntryComponent ?? ListEntryComponent; }
    protected get ListEntryInnerComponent() { return this.props.customListEntryInnerComponent ?? ListEntryInnerComponent; }
    protected get EntryInfoComponent() { return this.props.customEntryInfoComponent ?? EntryInfoCmoponent; }
    protected get ModificationComponent() { return this.props.modificationComponent; }



    async componentDidMount() {
        await this.loadAsync();
    }

    onMadeAnyChange = () => {
        this._madeAnyChange = true;
    }

    loadAsync = async () => {
        this.setState({ loading: true });
        const newData = await this.props.loadDataAsync();
        this.setState({
            data: newData,
            loading: false
        });
    }

    renderColumnSetting = (setting: ColumnSetting<TData>, index: number) => {
        if (setting.style)
            return (
                <col key={index} style={setting.style} />
            )
        return <col key={index} />
    }

    LoaderComponent =
        (<Loader
            enable={this.state?.loading}
            size={LoaderSize.Medium}
            type={LoaderType.Absolute}
        />);


    ListFundation = (props: {
        tbody: JSX.Element;
    }) =>
        <>
            {this.LoaderComponent}
            <table className="dm-table">
                <colgroup>
                    {this.props.columnsSetting.map(this.renderColumnSetting)}
                </colgroup>
                <thead>
                    <tr>
                        {this.props.columnsSetting.map((setting, i) => <th key={i}>{setting.header}</th>)}
                    </tr>
                </thead>
                <tbody>
                    {props.tbody}
                </tbody>
            </table>
        </>;

    public abstract render(): JSX.Element;
}