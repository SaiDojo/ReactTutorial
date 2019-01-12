import React from 'react';
import './ActivitiesView.css';


const ViewActivities = (props) => {

    const activities = props.activities.map((actvty)=>{
        return(
            <li className={actvty.status===0?'show':'strikeoff'}>{actvty.description}</li>
        )
    })

    return(
        <div>
            <ul>
                {activities}
            </ul>
        </div>
    )

}

export default ViewActivities;
