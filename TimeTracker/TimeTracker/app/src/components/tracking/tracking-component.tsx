import * as React from "react";
import ProjectPickerComponent from "./project-picker";
import { ITrackingProps } from "./tracking-props";
import { Tabs, Tab, TabList, TabPanel } from "react-tabs";
import SingleTimePickerComponent from "./single-time-picker";
import MultipleTimesEditorComponent from "./multiple-times-editor";
import 'react-tabs/style/react-tabs.css';
import request from 'axios';
import {SelectListItem} from "../../models/select-list-item";
import {Project} from "../../models/projects/project";
import {ProjectMemberAction} from "../../models/projects/project-member-action";

export interface ITrackingComponentState {
    currentProject: string,
    selectedAction: string, 
    enteredDuration: string,
    projectMemberActions: SelectListItem[]
}

export default class TrackingComponent extends React.Component<ITrackingProps, ITrackingComponentState> {
    
    constructor(props: ITrackingProps) {
        super(props);
        this.state = {
            currentProject: null,
            selectedAction: null,
            enteredDuration: null,
            projectMemberActions: null
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
        
    }
    
    private projectChanged(project: any) {
        const config: any = {
            headers: {
                'Content-Type': 'application/json',
            }
        };
        let requestUrl : string = "/Projects/GetAvailableProjectUserActions?projectId=" + project.value;
        request.get(requestUrl, config).then((response: any) => {
            console.log("response", response);
            let projectMemberActions : SelectListItem[] 
                = response.data.map((x: any) => {
                    return {
                        label: x.description,
                        value: x.id
                    };
            });
            const lastState: ITrackingComponentState = this.state;
            lastState.currentProject = project;
            lastState.projectMemberActions = projectMemberActions;
            this.setState(lastState);
            //console.log('projectMemberActions', projectMemberActions);
        });
        
    }
    
    render() {
        return (
            <div>
                <ProjectPickerComponent 
                    valueChanged={this.projectChanged.bind(this)}
                    projects={this.props.projects}
                    selectedValue={this.state.currentProject}
                />
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
                        <MultipleTimesEditorComponent />
                    </TabPanel>    
                </Tabs>
            </div>
        )
    }
}