type ButtonProps = {
    label: string;
    onClick: (() => void) | (() => Promise<void>);
    className?: string;
}
export default ButtonProps;