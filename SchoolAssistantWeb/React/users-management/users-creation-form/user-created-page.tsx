import React from "react";
import { LabelValue } from "../../shared/form-controls";
import TopBar from "../../shared/top-bar";
import CreatedUserInfo from "./interfaces/created-user-info";
import serverCreationForm from "./server-creation-form";
import './user-created-page.css';


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
                <h2>
                    Utworzono użytkownika
                </h2>
                <div className="user-created-page-rows">
                    <div className="usr-crea-page-user-info-row">

                        <LabelValue
                            label="Nazwisko i imię"
                            value={
                                <span>
                                    {`${this.props.user.lastName} ${this.props.user.firstName}`}
                                </span>
                            }
                        />
                        <LabelValue
                            label="Nazwa użytkownika"
                            value={
                                <span>
                                    {this.props.user.userName}
                                </span>
                            }
                        />
                        <LabelValue
                            label="Email"
                            value={
                                <span>
                                    {this.props.user.email}
                                </span>
                            }
                        />
                        <LabelValue
                            label="Hasło"
                            value={
                                <span>
                                    {this.passwordInfoComponent()}
                                </span>
                            }
                        />

                    </div>
                    <div className="usr-crea-page-messages-row">
                        <p>
                            Uzytkownik został utworzony. Na adres podany adres email zostało wysłane hasło tymczasowe hasło. Po zalogowaniu się użytkownik powinnien je zmienić.
                        </p>


                    </div>
                </div>
            </div>
        )
    }

    componentDidMount() {
        TopBar.Ref.showGoBack();
        TopBar.Ref.setGoBackAction(this.props.returnToSelector);
    }

    private passwordInfoComponent = () => {
        if (this.state.readablePassword == undefined)
            return (
                <div>
                    (<a
                        href="#"
                        onClick={this.unscramblePasswordAsync}
                    >
                        Pokaż
                    </a>)
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