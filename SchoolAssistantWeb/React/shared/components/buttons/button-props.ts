type ButtonProps = {
    label: string;
    onClick: (() => void) | (() => Promise<void>);
    className?: string;
    typeSubmit?: boolean;
}
export default ButtonProps;