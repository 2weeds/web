// A '.tsx' file enables JSX support in the TypeScript compiler, 
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
import * as React from "react";
import * as ReactDOM from "react-dom";
import { Tabs, Tab, TabList, TabPanel } from "react-tabs";
import 'react-tabs/style/react-tabs.css';
import MainProjectInfoComponent from "./project/main-project-info-component";
import ProjectMembersComponent from "./project/project-members-component";
import { IProjectMembersComponentProps } from "./project/project-members-component";

export interface IProjectTabsComponentProps extends IProjectMembersComponentProps {

}

export default class ProjectTabsComponent extends React.Component<IProjectTabsComponentProps, any> {
    render(){
        return (
            <Tabs>
                <TabList>
                    <Tab>
                        Create Project
                     </Tab>
                    <Tab>
                        Add members
                    </Tab>
                </TabList>
                <TabPanel>
                    <MainProjectInfoComponent />
                </TabPanel>
                <TabPanel>
                    <ProjectMembersComponent projectCreateModel={this.props.projectCreateModel} />
                </TabPanel>
            </Tabs>
        );
    }
}