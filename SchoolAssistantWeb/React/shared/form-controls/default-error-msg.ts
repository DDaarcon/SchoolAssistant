const defaultErrorMessage = (error: string) => {
    switch (error) {
        case 'null': return "Należy uzupełnić to pole.";
        case 'empty': return "Należy wybrać wartości.";
        case 'invalidDate': return "Nieprawidłowa data.";
        default: return error;
    }
}
export default defaultErrorMessage;