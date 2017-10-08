import * as React from "react";
import * as ReactDOM from "react-dom";
import { Project } from "../../models/projects/project";
import Select from "react-select";
import "react-select/dist/react-select.css";
import { SelectListItem } from "../../models/select-list-item";
import { IProjectComponentProps } from "./project-component-props";

export default class ProjectMembersComponent extends React.Component<IProjectComponentProps, any> {

    private valuesChanged(val: any) {
        const stateProject = this.props.project;
        stateProject.projectMemberIds = val;
        this.props.projectChanged(stateProject);
    }

    private saveChanges() {
        this.props.projectSaved(this.props.project);
    }

    render() {
        return (
            <div className="container-fluid">
                <div className="row">
                    <div className="form-group">
                        <label>Please pick project members</label>
                        <Select
                            name="UserIds"
                            options={this.props.project.usernamesWithIds}
                            multi={true}
                            value={this.props.project.projectMemberIds}
                            onChange={this.valuesChanged.bind(this)}
                        />
                        <br />
                        <button type="button" className="btn btn-default" onClick={this.saveChanges.bind(this)}>Save</button>
                    </div>
                </div>
            </div>
        );
    }
}