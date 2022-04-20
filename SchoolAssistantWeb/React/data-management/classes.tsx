import * as React from "react";

type Props = {};
type State = {
    a: number;
};

export default class ClassesPage extends React.Component<Props, State> {
    constructor(props) {
        super(props);
        
    }

    render() {
        return (
            <div>
                Classes
            </div>
        )
    }
}