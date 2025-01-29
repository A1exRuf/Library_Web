import { createAsyncThunk, createSlice } from "@reduxjs/toolkit"
import apiClient from "../../api/apiClient";

interface signUpState {
    userId: string | undefined,
    loading: boolean,
    error: string | undefined
}

const initialState: signUpState = {
    userId: undefined,
    loading: false,
    error: undefined
}

export const register = createAsyncThunk(
    "user/register",
    async (userData, { rejectWithValue }) => {
        try {
            const response = await apiClient.post("/api/register", userData);
            return response.data;
        } catch (error) {
            return rejectWithValue(error.response?.data); // Передаем response.data
        }
    }
);

const signUpSlice = createSlice({
    name: "signUp",
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(register.pending, (state) => {
            state.loading = true;
            state.error = undefined;
        })

        builder.addCase(register.fulfilled, (state, action) => {
            state.userId = action.payload.userIdl;
            state.loading = false;
            state.error = undefined;
        })

        builder.addCase(register.rejected, (state, action) => {
            state.loading = false;
            state.error = action.error.message;
        })
    }
})

export default signUpSlice.reducer