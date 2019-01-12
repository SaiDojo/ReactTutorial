import React from 'react';

const ActivityView = (props)=>{
// const actvties = this.props.activities
console.log(props.activity);
const actvty = props.activity;
    return(
         <div>
                <div>Description:{ actvty.description} </div>
                <div>Date:{actvty.date}</div>
                <div>Status:{actvty.status}</div>
            </div>
    )

}

export default ActivityView;

