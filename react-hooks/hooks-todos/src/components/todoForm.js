import React, { useState, useContext, useEffect } from 'react'
import TodosContext from '../context'

const TodoForm = () => {
  const [todo, setTodo] = useState('')
  const {
    state: { todos = [], currentTodo = {} },
    dispatch,
  } = useContext(TodosContext)

  useEffect(() => {
    if (currentTodo.text) {
      setTodo(currentTodo.text)
    } else {
      setTodo('')
    }
  }, [currentTodo.id])

  const handleSubmit = (e) => {
    e.preventDefault()
    setTodo('')
    if (currentTodo.text) {
      dispatch({
        type: 'UPDATE_TODO',
        payload: todo,
      })
    } else {
      dispatch({
        type: 'ADD_TODO',
        payload: { text: todo, id: todos.length + 1, complete: false },
      })
    }
  }

  return (
    <form onSubmit={handleSubmit} className="flex justify-center p-5">
      <input
        type="text"
        className="border-black border-solid border-2"
        onChange={(e) => setTodo(e.target.value)}
        value={todo}
      />
    </form>
  )
}

export default TodoForm
