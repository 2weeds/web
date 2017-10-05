import * as React from "react";
import * as ReactDOM from "react-dom";
import { ProjectCreateModel } from "../../models/projects/project-create-model";
import Select from "react-select";
import "react-select/dist/react-select.css";
import { SelectListItem } from "../../models/select-list-item";

export interface IProjectMembersComponentProps {
    projectCreateModel: ProjectCreateModel
}

export interface IProjectMembersComponentState {
    values: SelectListItem[];
}

export default class ProjectMembersComponent extends React.Component<IProjectMembersComponentProps, IProjectMembersComponentState> {

    constructor(props: IProjectMembersComponentProps) {
        super(props);
        console.log("received props: ", props.projectCreateModel);
        this.state = {
            values: []
        };
    }

    private valuesChanged(val: any) {
        this.setState({
            values: val
        });
    }

    render() {
        return (
            <div className="container-fluid">
                <div className="row">
                    <div className="form-group">
                        <label>Please pick project members</label>
                        <Select
                            name="UserIds"
                            options={this.props.projectCreateModel.usernamesWithIds}
                            multi={true}
                            value={this.state.values}
                            onChange={this.valuesChanged.bind(this)}
                        />
                        <br/>
                        <button type="button" className="btn btn-default">Save</button>
                    </div>
                </div>
            </div>
        );
    }
}