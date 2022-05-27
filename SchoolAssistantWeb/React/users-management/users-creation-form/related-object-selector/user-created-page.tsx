import React from "react";
import CreatedUserInfo from "../interfaces/created-user-info";
import serverCreationForm from "../server-creation-form";

type UserCreatedPageProps = {
    user: CreatedUserInfo;
    returnToSelector: () => void;
}
type UserCreatedPageState = {
    readablePassword?: string;
}

export default class UserCreatedPage extends React.Component<UserCreatedPageProps, UserCreatedPageState> {

    constructor(props) {
        super(props);

        this.state = {};
    }

    render() {
        return (
            <div className="user-created-page">
                <span>
                    {`${this.props.user.lastName} ${this.props.user.firstName}`}
                </span>
                <span>
                    Nazwa użytkownika: {this.props.user.userName}
                </span>
                <span>
                    Email: {this.props.user.email}
                </span>
                <span>
                    {this.passwordInfoComponent()}
                </span>
            </div>
        )
    }

    private passwordInfoComponent = () => {
        if (this.state.readablePassword == undefined)
            return (
                <div>
                    <button
                        onClick={this.unscramblePasswordAsync}
                    >
                        Pokaż
                    </button>
                    <span>
                        {this.printStars()}
                    </span>
                </div>
            )

        return (
            <span>
                {this.state.readablePassword}
            </span>
        )
    }

    private unscramblePasswordAsync = async () => {
        var res = await serverCreationForm.getAsync<{
            readablePassword: string
        }>("UnscramblePassword", {
            deformed: this.props.user.passwordDeformed
        });

        this.setState({ readablePassword: res.readablePassword });
    }

    private printStars() {
        let count = this.props.user.passwordDeformed.length;
        let stars = "";
        while (0 < count--)
            stars += "*";
        return stars;
    }
}