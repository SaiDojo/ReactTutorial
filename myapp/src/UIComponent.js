import React from 'react';
import ReactDOM from 'react-dom';

const Emp = (props)=>{
        const emps = props.employees.map(em=>{
            return (
                <div className="emp">
                    <div>Name: {em.name}</div>
                    <div>Age: {em.age}</div>
                    <div>City: {em.city}</div>
                </div>
            )
        } );

        return(
            <div>
                {emps}
            </div>
        )
    
}

export default Emp;