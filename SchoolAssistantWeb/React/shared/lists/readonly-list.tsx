import React from "react";
import SharedListComponent, { SharedListProps, SharedListState } from "./components/shared-list-component";
import ListEntry from "./interfaces/list-entry";

type ReadonlyListProps<TData extends ListEntry> = SharedListProps<TData, void, TData> & {
    initialData?: TData[];
};
type ReadonlyListState<TData extends ListEntry> = SharedListState<TData, TData> & {
}

export default class ReadonlyList<TData extends ListEntry> extends SharedListComponent<TData, void, TData, ReadonlyListProps<TData>, ReadonlyListState<TData>> {

    protected closeAllModCompState: <TKey extends keyof SharedListState<TData, TData>>() => Pick<ReadonlyListState<TData>, TKey> =
        //@ts-ignore
        () => ({ });


    constructor(props) {
        super(props);

        this.state = {
            data: this.props.initialData,
            loading: false
        };
    }


    render() {
        const displayProperties = this.props.columnsSetting.map(x => x.prop);

        return (
            <this.ListFundation
                tbody={
                    <>
                        {this.state.data?.map((data, i) => {
                            const recordId = data.id;
                            return (
                                <this.ListEntryComponent key={i}
                                    recordId={recordId}
                                    recordData={data}
                                    listEntryInnerComponent={this.ListEntryInnerComponent}
                                    customButtons={this.props.customButtons}
                                    modifying={false}
                                    displayProperties={displayProperties}
                                    reloadAsync={this.loadAsync}
                                    isEven={i % 2 == 1}
                                />
                            )
                        })}
                    </>
                }
            />
        )
    }
}