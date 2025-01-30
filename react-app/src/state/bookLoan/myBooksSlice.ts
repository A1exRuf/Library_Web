import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { bookLoan } from "./bookLoan";
import apiClient from "../../api/apiClient";

interface myBooksState {
    items: bookLoan[]
    page: number,
    pageSize: number,
    totalCount: number,
    hasNextPage: boolean,
    hasPreviousPage: boolean,
    loading: boolean,
    error: string | undefined,
}

const initialState: myBooksState = {
    items: [],
    page: 1,
    pageSize: 10,
    totalCount: 0,
    hasNextPage: false,
    hasPreviousPage: false,
    loading: false,
    error: undefined
}

export const fetchMyBooks = createAsyncThunk('myBooks/fetchMyBooks', async ({ userId, page }: { userId: string, page: number }, { rejectWithValue }
) => {
    try {
        const response = await apiClient
            .get('BookLoans/BookLoansByUserId', {
                params: {
                    userId: userId,
                    page: page,
                    pageSize: 10
                }
            })

        return response.data
    } catch (error: any) {
        return rejectWithValue(error.response?.data?.message || "Get book loans failed")
    }
})

const myBooksSlice = createSlice({
    name: "myBooks",
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(fetchMyBooks.pending, (state) => {
            state.loading = true;
            state.error = undefined;
        })
        builder.addCase(fetchMyBooks.fulfilled, (state, action) => {
            state.items = action.payload.items;
            state.page = action.payload.page;
            state.pageSize = action.payload.pageSize;
            state.totalCount = action.payload.totalCount;
            state.hasNextPage = action.payload.hasNextPage;
            state.hasPreviousPage = action.payload.hasPreviousPage;
            state.loading = false;
            state.error = undefined;
        })
        builder.addCase(fetchMyBooks.rejected, (state, action) => {
            state.loading = false;
            state.error = action.payload as string;
        })
    }
})

export default myBooksSlice.reducer