class Schedule extends React.Component {

    constructor() {
        this.weekDays = {
            ''
        }
    }

    render() {
        return (
            <div className="schedule-container"

            >
                <SchTitlePanel
                    height={50}
                    label="Your schedule"
                />


                
            </div>
        );
    }
}


class SchTitlePanel extends React.Component {
    render() {
        return (
            <div
                style={{
                    height: `${this.props.height}px`,
                    width: `100%`,
                    display: 'flex'
                }}
            >
                <span>
                    {this.props.label}
                </span>
            </div>
        );
    }
}