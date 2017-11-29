import * as React from "react";
import ProjectPickerComponent from "./project-picker";
import { ITrackingProps } from "./tracking-props";
import { Tabs, Tab, TabList, TabPanel } from "react-tabs";
import SingleTimePickerComponent from "./single-time-picker";
import MultipleTimesEditorComponent from "./multiple-times-editor";
import 'react-tabs/style/react-tabs.css';
import request from 'axios';
import {SelectListItem} from "../../models/select-list-item";
import AlertComponent, {IAlertComponentProps, MessageType} from "../universal/alert-component";
import {ProjectSelectListItem} from "../../models/project-select-list-item";
import {RegisteredAction} from "../../models/projects/registered-action";

export interface ITrackingComponentState {
    currentProject: ProjectSelectListItem,
    selectedAction: string, 
    enteredDuration: string,
    projectMemberActions: SelectListItem[],
    alertComponentProperties: IAlertComponentProps,
    registeredProjectMemberActions: RegisteredAction[]
}

const messages: any = {
    "MissingProperties": "Missing proerties",
    "UnknownError": "Unknown error has occured. Contact the developers.",
    "Success": "Your changes have been successfully submitted."
};

export default class TrackingComponent extends React.Component<ITrackingProps, ITrackingComponentState> {

    private config: any = {
        headers: {
            'Content-Type': 'application/json',
        }
    };
    
    constructor(props: ITrackingProps) {
        super(props);
        this.state = {
            currentProject: null,
            selectedAction: null,
            enteredDuration: null,
            projectMemberActions: null,
            alertComponentProperties: {
                display: false,
                message: "",
                messageType: MessageType.success
            },
            registeredProjectMemberActions: null    
        };
    }
    
    private selectedActionChanged(val: any) {
        const lastState : ITrackingComponentState = this.state;
        lastState.selectedAction = val;
        this.setState(lastState);
    }
    
    private enteredDurationChanged(val: any) {
        const lastState: ITrackingComponentState = this.state;
        lastState.enteredDuration = val;
        this.setState(lastState);
    }
    
    private actionRegistered() {
        let requestUrl: string = "/Tracking/RegisterTime?projectMemberActionId=" +
            this.state.selectedAction + "&duration=" + this.state.enteredDuration;
        console.log("requestUrl", requestUrl);
        const lastState: ITrackingComponentState = this.state;
        request.get(requestUrl).then((response: any) => {
           
            lastState.alertComponentProperties.display = true;
            if (response.data == "MissingParameters") {
                lastState.alertComponentProperties.messageType = MessageType.danger;
                lastState.alertComponentProperties.message = messages["MissingProperties"];
            } else {
                lastState.alertComponentProperties.messageType = MessageType.success;
                lastState.alertComponentProperties.message = messages["Success"];
                lastState.enteredDuration = "";
                lastState.selectedAction = "";
            }
            this.setState(lastState);
        });
        setTimeout(() => {
            const alertProps = this.state.alertComponentProperties;
            alertProps.display = false;
            lastState.alertComponentProperties = alertProps;
            this.setState(lastState);
        }, 5000);
    }
    
    private projectChanged(project: any) {
        let requestUrl : string = "/Projects/GetAvailableProjectUserActions?projectId=" + project.value;
        request.get(requestUrl, this.config).then((response: any) => {
            const lastState: ITrackingComponentState = this.state;
            lastState.alertComponentProperties.display = false;
            if (response.data == "MissingParameters") {
                lastState.alertComponentProperties.messageType = MessageType.danger;
                lastState.alertComponentProperties.message = messages["MissingProperties"];
            } else {
                console.log("response", response);
                lastState.alertComponentProperties.messageType = MessageType.success;
                lastState.alertComponentProperties.message = messages["Success"];
                let projectMemberActions : SelectListItem[]
                    = response.data.map((x: any) => {
                    return {
                        label: x.description,
                        value: x.id,
                    };
                });
                lastState.currentProject = project;
                lastState.projectMemberActions = projectMemberActions;
                requestUrl = 'Tracking/GetProjectMemberRegisteredTimes?projectMemberId=' + this.state.currentProject.projectMemberId;
                request.get(requestUrl).then((result : any) => {
                    if (result.data != "MissingParameters") {
                        lastState.registeredProjectMemberActions = result.data.result;
                        this.setState(lastState);
                    }
                });
            }
        });
        
    }
    
    render() {
        return (
            <div>
                <ProjectPickerComponent 
                    valueChanged={this.projectChanged.bind(this)}
                    projects={this.props.projects}
                    selectedValue={this.state.currentProject != null ? this.state.currentProject.value : ""}
                />
                {this.state.projectMemberActions == null ? 
                    <label>Choose the project first</label> : 
                <Tabs>
                    <TabList>
                        <Tab>
                            Momentinis laiko priskyrimas
                        </Tab>
                        <Tab>
                            Priskirtų laikų redagavimas
                        </Tab>    
                    </TabList>
                    <TabPanel>
                        <AlertComponent {...this.state.alertComponentProperties}/>
                        <SingleTimePickerComponent 
                            selectedProjectMemberActionChanged={this.selectedActionChanged.bind(this)}
                            enteredDurationChanged={this.enteredDurationChanged.bind(this)}
                            actionRegistered={this.actionRegistered.bind(this)}
                            projectMemberActions={this.state.projectMemberActions}
                            selectedAction={this.state.selectedAction}
                            enteredDuration={this.state.enteredDuration}
                        />
                    </TabPanel>
                    <TabPanel>
                        <MultipleTimesEditorComponent
                            registeredActions={this.state.registeredProjectMemberActions}
                            possibleUserActions={this.state.projectMemberActions}
                        />
                    </TabPanel>    
                </Tabs> }
            </div>
        )
    }
}