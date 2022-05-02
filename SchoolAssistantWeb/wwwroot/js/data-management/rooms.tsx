interface RoomListEntry extends TableData {
    name: string;
    floor: number;
}

interface RoomDetails {
    id?: number;
    name: string;
    floor: number;
    number?: number;
}

interface RoomModificationData {
    data: RoomDetails;
    defaultName: string;
}





type RoomsPageProps = {

};
type RoomsPageState = {

}

class RoomsPage extends React.Component<RoomsPageProps, RoomsPageState> {

    render() {
        return (
            <div className="dm-rooms-page">
                <RoomsTable />
            </div>
        )
    }
}






type RoomsTableProps = {

}

const RoomsTable = (props: RoomsTableProps) => {
    const columnsSetting: ColumnSetting<RoomListEntry>[] = [
        {
            header: "Nazwa",
            prop: "name",
            style: { width: '50%' }
        },
        {
            header: "Piętro",
            prop: "floor",
            style: { width: '10%' }
        }
    ];

    const loadAsync = async (): Promise<RoomListEntry[]> => {
        let response = await server.getAsync<RoomListEntry[]>("RoomEntries");
        return response;
    }

    return (
        <Table
            columnsSetting={columnsSetting}
            modificationComponent={RoomModificationComponent}
            loadDataAsync={loadAsync}
        />
    );
}






type RoomModificationComponentProps = ModificationComponentProps;
type RoomModificationComponentState = {
    awaitingData: boolean;
    data: RoomDetails;
    defaultName?: string;
}

class RoomModificationComponent extends React.Component<RoomModificationComponentProps, RoomModificationComponentState> {
    constructor(props) {
        super(props);

        this.state = {
            awaitingData: true,
            data: {
                name: '',
                floor: 0
            }
        }

        if (this.props.recordId)
            this.fetchAsync();
        else
            this.fetchDefaultNameAsync();
    }

    private async fetchAsync() {
        let res = await server.getAsync<RoomModificationData>("RoomDetails", {
            id: this.props.recordId
        });

        this.setState({ data: res.data, defaultName: res.defaultName, awaitingData: false });
    }

    private async fetchDefaultNameAsync() {
        const name = await server.getAsync<string>("RoomDefaultName");

        this.setState({ defaultName: name, awaitingData: false });
    }

    createOnChangeHandler: (property: keyof RoomDetails) => React.ChangeEventHandler<HTMLInputElement> = (property) => {
        return (event) => {
            const value = event.target.value;

            this.setState(prevState => {
                const data: RoomDetails = { ...prevState.data };
                data[property] = (value as unknown) as never;
                return { data };
            });

            this.props.onMadeAnyChange();
        }
    }

    onSubmitAsync: React.FormEventHandler<HTMLFormElement> = async (event) => {
        event.preventDefault();

        let response = await server.postAsync<ResponseJson>("RoomData", undefined, this.state.data);

        if (response.success)
            await this.props.reloadAsync();
        else
            console.debug(response);
    }

    render() {
        if (this.state.awaitingData)
            return (
                <Loader
                    enable={true}
                    size={LoaderSize.Medium}
                    type={LoaderType.DivWholeSpace}
                />
            )

        return (
            <div>
                <form onSubmit={this.onSubmitAsync}>
                    <div className="form-group">
                        <label htmlFor="name-input">Nazwa</label>
                        <input
                            type="text"
                            id="name-input"
                            placeholder={this.state.defaultName}
                            className="form-control"
                            value={this.state.data.name}
                            onChange={this.createOnChangeHandler('name')}
                        />
                    </div>
                    
                    <div className="form-group">
                        <label htmlFor="number-input">Numer</label>
                        <input
                            type="number"
                            id="number-input"
                            className="form-control"
                            value={this.state.data.number}
                            onChange={this.createOnChangeHandler('number')}
                        />
                    </div>
                    
                    <div className="form-group">
                        <label htmlFor="floor-input">Piętro</label>
                        <input
                            type="number"
                            id="floor-input"
                            className="form-control"
                            value={this.state.data.floor}
                            onChange={this.createOnChangeHandler('floor')}
                        />
                    </div>


                    <div className="form-group">
                        <input
                            type="submit"
                            value="Zapisz"
                            className="form-control"
                        />
                    </div>
                </form>
            </div>
        )
    }
}