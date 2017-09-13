import React, { Component } from 'react';
import './App.css';
import Country from './Country';
import City from './City';


class App extends Component {
  constructor(){
    super();
    this.state = {
      selectedCountry: ''
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
    console.log(e.target.value);
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
      </div>
    )
  }
}

export default App;
