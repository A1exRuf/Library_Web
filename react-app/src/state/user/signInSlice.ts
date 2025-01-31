import { createAsyncThunk, createSlice } from "@reduxjs/toolkit"
import apiClient from "../../api/apiClient"
import Cookies from "js-cookie"

var inOneMinute = new Date(new Date().getTime() + 60 * 1000);

interface signInState {
    accessToken: string | null,
    refreshToken: string | null,
    isAuthenticated: boolean,
    loading: boolean,
    error: string | undefined,
}

const initialState: signInState = {
    accessToken: Cookies.get("accessToken") || null,
    refreshToken: Cookies.get("refreshToken") || null,
    isAuthenticated: !!Cookies.get("refreshToken"),
    loading: false,
    error: undefined,
}

export const login = createAsyncThunk("user/signin", async ({ email, password }: { email: string, password: string }, { rejectWithValue }) => {
    try {
        const response = await apiClient
            .post('user/login', { email, password });
        return response.data;
    } catch (error: any) {
        return rejectWithValue(error.response?.data?.message || "Authentication failed")
    }

});

const signInSlice = createSlice({
    name: "signIn",
    initialState,
    reducers: {
        logout: (state) => {
            state.accessToken = null;
            state.refreshToken = null;
            state.isAuthenticated = false;

            Cookies.remove("accessToken");
            Cookies.remove("refreshToken");

            delete apiClient.defaults.headers.common["Authorization"];
        },
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

            Cookies.set("accessToken", action.payload.accessToken, { expires: inOneMinute })
            Cookies.set("refreshToken", action.payload.refreshToken, { expires: 7 })

            apiClient.defaults.headers.common["Authorization"] = `Bearer ${action.payload.accessToken}`
        })

        builder.addCase(login.rejected, (state, action) => {
            state.loading = false;
            state.error = action.payload as string;
        })
    }
})

export const { logout } = signInSlice.actions;
export default signInSlice.reducer;
