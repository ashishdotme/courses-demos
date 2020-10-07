import React, { useContext } from 'react'
import TodosContext from '../context'

const TodosList = () => {
  const { state, dispatch } = useContext(TodosContext)
  const title = state.todos.length > 0 ? `${state.todos.length} Todos` : 'Nothting to do'
  return (
    <div className="container mx-auto max-w-md text-center font-mono">
      <h1 className="text-bold text-3xl my-5">{title}</h1>
      <ul className="list-reset text-white p-0">
        {state &&
          state.todos.map((t, i) => (
            <li className="bg-orange-500 border-black border-dashed border-2 my-2 py-2 flex items-center">
              <span
                onDoubleClick={() => dispatch({ type: 'TOGGLE_TODO', payload: t })}
                className={`cursor-pointer flex-1 ml-12 ${
                  t.complete && 'line-through text-gray-800'
                }`}
              >
                {t.text}
              </span>
              <button onClick={() => dispatch({ type: 'CHANGE_CURRENT_TODO', payload: t })}>
                <img
                  src="https://microicon-clone.vercel.app/edit/0050c5"
                  alt="Edit"
                  className="h-6"
                />
              </button>
              <button onClick={() => dispatch({ type: 'DELETE_TODO', payload: t })}>
                <img
                  src="https://microicon-clone.vercel.app/delete/8b0000"
                  alt="Delete"
                  className="h-6"
                />
              </button>
            </li>
          ))}
      </ul>
    </div>
  )
}

export default TodosList
