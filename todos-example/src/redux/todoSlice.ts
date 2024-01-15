import { Action, PayloadAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit"
import { FETCH_TODOS } from "../routes";

interface TodoState {
	todos: Todo[],
	status: string,
	error: string
}

class Todo {
	constructor(public text: string) { }
	public readonly id: number = 0;
}

export const fetchTodos = createAsyncThunk(
	'todos/fetchTodos',
	async () => {
		const responce = await fetch(FETCH_TODOS);

		const data = await responce.json();

		return data;
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
		add: (state, action: PayloadAction<string>) => {
			state.todos.push(new Todo(action.payload));
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
			state.status = 'error'
			state.error = 'error text';
		})
	},
});

export const { add } = todosSliсe.actions;

export default todosSliсe.reducer;