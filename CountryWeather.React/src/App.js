import React, { Component } from 'react';
import './App.css';
import Country from './Country';
import City from './City';
import Weather from './Weather';

class App extends Component {
  constructor(){
    super();
    this.state = {
      selectedCountry: '',
      selectedCity: ''
    }
  }

  //callback function to get data from child component
  onCountryChange = (e) => {
    let country = e.target.value;
    //change state => make children components rerender if state is different
    this.setState((prevState) => {
      return {
        selectedCountry: country
      };
    });
  }

  onCityChange =(e) => {
    let city = e.target.value;

    this.setState((prevState) => {
      return {
        selectedCity: city
      };
    });
  }

  render() {
    return (
      <div >
        <div >
          <Country onCountryChange={this.onCountryChange} />
        </div>
        <div >
          <City country={this.state.selectedCountry} onCityChange={this.onCityChange} />
        </div>
        <div >
          <Weather city={this.state.selectedCity} country={this.state.selectedCountry} />
        </div>
      </div>
    )
  }
}

export default App;
