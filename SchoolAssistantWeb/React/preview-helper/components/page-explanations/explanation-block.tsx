import React from "react";
import './explanation-block.css';

const ExplanationBlock = ({ children }: {
    children: React.ReactNode;
}) => (
    <p className="explanation-block">
        {children}
    </p>
)
export default ExplanationBlock;