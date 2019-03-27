import React from 'react'

const TasksView = (props) => {

    const tasks = props.tasks.map( task =>{
        return (
            <div className="collection-item blue-text" key={task.id}>
               <span> {task.desc}</span>
                   
            </div>
        )
    })
    return(
        <div className="collection">{tasks}</div>
    )
}

export default TasksView;