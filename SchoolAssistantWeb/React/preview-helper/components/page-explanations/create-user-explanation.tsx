import React from "react";
import ExplanationBlock from "./explanation-block";

const CreateUserExplanation = () => (
    <ExplanationBlock>
        Na tej stronie można utworzyć użytkownika połączonego z danym Uczniem/Nauczycielem.
        Po wybraniu z listy wprowadź odpowiednie dane i utwórz użytkownika.
        Po utworzeniu wygenerowane zostanie hasło (aplikacja obecnie nie korzysta z klienta poczty, dlatego maile nie są wysyłane).
    </ExplanationBlock>
)
export default CreateUserExplanation;