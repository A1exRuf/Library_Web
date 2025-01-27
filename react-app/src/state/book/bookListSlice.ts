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

export const fetchBooks = createAsyncThunk('books/fetchBooks', async ({ searchTerm, authorId, page }: { searchTerm: string; authorId: string[], page: number }) => {
    const params = new URLSearchParams();

    if (searchTerm) {
        params.append('searchTerm', searchTerm);
    }

    authorId.forEach((id) => {
        params.append('authorId', id);
    });

    params.append('page', page.toString());
    params.append('pageSize', '2');

    const response = await apiClient
        .get(`books/books?${params.toString()}`)

    return response.data
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
            state.error = action.error.message
        })
    }
})

export default bookListSlice.reducer