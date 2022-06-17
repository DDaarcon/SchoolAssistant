import { enumAssignSwitch, enumSwitch } from "../../../shared/enum-help";
import { LoadLessonsButtonLayout } from "./layered-button";

type ArrowAnimationEventDetails = {
    amount: number;
    on: boolean;
}


export default class ArrowAnimationEventHelper {


    static dispatch(layout: LoadLessonsButtonLayout, details: ArrowAnimationEventDetails) {
        enumSwitch(LoadLessonsButtonLayout, layout, {
            Upright: () => ArrowAnimationEventHelper.dispatchUpright(details),
            UpsideDown: () => ArrowAnimationEventHelper.dispatchUpsideDown(details)
        });
    }
    static dispatchUpright(details: ArrowAnimationEventDetails) {
        dispatchEvent(createArrowAnimationEvent(LoadLessonsButtonLayout.Upright, details));
    }
    static dispatchUpsideDown(details: ArrowAnimationEventDetails) {
        dispatchEvent(createArrowAnimationEvent(LoadLessonsButtonLayout.UpsideDown, details));
    }

    static addListener(layout: LoadLessonsButtonLayout, listener: (details: ArrowAnimationEventDetails) => void) {
        enumSwitch(LoadLessonsButtonLayout, layout, {
            Upright: () => ArrowAnimationEventHelper.addListenerUpright(listener),
            UpsideDown: () => ArrowAnimationEventHelper.addListenerUpsideDown(listener)
        });
    }
    static addListenerUpright(listener: (details: ArrowAnimationEventDetails) => void) {
        addEventListener(getEventName(LoadLessonsButtonLayout.Upright), (ev) => {
            listener((ev as CustomEvent).detail);
        })
    }
    static addListenerUpsideDown(listener: (details: ArrowAnimationEventDetails) => void) {
        addEventListener(getEventName(LoadLessonsButtonLayout.UpsideDown), (ev) => {
            listener((ev as CustomEvent).detail);
        })
    }
}


function createArrowAnimationEvent(layout: LoadLessonsButtonLayout, detail: ArrowAnimationEventDetails, init?: EventInit) {
    return new CustomEvent(getEventName(layout), {
        detail,
        ...init
    })
}

function getEventName(layout: LoadLessonsButtonLayout) {
    return enumAssignSwitch<string, typeof LoadLessonsButtonLayout>(LoadLessonsButtonLayout, layout, {
        Upright: () => "arrow-animation-upright",
        UpsideDown: () => "arrow-animation-upside-down",
        _: () => { throw new Error("Invalid 'LoadLessonsButtonLayout' enum value") }
    });
}