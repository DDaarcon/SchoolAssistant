//@ts-nocheck
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import * as ReactDOMServer from 'react-dom/server';

import DataManagementMain from './data-management/main';

// any css-in-js or other libraries you want to use server-side
//import * as Select from 'react-select';
//import makeAnimated from 'react-select/animated';

global.React = React;
global.ReactDOM = ReactDOM;
global.ReactDOMServer = ReactDOMServer;
//global.Select = Select;
//global.makeAnimated = makeAnimated;

globalThis.Components = { DataManagementMain };