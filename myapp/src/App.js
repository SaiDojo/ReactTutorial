import React, { Component } from 'react';
import logo from './logo.svg';
//import './App.css';
import Employees from './Employees';

class App extends Component {
  state = {
    emp1:{name:'John',age:'45',city:'Sanfransisco'},
    emp2:{name:'Norm',age:'52',city:'Dallas'}
  };

  // state = {
  //   employees:[
  //               {name:'John',age:'45',city:'Sanfransisco'},
  //               {name:'Norm',age:'52',city:'Dallas'}
  //             ]
  // };
  render() {
    return (
      <div className="App">
        <p>
          This is a sample React app
        </p>
        <div>-----------------------------------------------------</div>
        <Employees employee={this.state.emp1} ></Employees>
        <Employees employee={this.state.emp2} ></Employees>
      </div>
    );
  }
}

export default App;
