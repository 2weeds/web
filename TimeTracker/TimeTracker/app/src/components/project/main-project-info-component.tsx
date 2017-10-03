import * as React from "react";
import * as ReactDOM from "react-dom";

export default class MainProjectInfoComponent extends React.Component<any, any> {
    render() {
        return (
            <div className="container-fluid">
                <form method="post" action="/CreateProject">
                    <div className="form-group">
                        <label>Project name:</label>
                        <input name="ProjectName" type="text" className="form-control" id="ProjectName" />
                        <br />
                        <button type="submit" className="btn btn-default">Save</button>
                    </div>
                </form>
            </div>
        );
    }
}