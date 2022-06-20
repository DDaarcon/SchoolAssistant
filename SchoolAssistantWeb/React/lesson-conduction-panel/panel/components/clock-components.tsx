import React from "react";

export const ClockDigit = ({ digit }: { digit: number }) => {

    return (
        <div className="lcp-clock-digit">
            {digit}
        </div>
    )
}

export const ClockColon = ({ }) => {
    return (
        <div className="lcp-clock-colon">
            :
        </div>
    )
}