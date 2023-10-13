import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { Navbar } from './NavMenu';
import { Footer } from './Footer'
export class Layout extends Component {
    static displayName = Layout.name;

    render() {
        return (
            <div>
                <Navbar />
                <Container tag="main">
                    {this.props.children}
                </Container>
                <Footer />
            </div>

        );
    }
}
