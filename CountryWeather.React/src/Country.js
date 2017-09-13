import React, { Component } from 'react';
import './App.css';

const apiUrl = 'http://localhost:3629/api/Countries';

class Country extends Component {
    constructor(props){
        super();
        this.state = {
            data: ['Countries are loading...']
        };
    } 

    //component life cycle - method is called after rendering
    componentDidMount() {    
        var that = this;
      
        //set the number synchrones operations
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
            //change state - rerender this component
          that.setState({ data: data });
        });
      }

    render(){

        return(
            <div>
                <select onChange={this.props.onCountryChange}>
                {this.state.data.map(id =>
                    <option key={id} value={id}>{id}</option>
                )}
                </select>
            </div>
        );
    }
}

export default Country;