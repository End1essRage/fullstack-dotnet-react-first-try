import { PayloadAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios from "axios";
import inMemoryJWT from "../services/inMemoryJWTService";
import { User } from "../types";
import { AxiosResponse } from "axios";

interface AuthState {
	user: User | undefined,
	status: string,
	error: string
}

const initialState: AuthState = {
	user: undefined,
	status: '',
	error: ''
}

interface UserDto {
	userName: string;
	password: string;
}

axios.interceptors.request.use((config) => {
	console.log('Request data:', config.data);
	return config;
});

export const login = createAsyncThunk(
	'auth/login',
	async (user: UserDto, { rejectWithValue, dispatch }) => {
		try {
			const response = await axios.post<string>('/auth/login', user);

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

export const register = createAsyncThunk(
	'auth/register',
	async (user: UserDto, { rejectWithValue, dispatch }) => {
		try {
			const response = await axios.post<string>('/auth/register', user);

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
)

export const authSlice = createSlice({
	name: 'auth',
	initialState,
	reducers: {
		setAuthenticated(state: AuthState, action: PayloadAction<User>) {
			console.log('5');
			state.status = 'authenticated';
			state.user = action.payload;
			state.error = '';
		},

		setUnAuthenticated(state) {
			state.status = 'unauthenticated';
			state = initialState;
		}
	},
	extraReducers(builder) {
		builder.addCase(login.fulfilled, (state, action) => {
			state.status = 'authenticated';
			inMemoryJWT.setToken(action.payload);
			console.log(action.payload);
		});
		builder.addCase(register.fulfilled, (state, action) => {
			state.status = 'authenticated';
			inMemoryJWT.setToken(action.payload);
		});
		builder.addCase(login.rejected, (state, action) => {
			console.log(action.payload);
		});
		builder.addCase(register.rejected, (state, action) => {
			console.log(action.payload);
		});
	},
});

const { setAuthenticated, setUnAuthenticated } = authSlice.actions;

export default authSlice.reducer;