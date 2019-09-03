const loggingMiddleware = (store: any) => (next: any) => (action: any) => {
    console.groupCollapsed(action.type);
    console.log('Action:', action);
    console.log('State - Before:', store.getState());
    const result = next(action);
    console.log('State - After:', store.getState());
    console.groupEnd();
    return result;
};

export default loggingMiddleware;