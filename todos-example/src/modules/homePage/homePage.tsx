import React, { ChangeEvent, useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { createTodo, fetchTodos } from '../../redux/todoSlice';
import { TodosList } from '../todosList';
import { AppDispatch } from '../../redux/store';
import styles from './homePage.module.css'

export const HomePage = () => {
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
		<div className={styles.wrapper}>
			<div className={styles.body}>
				<div className={styles.adding}>
					<input type='text' onChange={updateTodo} value={todo}></input>
					<button onClick={onAddTodoClick}>Add Todo</button>
				</div>
				<TodosList />
			</div>
		</div>
	);
}