import * as React from "react";
import * as ReactDOM from "react-dom";
import ProjectTabsComponent from "./components/project-tabs";
import { Project } from "./models/projects/project";

declare var project: Project;

ReactDOM.render(
    <ProjectTabsComponent project={project} />,
    document.getElementById("main")
);