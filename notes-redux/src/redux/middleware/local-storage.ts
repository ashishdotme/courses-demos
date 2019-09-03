import { localStorageKey } from '../../constant';


const loggingMiddleware = (store: any) => (next: any) => (action: any) => {
    const result = next(action);
    const state = JSON.stringify(store.getState());
    localStorage.setItem(localStorageKey, state);
    return result;
};

export default loggingMiddleware;