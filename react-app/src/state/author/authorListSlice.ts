import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import author from "./author";
import apiClient from "../../api/apiClient";

interface authorListState {
    loading: boolean,
    error: string | undefined,
    items: author[],
}

const initialState: authorListState = {
    loading: true,
    error: undefined,
    items: []
}

export const fetchAuthors = createAsyncThunk('authors/fetchAuthors', async () => {
    const response = await apiClient
        .get('authors')

    return response.data
})

const authorListSlice = createSlice({
    name: "authorList",
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(fetchAuthors.pending, (state) => {
            state.loading = true
        })
        builder.addCase(fetchAuthors.fulfilled, (state, action) => {
            state.loading = false
            state.error = ''
            state.items = action.payload.map((a: author) => ({
                ...a,
                dateOfBirth: new Date(a.dateOfBirth).toISOString()
            }))
        })
        builder.addCase(fetchAuthors.rejected, (state, action) => {
            state.loading = false
            state.error = action.error.message
        })
    }
})

export default authorListSlice.reducer