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
        this.setState({
            project: newProject
        });
    }

    render() {

        const componentProps: IProjectComponentProps = {
            projectSaved: this.allowAddingMembers.bind(this),
            project: this.state.project,
            projectChanged: this.projectChanged.bind(this)
        };

        return (
            <Tabs>
                <TabList>
                    <Tab>
                        Create Project
                     </Tab>
                    <Tab disabled={this.state.project.id == null}>
                        Add members
                    </Tab>
                </TabList>
                <TabPanel>
                    <MainProjectInfoComponent {...componentProps} />
                </TabPanel>
                <TabPanel>
                    <ProjectMembersComponent {...componentProps} />
                </TabPanel>
            </Tabs>
        );
    }
}