import React, { Component } from 'react';
import './App.css';
// import Activities from './Activities';
// import ActivityView from './ActivityView';
import ViewActivities from './ActivitiesView';
import AddActivities from './ActivitiesAdd';

class App extends Component {

  // state = {
  //   activities:[
  //                   {description:'Pay Rent',date:'12/31/2018',status:'not done'},
  //                   {description:'Pay Credit card',date:'12/31/2018',status:'not done'},
  //                   {description:'Check Account',date:'12/31/2018',status:'not done'},
  //               ]
  // };

  state = {
    // description:'Pay Rent',
    // date:'12/31/2018',
    // status:'not done',
    activities:[
                    {description:'Pay Rent',date:'12/31/2018',status:0, key:1},
                    {description:'Pay Credit card',date:'12/31/2018',status:0, key:1},
                    {description:'Check Account',date:'12/31/2018',status:1, key:2},
                ]
  };


  AddActivity(actvty)
  {
    console.log(actvty);
  }

  render() {
    return (
      <div>
        <h1>To-Do App</h1>
        {/* <Activities activity={this.state}></Activities>
        <ActivityView activity={this.state}></ActivityView> */}
        <AddActivities AddActivity = {this.AddActivity}></AddActivities>
        <br/>
        <ViewActivities activities={this.state.activities} ></ViewActivities>

      </div>
    );
  }
}

export default App;
