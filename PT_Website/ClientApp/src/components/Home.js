import React, { Component } from 'react';
import Slideshow from './Slideshow';


export class Home extends Component {
    static displayName = Home.name;

    render() {  const images = [
        'https://laptopaz.vn/media/banner/23_Aug1c2ec0138f3726988d730fe8c8895087.jpg',
        'https://laptopaz.vn/media/banner/23_Aug1c2ec0138f3726988d730fe8c8895087.jpg',
        'https://laptopaz.vn/media/banner/27_Julb770209ae06e9aaf422768eac1a097e8.jpg',]
        return (
    
            <div className="App">
              
                <Slideshow images={images} />
            </div>
        );
    };

}
