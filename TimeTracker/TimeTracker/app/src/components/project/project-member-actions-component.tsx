import * as React from "react";
import * as ReactDOM from "react-dom";
import { IProjectComponentProps } from "./project-component-props";
import { ProjectMemberAction } from "../../models/projects/project-member-action";

export default class ProjectMemberActionsComponent extends React.Component<IProjectComponentProps, any> {

    private inputChanged(index: number, val: any) {
        const currentProject = this.props.project;
        currentProject.projectMemberActions[index].description = val.target.value;
        this.props.projectChanged(currentProject);
    }

    private deleteRow(index: number) {
        const currentProject = this.props.project;
        if (currentProject.projectMemberActions.length > 1) {
            currentProject.projectMemberActions.splice(index, 1);
            this.props.projectChanged(currentProject);
        }
    }

    private addRow() {
        const newProjectMemberAction: ProjectMemberAction = {
            description: "",
            id: "",
            projectMemberId: "",
        };
        const currentProject = this.props.project;
        currentProject.projectMemberActions.push(newProjectMemberAction);
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
                        {this.props.project.projectMemberActions.map((item: ProjectMemberAction, index: number) => {
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

