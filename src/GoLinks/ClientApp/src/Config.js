// ----------------------------------------------------------------------------------------------------------
// Copyright (c) Alexandre Kerametlian. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ----------------------------------------------------------------------------------------------------------

import { deepFreeze } from './Utils';

const isDevelopmentEnvironment = process.env.NODE_ENV !== 'production';

export const config = deepFreeze(isDevelopmentEnvironment
    ?
    // Settings for development environment.
    {
        isDevelopmentEnvironment: true,

        auth: {
            appId: 'f6c82270-b4c2-40ac-82d4-19d1739e1594',
            authority: 'https://login.microsoftonline.com/bfdabf95-29f1-4a98-abb5-b90de95377b6'
        }
    }
    :
    // Settings for production.
    {
        isDevelopmentEnvironment: false,

        auth: {
            appId: 'f6c82270-b4c2-40ac-82d4-19d1739e1594',
            authority: 'https://login.microsoftonline.com/bfdabf95-29f1-4a98-abb5-b90de95377b6'
        }
    }
);