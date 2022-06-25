import React from 'react';
import StoreService from '../../services/store-service';
import './info-row.css';

const InfoRow = (props: {}) => {

    return (
        <div className="lcp-info-row">
            <span className="lcp-info-row-class-name">
                {StoreService.className}
            </span>
            <span className="lcp-info-row-subject-name">
                {StoreService.subjectName}
            </span>
        </div>
    )
}
export default InfoRow;