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
        dateOfBirth: new Date,
        country: ''
    },
    isAvailable: false,
    imageUrl: '',
    loading: false,
    error: undefined
}

export const fetchBook = createAsyncThunk('books/fetchBook', async (bookId: string) => {

    const response = await apiClient
        .get('books/book', {
            params: {
                bookId: bookId
            }
        })

    return response.data
})

const bookSlice = createSlice({
    name: 'book',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(fetchBook.pending, (state) => {
            state.loading = true
        })
        builder.addCase(fetchBook.fulfilled, (state, action) => {
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
            state.error = action.error.message
        })
    }
})

export default bookSlice.reducer