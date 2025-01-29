import { createAsyncThunk, createSlice } from "@reduxjs/toolkit"
import book from "./book"
import apiClient from "../../api/apiClient"

interface bookListState {
    loading: boolean,
    error: string | undefined,
    items: book[],
    page: number,
    pageSize: number,
    totalCount: number,
    hasNextPage: boolean,
    hasPreviousPage: boolean
}

const initialState: bookListState = {
    loading: false,
    error: '',
    items: [],
    page: 1,
    pageSize: 10,
    totalCount: 0,
    hasNextPage: false,
    hasPreviousPage: false,
}

export const fetchBooks = createAsyncThunk('books/fetchBooks', async (
    { searchTerm, authorId, genre, showUnavailable, page }:
        { searchTerm: string; authorId: string[], genre: string[], showUnavailable: boolean, page: number },
    { rejectWithValue }) => {
    const params = new URLSearchParams();

    if (searchTerm) {
        params.append('searchTerm', searchTerm);
    }

    authorId.forEach((id) => {
        params.append('authorId', id);
    });

    genre.forEach((g) => {
        params.append('genre', g);
    });

    params.append('page', page.toString());

    if (showUnavailable) {
        params.append('showUnavailable', 'true');
    }

    params.append('pageSize', '10');

    try {
        const response = await apiClient
            .get(`books/books?${params.toString()}`)

        return response.data
    } catch (error: any) {
        return rejectWithValue(error.response?.data?.message || "Get books failed")
    }
})

const bookListSlice = createSlice({
    name: "bookList",
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(fetchBooks.pending, (state) => {
            state.loading = true
        })
        builder.addCase(fetchBooks.fulfilled, (state, action) => {
            state.loading = false
            state.items = action.payload.items
            state.page = action.payload.page
            state.pageSize = action.payload.pageSize
            state.totalCount = action.payload.totalCount
            state.hasNextPage = action.payload.hasNextPage
            state.hasPreviousPage = action.payload.hasPreviousPage
            state.error = ''
        })
        builder.addCase(fetchBooks.rejected, (state, action) => {
            state.loading = false
            state.error = action.payload as string
        })
    }
})

export default bookListSlice.reducer