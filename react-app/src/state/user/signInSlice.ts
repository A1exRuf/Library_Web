import { createAsyncThunk, createSlice } from "@reduxjs/toolkit"
import apiClient from "../../api/apiClient"

interface signInState {
    accessToken: string | null,
    refreshToken: string | null,
    isAuthenticated: boolean,
    loading: boolean,
    error: string | undefined
}

const initialState: signInState = {
    accessToken: localStorage.getItem("accessToken"),
    refreshToken: localStorage.getItem("refreshToken"),
    isAuthenticated: !!localStorage.getItem("accessToken"),
    loading: false,
    error: undefined
}

export const login = createAsyncThunk("user/signin", async ({ email, password }: { email: string, password: string }) => {
    const response = await apiClient
        .post('user/login', { email, password });
    return response.data;
});

const signInSlice = createSlice({
    name: "signIn",
    initialState,
    reducers: {
        logout: (state) => {
            state.accessToken = null;
            state.refreshToken = null;
            state.isAuthenticated = false;

            localStorage.removeItem("accessToken");
            localStorage.removeItem("refreshToken");

            delete apiClient.defaults.headers.common["Authorization"];
        }
    },
    extraReducers: (builder) => {
        builder.addCase(login.pending, (state) => {
            state.loading = true;
            state.error = undefined;
        })

        builder.addCase(login.fulfilled, (state, action) => {
            state.accessToken = action.payload.accessToken;
            state.refreshToken = action.payload.refreshToken;
            state.isAuthenticated = true;
            state.loading = false;
            state.error = undefined;

            localStorage.setItem("accessToken", action.payload.accessToken)
            localStorage.setItem("refreshToken", action.payload.refreshToken)

            apiClient.defaults.headers.common["Authorization"] = `Bearer ${action.payload.accessToken}`
        })

        builder.addCase(login.rejected, (state, action) => {
            state.loading = false;
            state.error = action.error.message;
        })
    }
})

export const { logout } = signInSlice.actions;
export default signInSlice.reducer;
