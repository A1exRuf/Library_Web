import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import apiClient from "../../api/apiClient";

interface bookImageState {
    [key: string]: {
        imageUrl: string | null
        loading: boolean,
        error: string | undefined
    }
}

const initialState: bookImageState = {};

export const fetchBookImage = createAsyncThunk('books/fetchBookImage', async (imageId: string) => {
    const response = await apiClient
        .get('Books/image', {
            responseType: 'blob', params: {
                imageId: imageId
            }
        })

    return { imageId, imageUrl: URL.createObjectURL(response.data) };
})

const bookImageSlice = createSlice({
    name: "bookImage",
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(fetchBookImage.pending, (state, action) => {
            const imageId = action.meta.arg;
            state[imageId] = { loading: true, error: undefined, imageUrl: null }
        })
        builder.addCase(fetchBookImage.fulfilled, (state, action) => {
            const { imageId, imageUrl } = action.payload
            state[imageId] = { loading: false, error: undefined, imageUrl }
        })
        builder.addCase(fetchBookImage.rejected, (state, action) => {
            const imageId = action.meta.arg;
            state[imageId] = { loading: false, error: action.error.message, imageUrl: null };
        })
    }
})

export default bookImageSlice.reducer