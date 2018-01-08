import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { Timer } from "./models/timer";

export interface ISampleComponentProps 
{
    name: string;
    timers: Timer[]
}

export interface ISampleComponentState
{
    counter: number;
    timers: Timer[];
}

export default class SampleComponent extends React.Component<ISampleComponentProps, ISampleComponentState>
{
    constructor(props: ISampleComponentProps)
    {
        super(props);
        console.log("props", props);
        this.state = {
            counter: 0,
            timers: props.timers
        };
    }

    private addToCounter() : any
    {
        let oldTimers = this.state.timers;
        let timer: Timer = {};
        timer.description = "x";
        oldTimers.push(timer);
        this.setState({
            counter: this.state.counter + 1,
            timers: oldTimers
        });
    }

    render()
    {
        return (
            <div>
                <div>{this.props.name}</div>
                <button onClick={this.addToCounter.bind(this)}>PRESS ME-{this.state.counter}</button>
                <table className="table table-bordered table-responsive">
                    <tbody>
                        {this.props.timers.map((t, i) => {
                            return <tr key={i}>
                                        {t.description}
                                    </tr>
                        })}
                    </tbody>
                </table>
            </div>
            );
    }
}
