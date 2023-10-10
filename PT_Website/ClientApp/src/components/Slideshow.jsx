import React, { useState } from 'react';
import './Slideshow.css';

const Slideshow = ({ images }) => {
    const [currentIndex, setCurrentIndex] = useState(0);

    const nextSlide = () => {
        setCurrentIndex((prevIndex) => (prevIndex + 1) % images.length);
    };

    const prevSlide = () => {
        setCurrentIndex((prevIndex) =>
            prevIndex === 0 ? images.length - 1 : prevIndex - 1
        );
    };

    return (
        <div className="slideshow-container">
            <button className="prev" onClick={prevSlide}>
                &#10094;
            </button>
            {images.map((image, index) => (
                <div
                    key={index}
                    className={`slide ${index === currentIndex ? 'active' : ''}`}
                >
                    <img src={image} alt={`Slide ${index}`} />
                </div>
            ))}
            <button className="next" onClick={nextSlide}>
                &#10095;
            </button>
        </div>
    );
};

export default Slideshow;
