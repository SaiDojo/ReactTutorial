import React, {Component} from 'react'

class Activities extends Component
{
    render(){
        const actvty = this.props;
        console.log(actvty.activity);

        return(
            <div>
                <div>Description: {actvty.activity.description}</div>
                <div>Date: {actvty.activity.date}</div>
                <div>Status: {actvty.activity.status}</div>
            </div>
        )
    }
}

export default Activities;