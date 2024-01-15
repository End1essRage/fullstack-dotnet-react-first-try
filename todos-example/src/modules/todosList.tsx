import { useSelector } from "react-redux";
import { RootState } from "../redux/store";

export const TodosList = () => {

	var items = useSelector((state: RootState) => state.todos.todos);

	return (
		<div className='todos'>
			<ul>
				{items.map((el) => <li>{el.text}</li>)}
			</ul>
		</div>
	);
}