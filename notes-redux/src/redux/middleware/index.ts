import { applyMiddleware } from 'redux';
import loggingMiddleware from './logging';
import localStorageMiddleware from './local-storage';


export default applyMiddleware(
    loggingMiddleware,
    localStorageMiddleware,
);