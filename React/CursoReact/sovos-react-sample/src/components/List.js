import React from "react";


const List = props => (
  <div>
    {props.list.map((item, key) => (
      <p key={key}>
        {item.movieName} - {item.minutes} min
      </p>
    ))}
  </div>
);
export default List;
