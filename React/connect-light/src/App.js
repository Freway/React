import React, { Component, props } from 'react';
import { Map, GoogleApiWrapper, Marker, InfoWindow} from 'google-maps-react';
import ReactDOM from "react-dom";
import axios from 'axios';
import { Button , FormControl, InputGroup, ListGroup, Alert} from 'react-bootstrap';
import * as appSettings from './AppSettings.json';
import Moment from 'moment';
import 'moment/locale/pt-br'
import { Modal } from 'react-bootstrap';
import Search from './AutoComplete'

const formStyles  ={
  width: '50%',
  height: '50%',
  margin: '2% 0% 1% 10%'
}

const headerStyles  ={
  color: '#3399ff',
  margin: '2% 0% 1% 32%'
}

const mapStyles = {
  width: '80%',
  height: '60%',
  top: '1%',
  left: '10%',
  border: '3px solid #3399ff'
};

export class MapContainer extends Component {

  constructor(props) {
    super(props);  
    this.textInput = React.createRef(); 
    this.optionText = React.createRef();
    this.textRaio = React.createRef();
    this.toggleButtonState = this.toggleButtonState.bind(this);
    this.state = {  posx : { result: -23.4991613},
                    posy : { result: -46.8533796},
                    placa : { result : ""},
                    hour : { result : ""},
                    address : "",
                    selectedPlace: null,
                    activeMarker: null,
                    showingInfoWindow: true,
                    showSearchName: true,
                    isOpen: false,
                    drivers: [],
                    showRaio: true,
                    data_text_id: "txt_consulta"                    
                  }
  }

  toggleButtonState = () => {

    if(this.optionText.current.value < 1){
      alert('Selecione uma opção');
      return;
    }

    if(this.textInput.current.value.trim() === ''&& this.optionText.current.value != 4){
      alert('Insira uma informação para consulta');
      this.textInput.current.value = '';
      return;
    }

    if(this.optionText.current.value == 4 && this.textRaio.current.value.trim() === ''){
      alert('Insira uma informação para consulta');
      this.textRaio.current.value = '' ;
      return;
    }
    
    this.doPositioning(this.optionText.current.value, this.textInput.current.value,this.textRaio.current.value);
  };  

  displayDriver = (option, value) => {
    this.doPositioning(option, value);
    this.textInput.current.value = value;

    this.handleClose();
  }


  doPositioning = (option, value, raio) => {

     GetEndpoint.getPosition(option, value, raio).then( response => {
      if(response == null){
        alert("Não foi possível realizar a pesquisa.");
        return;
      }

      if(response.success && response.data.length > 0){       
        fetch('https://maps.googleapis.com/maps/api/geocode/json?address=' + response.data[0].lat + ',' + response.data[0].long + '&key=' + appSettings.AppSettings.apiKey)
          .then((response) => response.json())
          .then((responseJson) => {
            const address1 = responseJson.results[0].formatted_address;
              this.setState({
                posx : { result: response.data[0].lat},
                posy : { result: response.data[0].long},
                placa : { result: response.data[0].licencePlate},
                hour: { result: response.data[0].rowReferenceTime},
                showRaio: {result: raio},
                address : {result: address1}
              }) 
        })
      }

      else{
        alert('Informação não encontrada!')
      }
    });
  }

  optionChange = () => {
    this.setState({
      showSearchName: this.optionText.current.value != 3
    });

    this.setState({
      showRaio: this.optionText.current.value != 4,

     });

  }

  onMarkerClick = (props, marker, e) => {
    this.setState({
      selectedPlace: props,
      activeMarker: marker,
      showingInfoWindow: true
    });
  };

  handleClose = () => this.setState({
    isOpen: false
  });

  handleShow = () => {
    const promisse = GetEndpoint.getDrivers(this.textInput.current.value).then( response => {
      if(response == null){
        alert("Não foi possível realizar a pesquisa.");
        return;
      }

      if(response.success && response.data.length > 0){
        this.setState({
          isOpen: true,
          drivers: response.data
        });
    
      }
      else{
        alert('Informação não encontrada!')
      }
    });    
  }

  render() {

    console.log(this.state.posx)

    let markers = [];
   
    markers.push(<Marker position={{lat: this.state.posx.result, lng: this.state.posy.result}} onClick={this.onMarkerClick}/>)
    
    Moment.locale('pt-BR');

    return (
      <form>
        <h2 style={headerStyles}>Connect Light - Visualizador de Frota</h2>

        <div style={formStyles}>
          <InputGroup className="mb-6">
            <div class="input-group">
              <div  style={{width: `200px`}}>
              <select class="custom-select" id="inputGroupSelect04" ref={this.optionText} onChange={this.optionChange} >
                <option value="0" selected>Selecione a opção...</option>
                <option value="1">Placa do Veículo</option>
                <option value="2">Matrícula</option>
                <option value="3">Nome do Condutor</option>
                <option value="4">Endereço</option>
              </select>
              </div>
                 <div>
    <label hidden={this.state.showRaio}>
    <FormControl aria-describedby="basic-addon1"ref={this.textRaio}type="number" min="0" placeholder="Raio de Busca"/>
    </label>
    </div>              
              <label hidden={this.state.showRaio== true}>
              <Search            
                  id={this.state.data_text_id}
                  placeholder="Conteúdo para consulta"
                  ref={this.textInput}
                  value={this.state.posx,this.state.posy}
                />                  
              </label>  
              <label hidden={this.state.showRaio ==false}>
              <FormControl aria-describedby="basic-addon1"  style={{width: `600px`}} ref={this.textInput} placeholder="Conteúdo para consulta"/> 
              </label>                            

                <Button onClick={this.handleShow} variant="info" hidden={this.state.showSearchName} >Selecionar</Button>
                <Button onClick={this.toggleButtonState} variant="primary">Buscar</Button>
              
            </div>
          </InputGroup>
        </div>

        <div>
       
          <Map
            google={this.props.google}
            zoom={15}
            style={mapStyles}
            initialCenter={{
              lat: this.state.posx.result,
              lng: this.state.posy.result}}
            center={{
                lat: this.state.posx.result,
                lng: this.state.posy.result}}
          >
              
            {markers}
        
            <InfoWindow
              marker={this.state.activeMarker}
              visible={this.state.showingInfoWindow}>
                <div>
                  <h4>{this.state.placa.result}</h4>
                  <h5>{this.state.address.result}</h5>
                  <h6>Data da Última Comunicação: {Moment(this.state.hour.result).format('LLL') }</h6>
                </div>
            </InfoWindow>        
          </Map>        
  
          <Modal show={this.state.isOpen} onHide={this.handleClose}>
            <Modal.Header closeButton>
              <Modal.Title>Condutore(s)</Modal.Title>
            </Modal.Header>
            <Modal.Body>
              <div>
                <React.Fragment>
                  <ul className="list-group">
                    {this.state.drivers.map(driver => (
                      <li className="list-group-item list-group-item-default" onClick={() => this.displayDriver('3', driver.driverName)}>
                        {driver.driverName} 
                      </li>
                    ))}
                  </ul>
                </React.Fragment>              
              </div>
            </Modal.Body>
            <Modal.Footer>
              <Button variant="secondary" onClick={this.handleClose}>
                Fechar
              </Button>
            </Modal.Footer>
          </Modal>          
        </div>
        }
      </form>
    );
  }
}

class GetEndpoint extends Component {

  static async getToken() { 
    let json = null;
    
    await axios.post(appSettings.AppSettings.restUrl + "Token/Get", {
      user: appSettings.AppSettings.user,
      password: appSettings.AppSettings.password,
    }, 
    {
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      }).then(response => {
        json = response
      }).catch(error => {
        console.log(error.response);
      try {
        json = error.response;
      } catch {
        json = null
      }
    });
    return json != null ? json.data : null;
  };

   static getDrivers = async (name) => {

     let json = null;

     let url = appSettings.AppSettings.restUrl + "Position/GetDriversName";

     await axios.post(url + '?name=' + name,
     {
       headers: {
         'Accept': 'application/json',
         'Content-Type': 'application/json',
       },
       }).then(response => {
         json = response;
       }).catch(error => {
         console.log(error.response);
       try {
         json = error.response;
       } catch {
        json = null
       }
     });
     return json != null ? json.data : null;
   };   
  
  static getPosition = async (option, search, raio) => {
  
    let json = null;

    let url = "";

    switch(option) {
      case '1':
        url =  appSettings.AppSettings.restUrl + "Position/GetVehicleStatusLight";
        break;
      case '2':
        url =  appSettings.AppSettings.restUrl + "Position/GetVehicleStatusByNationalId";
        break;
      case '3':
        url =  appSettings.AppSettings.restUrl + "Position/GetVehicleStatusByDriverName";
        break;
      case '4':
        url =  appSettings.AppSettings.restUrl + "Position/GetAddressbyRadius";
        break;
    }

     await axios.post(url, {
           TokenId: "",
           InputData: [search],
           RaioData: [raio]
         },
    {
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      }).then(response => {
        json = response;
      }).catch(error => {
        console.log(error.response);
      try {
        json = error.response;
      } catch {
       json = null
      }
    });
    return json != null ? json.data : null;
   };
}

export default GoogleApiWrapper({
  apiKey: appSettings.AppSettings.apiKey
})(MapContainer);

