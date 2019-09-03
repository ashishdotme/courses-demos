import { createStore, compose } from 'redux';
import rootReducer from './reducers/index';
import middlewares from './middleware/index';
import { localStorageKey } from '../constant';

const composerFunction = (window as any).__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

const getLocalStorageState = () => {
    const cache = localStorage.getItem(localStorageKey);
    return cache ? JSON.parse(cache) : {};
}

export default createStore(
    rootReducer,
    getLocalStorageState(),
    composerFunction(middlewares),
);