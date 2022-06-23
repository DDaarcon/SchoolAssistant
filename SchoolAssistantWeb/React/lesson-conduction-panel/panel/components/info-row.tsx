import React from 'react';
import StoreAndSaveService from '../../services/store-and-save-service';
import './info-row.css';

const InfoRow = (props: {}) => {

    return (
        <div className="lcp-info-row">
            <span className="lcp-info-row-class-name">
                {StoreAndSaveService.className}
            </span>
            <span className="lcp-info-row-subject-name">
                {StoreAndSaveService.subjectName}
            </span>
        </div>
    )
}
export default InfoRow;