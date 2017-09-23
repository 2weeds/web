import * as React from 'react';
import * as ReactDOM from 'react-dom';

export interface ISampleComponentProps 
{
    name: string;
}

export interface ISampleComponentState
{
    counter: number;
}

export default class SampleComponent extends React.Component<ISampleComponentProps, ISampleComponentState>
{
    constructor(props: ISampleComponentProps)
    {
        super(props);

        this.state = {
            counter: 0,

        };
    }

    private addToCounter() : any
    {
        this.setState({
            counter: this.state.counter + 1,
        })
    }

    render()
    {
        return (
            <div>
                <div>{this.props.name}</div>
                <button onClick={this.addToCounter.bind(this)}>PRESS ME-{this.state.counter}</button>
            </div>
            );
    }
}
