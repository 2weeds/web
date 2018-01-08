import * as React from "react";
import * as ReactDOM from "react-dom";
import SampleComponent from "./sample-component";
import { Timer } from "./models/timer";

declare var myData: Timer[];

ReactDOM.render(
    //<div>Hello world a</div>,
    <SampleComponent name="OKEY" timers={myData} />,
    document.getElementById("main")
);
