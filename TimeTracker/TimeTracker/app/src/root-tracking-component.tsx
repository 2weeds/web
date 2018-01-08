import * as React from "react";
import * as ReactDOM from "react-dom";
import TrackingComponent from "./components/tracking/tracking-component";
import {SelectListItem} from "./models/select-list-item";
import {ProjectSelectListItem} from "./models/project-select-list-item";

declare var projects: ProjectSelectListItem[];

ReactDOM.render(
    <TrackingComponent projects={projects}/>,
    document.getElementById("main")
);