export default function reducer(state, action) {
  switch (action.type) {
    case 'TOGGLE_TODO':
      const toggledTodos = state.todos.map((t) =>
        t.id === action.payload.id ? { ...t, complete: !action.payload.complete } : t,
      )
      return {
        ...state,
        todos: toggledTodos,
      }
    case 'DELETE_TODO':
      const filteredTodos = state.todos.filter((t) => t.id !== action.payload.id)
      const isCurrentTodoDeleted =
        action.payload.id === state.currentTodo.id ? {} : state.currentTodo
      return {
        ...state,
        currentTodo: isCurrentTodoDeleted,
        todos: filteredTodos,
      }
    case 'CHANGE_CURRENT_TODO':
      return {
        ...state,
        currentTodo: action.payload,
      }
    case 'UPDATE_TODO':
      if (!action.payload) {
        return state
      }
      if (state.todos.findIndex((t) => t.text === action.payload) > -1) {
        return state
      }
      const updatedTodo = { ...state.currentTodo, text: action.payload }
      const updatedTodoIndex = state.todos.findIndex((t) => t.id === state.currentTodo.id)
      return {
        ...state,
        currentTodo: {},
        todos: [
          ...state.todos.slice(0, updatedTodoIndex),
          updatedTodo,
          ...state.todos.slice(updatedTodoIndex + 1),
        ],
      }
    case 'ADD_TODO':
      if (!action.payload.text) {
        return state
      }
      if (state.todos.findIndex((t) => t.text === action.payload.text) > -1) {
        return state
      }
      const newTodos = [...state.todos, action.payload]
      return {
        ...state,
        todos: newTodos,
      }

    default:
      return state
  }
}
