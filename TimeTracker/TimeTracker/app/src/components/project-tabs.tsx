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
import ProjectMemberActionsComponent from "./project/project-member-actions-component";
import { ProjectMemberAction } from "../models/projects/project-member-action";
import { ProjectMember } from "../models/projects/project-member";
import { IProjectMemberActionsComponentProps } from "./project/project-member-actions-component";

export interface IProjectTabsComponentProps {
    project: Project,
}

export interface IProjectTabsComponentState {
    project: Project
}

export default class ProjectTabsComponent extends React.Component<IProjectTabsComponentProps, IProjectTabsComponentState> {

    private static projectCreateUrl: string = '/Projects/Create';
    private static projectUpdateUrl: string = '/Projects/Edit';

    private getProjectUrl(project: Project) {
        return project.id != null ? ProjectTabsComponent.projectUpdateUrl : ProjectTabsComponent.projectCreateUrl;
    }

    constructor(props: IProjectTabsComponentProps) {
        super(props);
        this.state = {
            project: props.project
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
                    const responseProject: Project = response.data;
                    console.log("siuntem: ", project);
                    console.log("gavom", responseProject);
                    this.setState({
                        project: responseProject
                    });

                })
                .catch((response: any) => {
                    console.log("error response: ", response);
                });
        }
    }

    private projectChanged(newProject: Project) {
        console.log("newProject", newProject);
        this.setState({
            project: newProject
        });
    }

    render() {

        const currentProject = this.state.project;

        let currentProjectMemberIndex: number = -1;

        if (currentProject.projectMembers != null) {
            currentProjectMemberIndex = currentProject.projectMembers.map((e, i) => {
                if (e.isCurrentUser) {
                    return i;   
                }
            }).filter((e) => e != null)[0];
        }
        console.log("currentProjectMemberIndex", currentProjectMemberIndex);
        if (currentProjectMemberIndex != -1) {
            console.log("currentProject.projectMembers[currentProjectMemberIndex].projectMemberActions", currentProject.projectMembers[currentProjectMemberIndex].projectMemberActions);
        }
       
        if (currentProjectMemberIndex != null && currentProjectMemberIndex != -1 &&
            currentProject.projectMembers[currentProjectMemberIndex].projectMemberActions.length == 0) {
            let fakeMemberAction: ProjectMemberAction = {
                description: "",
                projectMemberId: currentProject.projectMembers[currentProjectMemberIndex].id,
                id: ""
            };

            currentProject.projectMembers[currentProjectMemberIndex].projectMemberActions = [];
            currentProject.projectMembers[currentProjectMemberIndex].projectMemberActions.push(fakeMemberAction);
        }

        const componentProps: IProjectComponentProps = {
            projectSaved: this.allowAddingMembers.bind(this),
            project: currentProject,
            projectChanged: this.projectChanged.bind(this)
        };

        const projectMemberComponentProps: IProjectMemberActionsComponentProps = {
            ...componentProps,
            currentUserIndex: currentProjectMemberIndex
        };

        return (
            <Tabs>
                <TabList>
                    <Tab>
                        Title
                     </Tab>
                    <Tab disabled={this.state.project.id == null}>
                        Members
                    </Tab>
                    <Tab disabled={this.state.project.projectMemberIds == null}>
                        Actions
                    </Tab>
                </TabList>
                <TabPanel>
                    <MainProjectInfoComponent {...componentProps} />
                </TabPanel>
                <TabPanel>
                    <ProjectMembersComponent {...componentProps} />
                </TabPanel>
                <TabPanel>
                    <ProjectMemberActionsComponent {...projectMemberComponentProps} />
                </TabPanel>
            </Tabs>
        );
    }
}