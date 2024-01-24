import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios from "../api/api";
import { API_LOGIN, API_REFRESH, API_REGISTER } from "../api/routes";
import { setValue } from "../services/sessionStorage";

interface AuthState {
	status: string,
	error: string,
	authenticated: boolean
}

const initialState: AuthState = {
	status: '',
	error: '',
	authenticated: false
}

interface UserDto {
	userName: string;
	password: string;
}

export const login = createAsyncThunk(
	'auth/login',
	async (user: UserDto, { rejectWithValue, dispatch }) => {
		try {
			const response = await axios.post<string>(API_LOGIN, user, { withCredentials: true });

			if (response.status !== 200) {
				throw new Error('Server Error!');
			}

			const data = await response.data;
			setValue('token', data);
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
			const response = await axios.post<string>(API_REGISTER, user);

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

export const refreshToken = createAsyncThunk(
	'auth/refresh-token',
	async (_, { rejectWithValue, dispatch }) => {
		try {
			const response = await axios.post<string>(API_REFRESH, {}, { withCredentials: true });

			if (response.status !== 200) {
				throw new Error('Server Error!');
			}

			const data = await response.data;
			setValue('token', data);
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
		setAuthenticated(state: AuthState) {
			state.authenticated = true;
			state.error = '';
		},

		setUnAuthenticated(state) {
			state.authenticated = false;
			state = initialState;
		}
	},
	extraReducers(builder) {
		builder.addCase(login.fulfilled, (state, action) => {
			state.authenticated = true;
		});
		builder.addCase(register.fulfilled, (state, action) => {
		});
		builder.addCase(refreshToken.fulfilled, (state, action) => {
			state.authenticated = true;
			console.log(action.payload);
		});
		builder.addCase(login.rejected, (state, action) => {
			console.log(action.payload);
		});
		builder.addCase(register.rejected, (state, action) => {
			console.log(action.payload);
		});
		builder.addCase(refreshToken.rejected, (state, action) => {
			console.log(action.payload);
		});
	},
});

export const { setUnAuthenticated } = authSlice.actions;

export default authSlice.reducer;