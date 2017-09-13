import React, { Component } from 'react';
import './App.css';

class City extends Component {
    constructor(props){
        super();
        this.state ={
            cities: []
        }
    }

    //component life cycle - method is called if property changes
    componentWillReceiveProps(nextProps) {  

        if(nextProps.country === '' || nextProps.country === this.props.country){
            return;
        }

        var that = this;
        
        const apiUrl = 'http://localhost:3629/api/Cities/?country=' + nextProps.country;

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
            
            //update the state and rerender the component
            that.setState((prevState)=>{
                                
                return (
                    {
                        cities: data
                    }
                )
            })
        });
    }

    render () {
        
        return(
            
            <div>                
                <select onChange={this.props.onCityChange}>
                {this.state.cities.map(id =>
                    <option key={id} value={id}>{id}</option>
                )}
                </select>
            </div>
        );
    }
}

export default City;