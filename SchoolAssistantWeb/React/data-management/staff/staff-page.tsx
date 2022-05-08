import React from "react"
import StaffList from "./staff-list"

type StaffPageProps = {}
type StaffPageState = {}
export default class StaffPage extends React.Component<StaffPageProps, StaffPageState> {

    render() {
        return (
            <StaffList />
        )
    }
}