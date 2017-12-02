import * as React from "react";
import * as ReactDOM from "react-dom";
import { IProjectComponentProps } from "./project-component-props";
import { ProjectAction } from "../../models/projects/project-action";
import {Project} from "../../models/projects/project";

export interface IProjectActionsComponentProps extends IProjectComponentProps {
    currentUserIndex: number
}

export default class ProjectActionsComponent extends React.Component<IProjectActionsComponentProps, any> {

    private inputChanged(index: number, val: any) {
        const currentProject = this.props.project;
        currentProject.projectActions[index].description = val.target.value;
        this.props.projectChanged(currentProject);
    }

    private deleteRow(index: number) {
        const currentProject = this.props.project;
        if (currentProject.projectActions.length > 1) {
            currentProject.projectActions.splice(index, 1);
            this.props.projectChanged(currentProject);
        }
    }

    private addRow() {
        const newProjectMemberAction: ProjectAction = {
            description: "",
            id: "",
            projectId: this.props.project.id
        };
        const currentProject = this.props.project;
        currentProject.projectActions.push(newProjectMemberAction);
        this.props.projectChanged(currentProject);
    }

    private saveChanges() {
        this.props.projectSaved(this.props.project);
    }

    render() {
        return (
            <div>
                <table className="table table-bordered table-hover table-responsive">
                    <thead>
                        <tr>
                            <td>
                                Action name
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                        {this.props.project.projectActions.map((item: ProjectAction, index: number) => {
                            return (
                                <tr key={index}>
                                    <td>
                                        <input
                                            className="form-control"
                                            onChange={this.inputChanged.bind(this, index)}
                                            value={item.description}/>
                                    </td>
                                    <td>
                                        <button
                                            className="btn btn-danger"
                                            onClick={this.deleteRow.bind(this, index)}>Delete
                                        </button>
                                    </td>
                                </tr>
                            );
                        })}
                    </tbody>
                </table>
                <button className="btn btn-primary" onClick={this.addRow.bind(this)}>Add row</button>
                <button className="btn btn-default" onClick={this.saveChanges.bind(this)}>Save results</button>
            </div>
        );
    }

}

