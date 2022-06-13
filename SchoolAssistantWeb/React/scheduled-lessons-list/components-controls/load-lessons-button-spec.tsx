import React from "react"
import LoadLessonsButton from "./load-lessons-button"

type LoadOlderLessonsButtonProps = {
}

export const LoadOlderLessonsButton = (props: LoadOlderLessonsButtonProps) => (
    <LoadLessonsButton
        text="Wcześniejsze 5"
        onClick={amount => {
            dispatchEvent(new CustomEvent("loadOlderLessons", {
                detail: { amount }
            }));
        }}
    />
)


type LoadNewerLessonsButtonProps = {
}

export const LoadNewerLessonsButton = (props: LoadNewerLessonsButtonProps) => (
    <LoadLessonsButton
        text="Późniejsze 5"
        onClick={amount => {
            dispatchEvent(new CustomEvent("loadNewerLessons", {
                detail: { amount }
            }));
        }}
    />
)