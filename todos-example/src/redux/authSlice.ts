import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios from "../api/api";
import { API_LOGIN, API_REFRESH, API_REGISTER } from "../constants/apiRoutes";
import { setValue } from "../services/sessionStorage";
import { STATUS_BLANK, STATUS_FULLFILLED, STATUS_PENDING, STATUS_REJECTED } from "../constants/statuses";

interface AuthState {
	status: string,
	error: string,
	authenticated: boolean
}

const initialState: AuthState = {
	status: STATUS_BLANK,
	error: '',
	authenticated: false
}

interface LoginDto {
	userName: string,
	password: string,
}

interface RegisterDto {
	email: string,
	userName: string,
	password: string,
}

export const login = createAsyncThunk(
	'auth/login',
	async (user: LoginDto, { rejectWithValue }) => {
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
	async (user: RegisterDto, { rejectWithValue }) => {
		try {
			const response = await axios.post<string>(API_REGISTER, user);

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

export const refreshToken = createAsyncThunk(
	'auth/refresh-token',
	async (_, { rejectWithValue }) => {
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
		},

		setUnAuthenticated(state) {
			state.authenticated = false;
			state = initialState;
		}
	},
	extraReducers(builder) {
		builder.addCase(login.fulfilled, (state, action) => {
			state.status = STATUS_FULLFILLED;
			state.authenticated = true;
		});
		builder.addCase(register.fulfilled, (state, action) => {
			state.status = STATUS_FULLFILLED;
			state.authenticated = true;
		});
		builder.addCase(refreshToken.fulfilled, (state, action) => {
			state.status = STATUS_FULLFILLED;
			state.authenticated = true;
		});
		builder.addCase(login.pending, (state, action) => {
			state.status = STATUS_PENDING;
		});
		builder.addCase(register.pending, (state, action) => {
			state.status = STATUS_PENDING;
		});
		builder.addCase(refreshToken.pending, (state, action) => {
			state.status = STATUS_PENDING;
		});
		builder.addCase(login.rejected, (state, action) => {
			state.status = STATUS_REJECTED;
			console.log(action.payload);
		});
		builder.addCase(register.rejected, (state, action) => {
			state.status = STATUS_REJECTED;
			console.log(action.payload);
		});
		builder.addCase(refreshToken.rejected, (state, action) => {
			state.status = STATUS_REJECTED;
			console.log(action.payload);
		});
	},
});

export const { setUnAuthenticated } = authSlice.actions;

export default authSlice.reducer;