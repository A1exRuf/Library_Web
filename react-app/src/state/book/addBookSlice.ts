import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import apiClient from '../../api/apiClient';

interface addBookState {
    loading: boolean;
    success: boolean;
    error: string | undefined;
}

const initialState: addBookState = {
    loading: false,
    success: false,
    error: undefined,
};

export const addBook = createAsyncThunk(
    'books/addBook',
    async (bookData: FormData, { rejectWithValue }) => {
        try {
            const response = await apiClient.post('books', bookData, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
            });
            return response.data;
        } catch (error: any) {
            return rejectWithValue(error.response?.data?.message || 'Failed to add book');
        }
    }
);

const addBookSlice = createSlice({
    name: 'addBook',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(addBook.pending, (state) => {
            state.loading = true;
            state.success = false;
            state.error = undefined;
        });
        builder.addCase(addBook.fulfilled, (state) => {
            state.loading = false;
            state.success = true;
        });
        builder.addCase(addBook.rejected, (state, action) => {
            state.loading = false;
            state.success = false;
            state.error = action.payload as string;
        });
    },
});

export default addBookSlice.reducer;
