import { PayloadAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit"
import axios from "../api/api";
import { API_TODOS } from "../api/routes";
import { loadValue } from "../services/sessionStorage";

interface TodoState {
	todos: Todo[],
	status: string,
	error: string
}

class Todo {
	constructor(public title: string, public completed: boolean) { }
	public readonly id: number = 0;
}

export const fetchTodos = createAsyncThunk(
	'todos/fetchTodos',
	async (_, { rejectWithValue }) => {
		try {
			const response = await axios(API_TODOS);

			if (response.status !== 200) {
				throw new Error('Server Error!');
			}

			const data = await response.data;

			return data;
		}
		catch (error) {
			let message = '';

			if (error instanceof Error)
				message = error.message;

			return rejectWithValue(message);
		}
	}
);

export const toggleComplete = createAsyncThunk(
	'todos/toggleComplete',
	async (id: number, { rejectWithValue, dispatch }) => {
		try {
			const response = await axios.patch(API_TODOS, undefined, {
				headers: {
					'Authorization': 'Bearer ' + loadValue('token'),
				},
			});

			if (response.status !== 200) {
				throw new Error('Server Error!');
			}

			dispatch(toggle(id));
		}
		catch (error) {
			let message = 'Server Error';

			if (error instanceof Error)
				message = error.message;

			return rejectWithValue(message);
		}
	}
);

export const deleteTodo = createAsyncThunk(
	'todos/deleteTodo',
	async (id: number, { rejectWithValue, dispatch }) => {
		try {
			const response = await axios.delete(API_TODOS + `?id=${id}`, {
				headers: {
					'Authorization': 'Bearer ' + loadValue('token'),
				},
			});

			if (response.status !== 200) {
				throw new Error('Server Error!');
			}

			dispatch(remove(id));
		}
		catch (error) {
			let message = 'Server Error';

			if (error instanceof Error)
				message = error.message;

			return rejectWithValue(message);
		}
	}
);

export const createTodo = createAsyncThunk(
	'todos/createTodo',
	async (title: string, { rejectWithValue, dispatch }) => {
		try {
			const response = await axios.post(API_TODOS, JSON.stringify({ title }), {
				headers: {
					'Content-Type': 'application/json',
					'Authorization': 'Bearer ' + loadValue('token'),
				},
			});

			if (response.status !== 200) {
				throw new Error('Server Error!');
			}

			const data: Todo = await response.data;

			dispatch(add(data));
		}
		catch (error) {
			let message = 'Server Error';

			if (error instanceof Error)
				message = error.message;

			return rejectWithValue(message);
		}
	}
);

const initialState: TodoState = {
	todos: [],
	status: '',
	error: ''
}

export const todosSliсe = createSlice({
	name: 'todos',
	initialState,
	reducers: {
		add: (state, action: PayloadAction<Todo>) => {
			state.todos.push(action.payload);
		},
		toggle: (state, action: PayloadAction<number>) => {
			const item = state.todos.find(c => c.id === action.payload);
			if (item !== undefined)
				item.completed = !item.completed;
		},
		remove: (state, action: PayloadAction<number>) => {
			state.todos = state.todos.filter(todo => todo.id !== action.payload);
		}
	},
	extraReducers(builder) {
		builder.addCase(fetchTodos.pending, (state, action) => {
			state.status = 'loading';
			state.error = '';
		});
		builder.addCase(fetchTodos.fulfilled, (state, action) => {
			state.status = 'loaded'
			state.todos = action.payload;
			state.error = '';
		})
		builder.addCase(fetchTodos.rejected, (state, action) => {
			console.log(action.payload);
		})
		builder.addCase(toggleComplete.rejected, (state, action) => {
			console.log('toggle error');
		})
		builder.addCase(deleteTodo.rejected, (state, action) => {
			console.log('delete error');
		})
		builder.addCase(createTodo.rejected, (state, action) => {
			console.log('add error');
		})
	},
});

const { add, toggle, remove } = todosSliсe.actions;

export default todosSliсe.reducer;