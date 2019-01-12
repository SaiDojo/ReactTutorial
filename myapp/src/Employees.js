import React, {Component} from 'react';
import ReactDom from 'react-dom';

class Employees extends Component{

    render(){
        console.log(this.props.employees);
        const emps = this.props.employees.map((emp,index)=> {
            console.log(index);
            return (
                <div className="employee" key={index}>
                <div>Name: {emp.name}</div>
                <div>Age: {emp.age}</div>
                <div>City: {emp.city}</div>
            </div>
            )
        });

        return (
            <div>
                {emps}
            </div>
        )
    }

}

export default Employees;