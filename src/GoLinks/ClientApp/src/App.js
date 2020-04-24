// ----------------------------------------------------------------------------------------------------------
// Copyright (c) Alexandre Kerametlian. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------------------------------------------------

import React, { Component, Suspense, lazy } from 'react';
import { Container, Spinner } from 'reactstrap';
import { Route } from 'react-router';

import { NavMenu } from './components/NavMenu';
import { ErrorMessage } from './components/ErrorMessage';
import withAuthProvider from './components/AuthProvider';

import './custom.css'

import { LandingPage } from './components/LandingPage';

// Lazy load these pages
const LinkManagementPage = lazy(() => import('./components/LinkManagementPage'))
const AdminPage = lazy(() => import('./components/AdminPage'))

const loadingView =
    <div>
        <h1>Hold on a sec...</h1>
        <Spinner />
    </div>

class App extends Component {
    static displayName = App.name;

    render() {

        let errorDisplay = null;
        let error = null;

        if (error) {
            errorDisplay = <ErrorMessage message={error.message} debug={error.debug} />
        }

        let mainPage = this.props.isAuthenticated ? <LinkManagementPage {...this.props} /> : <LandingPage login={this.props.login} />;

        console.log(this.props);

        return (
            <div>
                <NavMenu />
                <Container>
                    <Suspense fallback={loadingView}>
                        {errorDisplay}
                        <Route exact path='/' render={() => mainPage} />
                        <Route path='/Admin' component={AdminPage} />
                    </Suspense>
                </Container>
            </div>
        );
    }
}

export default withAuthProvider(App);