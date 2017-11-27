import * as React from "react";
import * as ReactDOM from "react-dom";
import TrackingComponent from "./components/tracking/tracking-component";
import {SelectListItem} from "./models/select-list-item";

declare var projects: SelectListItem[];

ReactDOM.render(
    <TrackingComponent projects={projects}/>,
    document.getElementById("main")
);