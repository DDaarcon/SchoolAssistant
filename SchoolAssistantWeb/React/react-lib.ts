//@ts-nocheck
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import * as ReactDOMServer from 'react-dom/server';
import * as ReactSelect from 'react-select'
import FullCalendar from '@fullcalendar/react' // must go before plugins
import dayGridPlugin from '@fullcalendar/daygrid' // a plugin!

global.React = React;
global.ReactDOM = ReactDOM;
global.ReactDOMServer = ReactDOMServer

globalThis.Components = {};