

type SharedTableProps<
    TData extends TableData,
    TModificationComponentProps extends ModificationComponentProps | GroupedModificationComponentProps,
    TStoredData extends TData | GroupedTableData<TData>
    > = {
        modificationComponent: new (props: TModificationComponentProps) => React.Component<TModificationComponentProps>;
        customTableRecordComponent?: new (props: TableRecordProps<TData, TModificationComponentProps>) => React.Component<TableRecordProps<TData, TModificationComponentProps>>;
        customRecordRowsComponent?: new (props: RecordRowsProps) => React.Component<RecordRowsProps>;
        columnsSetting: ColumnSetting<TData>[];
        loadDataAsync: () => Promise<TStoredData[]>;
    }
type SharedTableState<
    TData extends TableData,
    TStoredData extends TData | GroupedTableData<TData>
    > = {
        data?: TStoredData[];
        loading: boolean;
    }

abstract class SharedTable<
    TData extends TableData,
    TModificationComponentProps extends ModificationComponentProps | GroupedModificationComponentProps,
    TStoredData extends TData | GroupedTableData<TData>,
    TProps extends SharedTableProps<TData, TModificationComponentProps, TStoredData>,
    TState extends SharedTableState<TData, TStoredData>
    >
    extends React.Component<TProps, TState> {
    protected readonly _defaultTableRowComponent = TableRecord;
    protected readonly _defaultRecordRowsComponent = RecordRows;

    protected _madeAnyChange: boolean = false;

    protected get TableRecordToUse() { return this.props.customTableRecordComponent ?? this._defaultTableRowComponent; }
    protected get RecordRowsTouse() { return this.props.customRecordRowsComponent ?? this._defaultRecordRowsComponent; }
    protected get ModificationComponentToUse() { return this.props.modificationComponent; }



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

    LoaderComponent = () =>
        <Loader
            enable={this.state?.loading}
            size={LoaderSize.Medium}
            type={LoaderType.Absolute}
        />;


    TableFundation = (props: {
        tbody: JSX.Element;
    }) =>
        <>
            <this.LoaderComponent />
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