import { useDispatch } from "react-redux";
import { deleteTodo, toggleComplete } from "../../redux/todosSlice";
import { AppDispatch } from "../../redux/store";

interface TodoProps {
	id: number,
	completed: boolean,
	title: string,
}

export const TodoItem = (props: TodoProps) => {
	const dispatch = useDispatch<AppDispatch>();

	return (
		<li>
			<input
				type='checkbox'
				checked={props.completed}
				onChange={() => dispatch(toggleComplete(props.id))}
			/>
			<span>{props.title}</span>
			<span onClick={() => dispatch(deleteTodo(props.id))}>&times;</span>
		</li>
	);
}