import * as React from "react";
import {RegisteredAction} from "../../models/projects/registered-action";
import {Project} from "../../models/projects/project";
import {SelectListItem} from "../../models/select-list-item";
import Select from "react-select";
import "react-select/dist/react-select.css";
import DatePicker from 'react-date-picker';
import * as moment from "moment";

export interface IMultipleTimesEditocComponentProps {
    registeredActions: RegisteredAction[],
    possibleUserActions: SelectListItem[]
}

export default class MultipleTimesEditorComponent extends React.Component<IMultipleTimesEditocComponentProps, any> {
    
    private dateChanged() : any {
        console.log("asd");        
    }
    
    render() {
        console.log("this.props.registeredActions", this.props.registeredActions);
        console.log("this.props.possibleUserActions", this.props.possibleUserActions);
        return (
            <div className={"container-fluid"}>
                <div className={"row"}>
                    <label>Čia galite redaguoti įvestas reikšmes</label>
                    <table className={"table table-responsive table-bordered table-hover table-striped table-condensed"}>
                        <thead>
                            <tr>
                                <th>Start date</th>
                                <th>End date</th>
                                <th>RegisteredAction</th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.props.registeredActions.map((registeredAction: RegisteredAction, index: number) => {
                                return (
                                    <tr key={index}>
                                        <th>
                                            <DatePicker
                                                value={moment(registeredAction.StartTime).toDate()}
                                                onChange={this.dateChanged.bind(this)}
                                            />
                                        </th>
                                        <th>
                                            <DatePicker
                                                value={moment(registeredAction.EndTime).toDate()}
                                                onChange={this.dateChanged.bind(this)}
                                            />
                                        </th>
                                        <th>
                                            <Select
                                                name={"SingleProjectMemberAction"}
                                                options={this.props.possibleUserActions}
                                                multi={false}
                                                value={registeredAction.ProjectMemberActionId}
                                            />
                                        </th>    
                                    </tr>    
                                );
                            })}
                        </tbody>
                    </table>    
                </div>
            </div>
        );
    }
}