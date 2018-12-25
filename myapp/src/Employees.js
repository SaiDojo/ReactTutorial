import React, {Component} from 'react';
import ReactDom from 'react-dom';

class Employees extends Component{

    render(){
        console.log(this.props.employee);
        const emp = this.props.employee;
        return (
            <div className="employees">
                <div>Name: {emp.name}</div>
                <div>Age: {emp.age}</div>
                <div>City: {emp.city}</div>
            </div>
        )
    }

}

export default Employees;