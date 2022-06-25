import React from 'react';
import StoreService from '../../services/store-service';
import './info-row.css';
import SaveButton from './save-button';

const InfoRow = (props: {}) => {

    return (
        <div className="lcp-info-row">
            <SaveButton />

            <div className="lcp-info-row-text-info">
                <span className="lcp-info-row-class-name">
                    {StoreService.className}
                </span>

                <span className="lcp-info-row-subject-name">
                    {StoreService.subjectName}
                </span>
            </div>
        </div>
    )
}
export default InfoRow;