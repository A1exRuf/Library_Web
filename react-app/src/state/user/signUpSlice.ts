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

export const register = createAsyncThunk("user/signup", async ({ name, email, password, confirmPassword, role }:
    { name: string, email: string, password: string, confirmPassword: string, role: string }, { rejectWithValue }
) => {
    try {
        const response = await apiClient.post('user/register', {
            name,
            email,
            password,
            confirmPassword,
            role
        });
        return response.data;
    } catch (error: any) {
        return rejectWithValue(error.response?.data?.message || "Registration failed")
    }
});

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
            state.userId = action.payload;
            state.loading = false;
            state.error = undefined;
        })

        builder.addCase(register.rejected, (state, action) => {
            state.loading = false;
            state.error = action.payload as string;
        })
    }
})

export default signUpSlice.reducer