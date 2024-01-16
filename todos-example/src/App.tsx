import React, { ChangeEvent, useEffect } from 'react';
import logo from './logo.svg';
import './App.css';
import { useDispatch, useSelector } from 'react-redux';
import { AppDispatch, RootState } from './redux/store';
import { createTodo, fetchTodos } from './redux/todoSlice';
import { TodosList } from './modules/todosList';

function App() {

  const [todo, setTodo] = React.useState('');
  const dispatch = useDispatch<AppDispatch>();


  const updateTodo = (event: ChangeEvent<HTMLInputElement>) => {
    setTodo(event.target.value);
  }

  const onAddTodoClick = () => {
    if (todo.trim() !== '')
      dispatch(createTodo(todo));
    setTodo('');
  }

  useEffect(() => {
    dispatch(fetchTodos());
  }, [dispatch]);

  return (
    <div className="App">
      <header className="App-header">
        <div className='input'>
          <input type='text' onChange={updateTodo} value={todo}></input>
          <button onClick={onAddTodoClick}>Add Todo</button>
        </div>
        <TodosList />
      </header>
    </div>
  );
}

export default App;

