import React, { Component } from 'react';
import './App.css';

const apiUrl = 'http://localhost:3629/api/Countries';

class Country extends Component {
    constructor(){
        super();
        this.state = {
            data: []
        };
    } 

    componentDidMount() {    
        var that = this;
      
        fetch(apiUrl, {
            headers : { 
                'Content-Type': 'application/json',
                'Accept': 'application/json'
                }      
            })
        .then(function(response) {
          if (response.status >= 400) {
            throw new Error("Bad response from server");
          }
          return response.json();
        })
        .then(function(data) {
          that.setState({ data: data });
        });
      }

    render(){

        return(
            <div>
                <select id="selCountries">
                {this.state.data.map(id =>
                    <option key={id} value={id}>{id}</option>
                )}
                </select>
            </div>
        );
    }
}

export default Country;