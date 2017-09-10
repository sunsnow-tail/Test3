import React, { Component } from 'react';
import './App.css';

const apiUrl = '';

class Country extends Component {

    getCountries = () => {
        
    }

    render(){

        this.getCountries();

        return(
            <div>
                <select id="selCountries"/>
            </div>
        );
    }
}

export default Country;