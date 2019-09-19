import React, { Component } from "react";
import TodoList from "./constants/taskList";
import List from "./components/List";

class App extends Component {
  state = {};

  render() {
    return (
      <div>
        <h3>To Do List</h3>
        <div>
          <div>
            <div>
              <label>
                <b>Insert a new task:</b>
              </label>
              <input
                type="text"
                placeholder="New task name"
              />
              <button onClick={}>Add</button>
            </div>
            <div>
              <label>
                <b>Search task:</b>
              </label>
              <input
                type="text"
                placeholder="Enter task name"
              />
            </div>
          </div>
        </div>

        <div>
          <h3>To Do</h3>
          {"FUNC"}
        </div>
      </div>
    );
  }
}

export default App;
