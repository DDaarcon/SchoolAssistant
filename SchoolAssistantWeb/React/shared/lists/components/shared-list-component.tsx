import React from "react";
import Loader, { LoaderSize, LoaderType } from "../../../shared/loader";
import ColumnSetting from "../interfaces/column-setting";
import CustomRowButton from "../interfaces/custom-row-button";
import GroupListEntry from "../interfaces/group-list-entry";
import ListEntry from "../interfaces/list-entry";
import { SharedGroupModCompProps } from "../interfaces/shared-group-mod-comp-props";
import ModCompProps from "../interfaces/shared-mod-comp-props";
import ListEntryComponent, { ListEntryProps } from "./list-entry-coponent";
import ListEntryInnerComponent, { ListEntryInnerProps } from "./list-entry-inner-component";
import '../lists.css';

export type SharedListProps<
    TEntry extends ListEntry,
    TModificationComponentProps extends ModCompProps | SharedGroupModCompProps | void,
    TStoredData extends TEntry | GroupListEntry<TEntry>
    > = {
        modificationComponent?: new (props: TModificationComponentProps) => React.Component<TModificationComponentProps>;
        customListEntryComponent?: new (props: ListEntryProps<TEntry, TModificationComponentProps>) => React.Component<ListEntryProps<TEntry, TModificationComponentProps>>;
        customListEntryInnerComponent?: new (props: ListEntryInnerProps) => React.Component<ListEntryInnerProps>;
        columnsSetting: ColumnSetting<TEntry>[];
        loadDataAsync: () => Promise<TStoredData[]>;
        customButtons?: CustomRowButton<TEntry>[]
    }
export type SharedListState<
    TEntry extends ListEntry,
    TStoredData extends TEntry | GroupListEntry<TEntry>
    > = {
        data?: TStoredData[];
        loading: boolean;
    }

export default abstract class SharedListComponent<
    TEntry extends ListEntry,
    TModificationComponentProps extends ModCompProps | SharedGroupModCompProps | void,
    TStoredData extends TEntry | GroupListEntry<TEntry>,
    TProps extends SharedListProps<TEntry, TModificationComponentProps, TStoredData>,
    TState extends SharedListState<TEntry, TStoredData>
    >
    extends React.Component<TProps, TState> {

    protected _madeAnyChange: boolean = false;

    protected get ListEntryComponent() { return this.props.customListEntryComponent ?? ListEntryComponent; }
    protected get ListEntryInnerComponent() { return this.props.customListEntryInnerComponent ?? ListEntryInnerComponent; }
    protected get ModificationComponent() { return this.props.modificationComponent; }

    protected get columnsCount() { return (this.props.columnsSetting?.length ?? 0) + (this.props.customButtons?.length ?? 0) + 1; }


    async componentDidMount() {
        if (!this.state.data)
            await this.loadAsync();
    }

    onMadeAnyChange = () => {
        this._madeAnyChange = true;
    }

    loadAsync = async () => {
        this.setState({ ...this.closeAllModCompState(), loading: true });
        const newData = await this.props.loadDataAsync();
        this.setState({
            data: newData,
            loading: false
        });
    }


    LoaderComponent =
        (<Loader
            enable={this.state?.loading}
            size={LoaderSize.Medium}
            type={LoaderType.Absolute}
        />);

    renderColumnSetting = (index: number, style?: React.CSSProperties) => {
        if (style)
            return (
                <col key={index} style={style} />
            )
        return <col key={index} />
    }


    ListFundation = (props: {
        tbody: JSX.Element;
    }) => {
        const buttons = this.props.customButtons?.map(x => x.columnStyle) ?? [];
        buttons.push({
            width: '0.1%'
        });

        return (
            <>
                {this.LoaderComponent}
                <table className="dm-list">
                    <colgroup>
                        {this.props.columnsSetting.map((setting, index) => this.renderColumnSetting(index, setting.style))}
                        {buttons.map((style, index) => this.renderColumnSetting(index, style))}
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
            </>
        )
    }

    public abstract render(): JSX.Element;

    protected abstract closeAllModCompState: <TKey extends keyof TState>() => Pick<TState, TKey>;
}