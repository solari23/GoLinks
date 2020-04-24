// ----------------------------------------------------------------------------------------------------------
// Copyright (c) Alexandre Kerametlian. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------------------------------------------------

import React, { Component } from 'react';
import { Button } from 'reactstrap';

export class LandingPage extends Component {
    static displayName = LandingPage.name;

    render() {
        return <Button onClick={this.props.login}>Sign In</Button>;
    }
}