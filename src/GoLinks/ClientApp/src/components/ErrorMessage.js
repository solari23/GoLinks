// ----------------------------------------------------------------------------------------------------------
// Copyright (c) Alexandre Kerametlian. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------------------------------------------------

import React, { Component } from 'react';
import { Alert } from 'reactstrap';


export class ErrorMessage extends Component {

    render() {
        let debugInfo = null;
        if (this.props.debug) {
            debugInfo = <pre className="alert-pre border bg-light p-2"><code>{this.props.debug}</code></pre>
        }

        return (
            <Alert color="danger">
                <p className="mb-3">{this.props.message}</p>
                {debugInfo}
            </Alert>
        );
    }
}