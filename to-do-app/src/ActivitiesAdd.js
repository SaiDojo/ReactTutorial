import React, {Component} from 'react';

class AddActivities extends Component{

    state = {
        description:'test',
        date:'12/31/2018',
        status:0, 
        key:-1
    }

    handleSubmit = (e) => {
        e.preventDefault();
        console.log(document.getElementById("txtDesc").value);
        
    }

    render(){
        return(
            <form onSubmit={this.handleSubmit}>
                <label htmlFor="txtDesc">Description: </label>
                <input type="text" id="txtDesc"></input>
                &nbsp;
                <button>Add</button>
            </form>
        )
    }

}

export default AddActivities;
