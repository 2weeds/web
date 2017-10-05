import * as React from "react";
import * as ReactDOM from "react-dom";
import request from 'axios';

export interface IMainProjectInfoComponentProps {
    projectNameSaved(projectId: string): void;
}

export interface IMainProjectInfoComponentState {
    projectName: string
}

export default class MainProjectInfoComponent extends React.Component<IMainProjectInfoComponentProps, IMainProjectInfoComponentState> {

    constructor() {
        super();
        this.state = {
            projectName: ""
        };
    }

    private projectNameChanged(newProjectName: any) {
        this.setState({
            projectName: newProjectName.target.value
        });
    }

    private saveProject() {
        const projectName = this.state.projectName;
        const config: any = {
            headers: {
                'Content-Type': 'application/json',
            }
        };
        if (projectName.length > 0) {
            const projectToPost = {
                    'Title': projectName
            };
            console.log("payload:", { "Title": projectName } );
            request.post('/Projects/Create', JSON.stringify(projectToPost), config)
                .then((response: any) => {
                    console.log("response is: ", response);
                })
                .catch((response: any) => {
                    console.log("error response: ", response);
                });
        }
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
                        value={this.state.projectName}
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