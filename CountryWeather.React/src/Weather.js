import React, { Component } from 'react';
import './App.css';

class Weather extends Component {
    constructor(props){
        super();
        this.state ={
            data: []
        }
    }

    //component life cycle - method is called if property changes
    componentWillReceiveProps(nextProps) {  

        if(nextProps.country === '' || nextProps.country !== this.props.country){
            this.setState((prevState)=>{
                                
                return (
                    {
                        data: []
                    }
                )
            })
            return;
        }

        var that = this;
        
        const apiUrl = 'http://localhost:3629/api/Weather/?city='+ nextProps.city +'&country=' + nextProps.country;

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
                        data: data
                    }
                )
            })
        });
    }

    render () {
        
        return(    
            <div>
                {Object.keys(this.state.data).map(function(key) {
                    return <div>{key}: {this.state.data[key]}</div>;
                }, this)}
            </div>
        );
    }
}

export default Weather;