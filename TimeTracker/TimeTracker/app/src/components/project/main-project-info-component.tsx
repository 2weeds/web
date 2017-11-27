import * as React from "react";
import * as ReactDOM from "react-dom";
import { Project } from "../../models/projects/project";
import { IProjectComponentProps } from "./project-component-props";
import AlertComponent from "../universal/alert-component";

export default class MainProjectInfoComponent extends React.Component<IProjectComponentProps, any> {

    private projectNameChanged(newProjectName: any) {
        const newProject: Project = this.props.project;
        newProject.title = newProjectName.target.value;
        this.props.projectChanged(newProject);
    }

    private saveProject() {
        this.props.projectSaved(this.props.project);
    }

    render() {
        return (
            <div className="container-fluid">
                <div className="form-group">
                    <label>Project name:</label>
                    <input
                        name="ProjectName"
                        type="text"
                        className="form-control"
                        id="ProjectName"
                        value={this.props.project.title}
                        onChange={this.projectNameChanged.bind(this)} />
                    <br />
                    <button
                        type="submit"
                        className="btn btn-default"
                        onClick={this.saveProject.bind(this)}>Save</button>
                </div>
            </div>
        );
    }
}