import React from 'react';

const NewEmp = (props)=>{
    return(
        <div>
            <form onSubmit={props.addEmployee()}>
            <label htmlFor="name">Name</label>
            <input type="text" id="name"></input>
            <br/>
            <label htmlFor="age">Age</label>
            <input type="text" id="age"></input>
            <br/>
            <label htmlFor="city">City</label>
            <input type="text" id="city"></input>
            <button>Add Employee</button>
            </form>
        </div>
    )
}



export default NewEmp;