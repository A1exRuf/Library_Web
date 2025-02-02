import { createAsyncThunk, createSlice } from "@reduxjs/toolkit"
import book from "./book"
import apiClient from "../../api/apiClient";

const initialState: book = {
    id: 'id',
    isbn: 'isbn',
    title: 'title',
    genree: 'genre',
    description: 'description',
    author: {
        id: '',
        firstName: '',
        lastName: '',
        dateOfBirth: '',
        country: ''
    },
    isAvailable: false,
    imageUrl: '',
    loading: false,
    error: undefined
}

export const fetchBook = createAsyncThunk('books/fetchBook',
    async (bookId: string, { rejectWithValue }) => {
        try {
            const response = await apiClient
                .get('books/book', {
                    params: {
                        bookId: bookId
                    }
                })
            return response.data
        } catch (error: any) {
            return rejectWithValue(error.response?.data?.message || "Get book failed")
        }
    })

export const updateBook = createAsyncThunk(
    'books/updateBook',
    async ({ bookData }: { bookData: FormData }, { rejectWithValue }) => {
        try {
            const response = await apiClient.put(`books`, bookData, {
                headers: { 'Content-Type': 'multipart/form-data' },
            });
            return response.data;
        } catch (error: any) {
            return rejectWithValue(error.response?.data?.message || "Update book failed");
        }
    }
);

export const deleteBook = createAsyncThunk('books/delete',
    async (bookId: string, { rejectWithValue }) => {
        try {
            await apiClient.delete(`books/${bookId}`);
            return bookId;
        } catch (error: any) {
            return rejectWithValue(error.response?.data?.message || "Delete book failed");
        }
    })

const bookSlice = createSlice({
    name: 'book',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        //fetchBook
        builder.addCase(fetchBook.pending, (state) => {
            state.loading = true
        })
        builder.addCase(fetchBook.fulfilled, (state, action) => {
            state.id = action.payload.id
            state.isbn = action.payload.isbn
            state.title = action.payload.title
            state.genree = action.payload.genree
            state.description = action.payload.description
            state.author = action.payload.author
            state.isAvailable = action.payload.isAvailable
            state.imageUrl = action.payload.imageUrl
            state.loading = false
            state.error = undefined
        })
        builder.addCase(fetchBook.rejected, (state, action) => {
            state.loading = false
            state.error = action.payload as string;
        })
        //updateBook
        builder.addCase(updateBook.pending, (state) => {
            state.loading = true;
        });
        builder.addCase(updateBook.fulfilled, (state) => {
            state.loading = false;
            state.error = undefined;
        });
        builder.addCase(updateBook.rejected, (state, action) => {
            state.loading = false;
            state.error = action.payload as string;
        });
        //deleteBook
        builder.addCase(deleteBook.pending, (state) => {
            state.loading = true;
        })
        builder.addCase(deleteBook.fulfilled, (state) => {
            state = initialState;
        })
        builder.addCase(deleteBook.rejected, (state, action) => {
            state.loading = false;
            state.error = action.payload as string;
        })
    }
})

export default bookSlice.reducer