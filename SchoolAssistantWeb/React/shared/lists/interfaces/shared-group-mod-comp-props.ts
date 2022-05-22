import ModCompProps from "./shared-mod-comp-props";

export type SharedGroupModCompProps = ModCompProps & {
    groupId: string | number;
}