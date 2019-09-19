import React, { Component } from "react";
import MovieList from "./constants/movieList";
import List from "./components/List";

class App extends Component {
  state = {
    movieList: MovieList,
    inputSearch: ""
  };

  counterMinutes = () => {
    const { movieList } = this.state;

    return movieList.reduce((total, current) => total + current.minutes, 0);
  };

  renderMovieList = () => {
    const { movieList, inputSearch } = this.state;

    if (!inputSearch) return <List list={movieList} />;

    let newList = movieList.filter(movie =>
      movie.movieName.toLowerCase().includes(inputSearch.toLowerCase())
    );

    return <List list={newList} />;
  };

  render() {
    return (
      <div>
        <h3>Movie list</h3>
        <div>
          <label>Movie name:</label>
          <input
            id="input-movies"
            type="text"
            placeholder="Enter movie name"
            onChange={event =>
              this.setState({ ...this.state, inputSearch: event.target.value })
            }
          />
          {" "}
          <span>Total duration: </span>
          <span>{this.counterMinutes()}</span>
          <span>min</span>
        </div>
        {this.renderMovieList()}
      </div>
    );
  }
}

export default App;
