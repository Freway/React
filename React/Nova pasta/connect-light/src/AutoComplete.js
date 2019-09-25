// Imports
import React, { Component } from "react";

// Import Search Bar Components
import { Form } from "react-bootstrap";

class Search extends Component {
  // Define Constructor
  constructor(props) {
    super(props);

    // Declare State
    this.state = {
      city: "",
      query: ""
    };
    // Bind Functions

    this.handlePlaceSelect = this.handlePlaceSelect.bind(this);
  }

  componentDidMount() {
    var options = {
      types: ["address"]
    }; // To disable any eslint 'google not defined' errors

    // Initialize Google Autocomplete
    /*global google*/

    this.autocomplete = new google.maps.places.Autocomplete(
      document.getElementById(this.props.id),
      options
    );

    // Fire Event when a suggested name is selected
    this.autocomplete.addListener("place_changed", this.handlePlaceSelect);
  }

  handlePlaceSelect() {
    // Extract City From Address Object
    let addressObject = this.autocomplete.getPlace();

    let address = addressObject.address_components;

    // Check if address is valid
    if (address) {
      // Set State
      this.setState({
        city: address[0].long_name,
        query: addressObject.formatted_address
      });

      this.props.handleSelect(addressObject);
    }
  }

  render() {
    return (
      <div>
        <Form.Control
          id={this.props.id}
          required
          // value={this.state.query}
          onChange={e => this.setState({ query: e.target.value })}
          style={{
            margin: "0 auto",
            maxWidth: 800
          }}
        />
      </div>
    );
  }
}

export default Search;
