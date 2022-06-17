import React from "react"
import { LoadLessonsButtonLayout } from "./load-lessons-buttons/layered-button";
import LoadLessonsButton from "./load-lessons-buttons/load-lessons-button"
import LoadLessonsButtonIcon from "./load-lessons-buttons/load-lessons-button-icon";

type LoadOlderLessonsButtonProps = {
}

export const LoadOlderLessonsButton = (props: LoadOlderLessonsButtonProps) => (
    <LoadLessonsButton
        layout={LoadLessonsButtonLayout.Upright}
        title="Wczytaj wcześniejsze lekcje"
        onClick={amount => {
            dispatchEvent(new CustomEvent("loadOlderLessons", {
                detail: { amount }
            }));
        }}
    >
        <LoadLessonsButtonIcon
            layout={LoadLessonsButtonLayout.Upright}
            maxAmount={50}
        />
    </LoadLessonsButton>
)


type LoadNewerLessonsButtonProps = {
}

export const LoadNewerLessonsButton = (props: LoadNewerLessonsButtonProps) => (
    <LoadLessonsButton
        layout={LoadLessonsButtonLayout.UpsideDown}
        title="Wczytaj późniejsze lekcje"
        onClick={amount => {
            dispatchEvent(new CustomEvent("loadNewerLessons", {
                detail: { amount }
            }));
        }}
    >
        <LoadLessonsButtonIcon
            layout={LoadLessonsButtonLayout.UpsideDown}
            maxAmount={50}
        />
    </LoadLessonsButton>
)