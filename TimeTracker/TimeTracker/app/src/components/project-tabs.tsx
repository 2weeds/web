// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
import * as React from "react";
import * as ReactDOM from "react-dom";
import { Tabs, Tab, TabList, TabPanel } from "react-tabs";
import 'react-tabs/style/react-tabs.css';
import MainProjectInfoComponent from "./project/main-project-info-component";
import ProjectMembersComponent from "./project/project-members-component";
import { Project } from "../models/projects/project";
import request from 'axios';
import { IProjectComponentProps } from "./project/project-component-props";
import ProjectActionsComponent from "./project/project-actions-component";
import { ProjectAction } from "../models/projects/project-action";
import { ProjectMember } from "../models/projects/project-member";
import { IProjectActionsComponentProps } from "./project/project-actions-component";
import { MessageType, IAlertComponentProps } from "./universal/alert-component";
import AlertComponent from "./universal/alert-component";

export interface IProjectTabsComponentProps {
    project: Project,
}

export interface IProjectTabsComponentState {
    project: Project,
    alertComponentProperties: IAlertComponentProps
}

const messages: any = {
    "ProjectTitleNotUnique": "Project title is not unique.",
    "UnknownError": "Unknown error has occured. Contact the developers.",
    "Success": "Your changes have been successfully submitted."
};

export default class ProjectTabsComponent extends React.Component<IProjectTabsComponentProps, IProjectTabsComponentState> {

    private static projectCreateUrl: string = '/Projects/Create';
    private static projectUpdateUrl: string = '/Projects/Edit';

    private getProjectUrl(project: Project) {
        return project.id != null ? ProjectTabsComponent.projectUpdateUrl : ProjectTabsComponent.projectCreateUrl;
    }

    constructor(props: IProjectTabsComponentProps) {
        super(props);
        const componentProject = props.project;
        if (componentProject.title == null) {
            componentProject.title = "";
        }
        this.state = {
            project: componentProject,
            alertComponentProperties: {
                display: false,
                message: "",
                messageType: MessageType.success
            }
        };
    }

    private static getAlertComponentProperties(message: string, messageType: MessageType): IAlertComponentProps {
        return {
            display: true,
            message: message,
            messageType: messageType
        };
    }

    private allowAddingMembers(newProject: Project) {
        const project = this.state.project;
        const config: any = {
            headers: {
                'Content-Type': 'application/json',
            }
        };
        if (project.title.length > 0) {
            request.post(this.getProjectUrl(project), JSON.stringify(project), config)
                .then((response: any) => {
                    let lastAlertProperties: IAlertComponentProps = this.state.alertComponentProperties;
                    let responseProject: Project = this.state.project;
                    if (response.data.message != null) {
                        const message = response.data.message == "ProjectTitleNotUnique" ? messages["ProjectTitleNotUnique"] : messages["UnknownError"];
                        lastAlertProperties = ProjectTabsComponent.getAlertComponentProperties(message, MessageType.danger);
                    
                    } else {
                        responseProject = response.data;
                        lastAlertProperties = ProjectTabsComponent.getAlertComponentProperties(messages["Success"], MessageType.success);
                    }
                    this.setState({
                        project: responseProject,
                        alertComponentProperties: lastAlertProperties
                    });
                })
                .catch((response: any) => {
                    this.setState({
                        project: this.state.project,
                        alertComponentProperties: ProjectTabsComponent.getAlertComponentProperties(messages["UnknownError"], MessageType.danger)
                    });
                });
            setTimeout(() => {
                const alertProps = this.state.alertComponentProperties;
                alertProps.display = false;
                this.setState({
                    alertComponentProperties: alertProps,
                    project: this.state.project
                });
            }, 3000);
        }
    }

    private projectChanged(newProject: Project) {
        this.setState({
            project: newProject
        });
    }

    private tabSelected() {
        const alertProps = this.state.alertComponentProperties;
        alertProps.display = false;
        this.setState({
            project: this.state.project,
            alertComponentProperties: alertProps
        });
    }

    render() {

        const currentProject = this.state.project;
        console.log("currentProject", currentProject);
        if (currentProject.projectActions == null) {
            currentProject.projectActions = [];
        }

        if (currentProject.projectActions.length == 0) {
            const emptyAction: ProjectAction = {
                description: '',
                id: '',
                projectId: this.state.project.id
            }
            currentProject.projectActions = [emptyAction];
        }

        let currentProjectMemberIndex: number = -1;

        if (currentProject.projectMembers != null) {
            currentProjectMemberIndex = currentProject.projectMembers.map((e, i) => {
                if (e.isCurrentUser) {
                    return i;   
                }
            }).filter((e) => e != null)[0];
        }
        const componentProps: IProjectComponentProps = {
            projectSaved: this.allowAddingMembers.bind(this),
            project: currentProject,
            projectChanged: this.projectChanged.bind(this)
        };

        const projectMemberComponentProps: IProjectActionsComponentProps = {
            ...componentProps,
            currentUserIndex: currentProjectMemberIndex
        };

        return (
            <Tabs onSelect={this.tabSelected.bind(this)}>
                <TabList>
                    <Tab>
                        Title
                     </Tab>
                    <Tab disabled={this.state.project.id == null}>
                        Members
                    </Tab>
                    <Tab disabled={this.state.project.id == null}>
                        Actions
                    </Tab>
                </TabList>
                <TabPanel>
                    <AlertComponent {...this.state.alertComponentProperties} />
                    <MainProjectInfoComponent {...componentProps} />
                </TabPanel>
                <TabPanel>
                    <AlertComponent {...this.state.alertComponentProperties} />
                    <ProjectMembersComponent {...componentProps} />
                </TabPanel>
                <TabPanel>
                    <AlertComponent {...this.state.alertComponentProperties} />
                    <ProjectActionsComponent {...projectMemberComponentProps} />
                </TabPanel>
            </Tabs>
        );
    }
}