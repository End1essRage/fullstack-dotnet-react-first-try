import { useSelector } from "react-redux";
import { RootState } from "../redux/store";
import { TodoItem } from "./todoItem";

export const TodosList = () => {

	var items = useSelector((state: RootState) => state.todos.todos);

	return (
		<>
			<ul>
				{items.map((el) => <TodoItem id={el.id} completed={el.completed} title={el.title} />)}
			</ul>
		</>
	);
}