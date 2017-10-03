import * as React from "react";
import * as ReactDOM from "react-dom";
import { ProjectCreateModel } from "../../models/projects/project-create-model";
import Select from "react-select";
import "react-select/dist/react-select.css";

export interface IProjectMembersComponentProps {
    projectCreateModel: ProjectCreateModel
}

export default class ProjectMembersComponent extends React.Component<IProjectMembersComponentProps, any> {

    constructor(props: IProjectMembersComponentProps) {
        super(props);
        console.log("received props: ", props);
    }

    render() {
        return (
            <div></div>
            );
    }
}