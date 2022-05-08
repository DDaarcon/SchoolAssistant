import React from "react";
import RoomsList from "./room-list";

type RoomsPageProps = {}
type RoomsPageState = {}
export default class RoomsPage extends React.Component<RoomsPageProps, RoomsPageState> {

    render() {
        return (
            <div className="dm-rooms-page">
                <RoomsList />
            </div>
        )
    }
}