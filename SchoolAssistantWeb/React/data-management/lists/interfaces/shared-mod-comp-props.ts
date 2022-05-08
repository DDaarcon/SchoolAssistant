type ModCompProps = {
    recordId?: number;
    reloadAsync: () => Promise<void>;
    onMadeAnyChange: () => void;
}
export default ModCompProps;