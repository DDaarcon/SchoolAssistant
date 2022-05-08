import React from "react";

export type ListEntryInnerProps = {
    isEven: boolean;
    openedModification: boolean;
    columnsCount: number;
    dataRow: JSX.Element;
    modificationComponent: JSX.Element;
}
export default class ListEntryInnerComponent extends React.Component<ListEntryInnerProps> {
    render() {

        const modificationRow = this.props.openedModification
            ? (
                <td className="dm-modification-component-container"
                    colSpan={this.props.columnsCount}
                >
                    {this.props.modificationComponent}
                </td>
            ) : <></>;

        return (
            <>
                <tr className={
                    (this.props.isEven ? "even-row" : "") +
                    " data-row" +
                    (this.props.openedModification ? "" : " single-standing-row")
                }>
                    {this.props.dataRow}
                </tr>
                <tr className={this.props.isEven ? "even-row" : ""}>
                    {modificationRow}
                </tr>
                <tr className="separation-row">
                    <td colSpan={this.props.columnsCount}> </td>
                </tr>
            </>
        );
    }
}