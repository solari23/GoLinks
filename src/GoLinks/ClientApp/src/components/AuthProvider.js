// ----------------------------------------------------------------------------------------------------------
// Copyright (c) Alexandre Kerametlian. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------------------------------------------------

import React, { Component } from 'react';
import { UserAgentApplication } from 'msal';

import { config } from '../Config';

export default function withAuthProvider(WrappedComponent) {
    return class extends Component {

        constructor(props) {
            super(props);

            this.state = {
                error: null,
                isAuthenticated: false,
                userAccount: {}
            };

            this.msalUserAgent = new UserAgentApplication({
                auth: {
                    clientId: config.auth.appId,
                    authority: config.auth.authority,
                    redirectUri: window.location.origin
                },
                cache: {
                    cacheLocation: "localStorage",
                    storeAuthStateInCookie: true
                }
            });
        }

        componentDidMount() {
            // If MSAL already has an account, the user is already logged in
            var account = this.msalUserAgent.getAccount();

            if (account) {
                this.setState(this.makeAuthenticatedState(account));
            }
        }

        render() {
            return <WrappedComponent
                error={this.state.error}
                isAuthenticated={this.state.isAuthenticated}
                user={this.state.user}
                login={() => this.login()}
                logout={() => this.logout()}
                setError={(message, debug) => this.setErrorMessage(message, debug)}
                {...this.props}
                {...this.state} />;
        }

        async login() {
            try {
                var authResponse = await this.msalUserAgent.loginPopup({
                    scopes: config.scopes,
                    prompt: "select_account"
                });

                if (authResponse) {
                    this.setState(this.makeAuthenticatedState(authResponse.account));
                }
            }
            catch (err) {
                this.setState(this.makeErrorState(err));
            }
        }

        logout() {
            this.msalUserAgent.logout();
        }

        setErrorMessage(message, debug) {
            this.setState({
                error: { message: message, debug: debug }
            });
        }

        makeAuthenticatedState(userAccount) {
            return {
                isAuthenticated: true,
                userAccount: userAccount,
                error: null
            };
        }

        makeErrorState(err) {
            return {
                isAuthenticated: false,
                userAccount: {},
                error: err
            };
        }
    }
}