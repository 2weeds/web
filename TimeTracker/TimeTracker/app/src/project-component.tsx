import * as React from "react";
import * as ReactDOM from "react-dom";
import ProjectTabsComponent from "./components/project-tabs";
import { ProjectCreateModel } from "./models/projects/project-create-model";

declare var projectCreateModel: ProjectCreateModel;

ReactDOM.render(
    <ProjectTabsComponent projectCreateModel={projectCreateModel} />,
    document.getElementById("main")
);