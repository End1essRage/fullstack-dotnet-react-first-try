import { configureStore } from "@reduxjs/toolkit";
import todosReducer from "./todoSlice";

export const store = configureStore({
	reducer: {
		//имя: объект редьюсера
		todos: todosReducer
	}
})

export type RootState = ReturnType<typeof store.getState>

export type AppDispatch = typeof store.dispatch