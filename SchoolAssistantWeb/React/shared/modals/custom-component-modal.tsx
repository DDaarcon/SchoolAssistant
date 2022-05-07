import * as React from "react";
import { CommonModalProps, ModalBody } from "./shared-modal-body";


export type CustomComponentModalProps
    <TComponentProps,
    TComponentAndModalProps extends TComponentProps & CommonModalProps> = CommonModalProps &
    {
        modificationComponent: new (props: TComponentAndModalProps) => React.Component<TComponentAndModalProps>;
        modificationComponentProps: TComponentProps;
        style?: React.CSSProperties;
    }
type CustomComponentModalState = {

}
export default class CustomComponentModal<TComponentProps>
    extends React.Component<CustomComponentModalProps<TComponentProps, TComponentProps & CommonModalProps>, CustomComponentModalState>
{

    render() {
        return (
            <ModalBody
                assignedAtPresenter={this.props.assignedAtPresenter}
                style={this.props.style}
            >
                <this.props.modificationComponent
                    {...this.props.modificationComponentProps}
                    assignedAtPresenter={this.props.assignedAtPresenter}
                />
            </ModalBody>
        )
    }
}