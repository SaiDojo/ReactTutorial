import React, { Component } from 'react';
import TasksView from './TasksView'

class App extends Component {

state = {
  tasks:[
    {id:1,desc:"Fill Water tank"},
    {id:2, desc:"get pizza"}
  ]
}

  render() {
    return (
      <div className="App container">
         <h1 className="blue-text center">Todo's</h1>
         <TasksView tasks={this.state.tasks}></TasksView>
      </div>
    );
  }
}

export default App;
