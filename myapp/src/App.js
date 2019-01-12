import React, { Component } from 'react';
import logo from './logo.svg';
//import './App.css';
import Employees from './Employees';
import Emp from './UIComponent';
import NewEmp from './AddEmp';

 class App extends Component {
//   state = {
//     emp1:{name:'John',age:'45',city:'Sanfransisco'},
//     emp2:{name:'Norm',age:'52',city:'Dallas'}
//   };

  state = {
    employees:[
                {name:'John',age:'45',city:'Sanfransisco',id:''},
                {name:'Norm',age:'52',city:'Dallas',id:''}
              ]
  };

  addEmployee = (e, name, age, city)=>{
    e.preventDefault();
    console.log(name, age, city);
  }

  render() {
    return (
      <div className="App">
        <p>
          This is a sample React app
        </p>
        <div>-----------------------------------------------------</div>
        <Employees employees={this.state.employees} ></Employees>
        <Emp employees={this.state.employees}></Emp>
        <NewEmp addEmployee={this.addEmployee}></NewEmp>
      </div>
    );
  }
}

export default App;
