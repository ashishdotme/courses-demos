import React, { useState } from 'react'

const App = () => {
  const [count, setCount] = useState(0)

  const increaseCounter = () => {
    setCount(count + 1)
  }
  return <button onClick={increaseCounter}>I was clicked {count} times</button>
}

export default App
