import * as React from "react";
import * as ReactDOM from "react-dom";

export enum MessageType {
    success = 1,
    danger = 2
} 

export interface IAlertComponentProps {
    display: boolean
    messageType: MessageType,
    message: string
}

const messages : any= {
    success: "Success!",
    danger: "Error!",
};

export default class AlertComponent extends React.Component<IAlertComponentProps, any> {

    private static messages = {
        success: "Success!",
        danger: "Error!",
    };

    render() {

        const messageType = MessageType[this.props.messageType];

        const elementToRender = this.props.display ?
            <div className={"alert alert-" + messageType}>
                <strong>{messages[messageType]}</strong> {this.props.message}
            </div> :
            <div></div>;

        return elementToRender;
    }
} 