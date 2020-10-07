import React, { useContext, useReducer } from 'react'
import TodosList from './components/todosList'
import TodoForm from './components/todoForm'
import todosReducer from './reducer'
import TodosContext from './context'

function App() {
  const initialState = useContext(TodosContext)
  const [state, dispatch] = useReducer(todosReducer, initialState)
  return (
    <TodosContext.Provider value={{ state, dispatch }}>
      <TodoForm />
      <TodosList />
    </TodosContext.Provider>
  )
}

export default App
