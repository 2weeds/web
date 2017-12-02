import * as React from "react";
import { Project } from "../../models/projects/project";
import Select from "react-select";
import "react-select/dist/react-select.css";
import { ITrackingProps } from "./tracking-props"

export interface IProjectPickerProps extends ITrackingProps {
    valueChanged(selectedProject : Project, canAdminModeBeEnabled: boolean): any;
    selectedValue: string;
}

export default class ProjectPickerComponent extends React.Component<IProjectPickerProps, any> {
    
    private valueChanged(val: any) {
        console.log("val", val);
        this.props.valueChanged(val, val.canAdminModeBeEnabled);
    }
    
    render() {
        console.log("this.props.projects", this.props.projects);
        return (
          <div className="container-fluid">
              <div className="row">
                  <div className={"form-group"}>
                      <label>Please pick project</label>
                      <Select
                        name={"ProjectSelector"}
                        options={this.props.projects}
                        multi={false}
                        value={this.props.selectedValue}
                        onChange={this.valueChanged.bind(this)}
                      />
                  </div>    
              </div>
          </div>    
        );
    }
    
} 