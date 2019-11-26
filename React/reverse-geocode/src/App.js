import React, { Component, useState } from 'react';
import { GoogleApiWrapper } from 'google-maps-react';
import { Button } from 'react-bootstrap';
import * as appSettings from './AppSettings.json';
import Search from './AutoComplete'
import ReactDOM from "react-dom";


const formStyles = {
  width: '50%',
  height: '50%',
  margin: '2% 0% 1% 10%'
}

const headerStyles = {
  color: '#3399ff',
  margin: '2% 0% 1% 32%'
}

//Leitor de JSON
// function App() {
//   const [json, setJson] = useState("");
//   let fileInputRef = React.createRef();

//   return (
//     <div className="App">
//       <p>{json}</p>
//       <input
//         type="file"
//         ref={fileInputRef}
//         style={{ display: "none" }}
//         onChange={async e => {
//           const reader = new FileReader();
//           reader.onload = function() {
//             const text = reader.result;
//             setJson(text);
//           };
//           reader.readAsText(e.target.files[0]);
//         }}
//         accept=".json,application/json"
//       />
//       <button
//         onClick={() => {
//           fileInputRef.current.click();
//         }}
//       >
//         Upload JSON file
//       </button>
//     </div>
//   );
// }

// function readLines(input, func) {
//   var remaining = '';

//   input.on('data', function(data) {
//     remaining += data;
//     var index = remaining.indexOf('\n');
//     var last  = 0;
//     while (index > -1) {
//       var line = remaining.substring(last, index);
//       last = index + 1;
//       func(line);
//       index = remaining.indexOf('\n', last);
//     }

//     remaining = remaining.substring(last);
//   });

//   input.on('end', function() {
//     if (remaining.length > 0) {
//       func(remaining);
//     }
//   });
// }

// var fs = require('fs');
// var array = fs.readFileSync('file.txt').toString().split("\n");
// for(let i in array) {
//     console.log(array[i]);
// }

export class ReverseGeocode extends Component {

  constructor(props) {
    super(props);
    this.textInput = React.createRef();
    //this.textInput = rl.on('Line');
    this.toggleButtonState = this.toggleButtonState.bind(this);
    this.state = {
      data_text_id: "txt_consulta"
    }
  }

  toggleButtonState = () => {
    var InputData = [];

    InputData.pop();
    //Lat
    InputData.push(this.textInput.current.state.lat);
    //Lng
    InputData.push(this.textInput.current.state.long);

    ReactDOM.render(parseFloat(InputData[0]).toFixed(4) + parseFloat(InputData[1]).toFixed(4), document.getElementById('teste'));
  };

  render() {
    return (
      <form>
        <h2 style={headerStyles}>Reverse Geocode</h2>

        <div style={formStyles}>
          <div>
            <Search
              id={this.state.data_text_id}
              placeholder="Conteúdo para consulta"
              ref={this.textInput}
              //ref={fileInputRef}            
              //ref={this.readTextFile(file)}
            />
            <Button onClick={this.toggleButtonState} variant="primary">Buscar</Button>
          </div>
          <p></p>
          <b><div id="teste" ></div></b>     
          {/* <App /> */}
        </div>        
      </form>
    );
  };
}

export default GoogleApiWrapper({
  apiKey: appSettings.AppSettings.apiKey
})(ReverseGeocode);