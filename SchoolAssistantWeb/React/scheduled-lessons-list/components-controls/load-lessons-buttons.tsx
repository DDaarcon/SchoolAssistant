import React from "react"
import { LoadLessonsButtonLayout } from "./load-lessons-buttons/button-layer";
import LoadLessonsButton from "./load-lessons-buttons/load-lessons-button"

type LoadOlderLessonsButtonProps = {
}

export const LoadOlderLessonsButton = (props: LoadOlderLessonsButtonProps) => (
    <LoadLessonsButton
        layout={LoadLessonsButtonLayout.Upright}
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
        layout={LoadLessonsButtonLayout.UpsideDown}
        onClick={amount => {
            dispatchEvent(new CustomEvent("loadNewerLessons", {
                detail: { amount }
            }));
        }}
    />
)