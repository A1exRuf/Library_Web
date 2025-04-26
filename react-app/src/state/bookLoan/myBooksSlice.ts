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

// Fetch MyBooks
export const fetchMyBooks = createAsyncThunk('myBooks/fetchMyBooks',
    async (page: number, { rejectWithValue }) => {
        try {
            const response = await apiClient
                .get('BookLoans', {
                    params: {
                        page: page,
                        pageSize: 10
                    }
                })

            return response.data
        } catch (error: any) {
            return rejectWithValue(error.response?.data?.message || "Get book loans failed")
        }
    })

// Take Book
export const takeBook = createAsyncThunk('myBooks/takebook',
    async ({ bookId, userId }: { bookId: string, userId: string }, { rejectWithValue }) => {
        try {
            const response = await apiClient.post(`BookLoans`, { bookId: bookId, userId: userId })
            return response.data
        } catch (error: any) {
            return rejectWithValue(error.response?.data?.message || "Take book failed")
        }
    })

// Return Book
export const returnBook = createAsyncThunk('myBooks/returnBook',
    async (bookLoanId: string, { rejectWithValue }) => {
        try {
            await apiClient.delete(`BookLoans/${bookLoanId}`)
            return bookLoanId
        } catch (error: any) {
            return rejectWithValue(error.response?.data?.message || "Return book failed")
        }
    })

const myBooksSlice = createSlice({
    name: "myBooks",
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        // Fetch MyBooks
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

        // Take Book
        builder.addCase(takeBook.pending, (state) => {
            state.loading = true;
            state.error = undefined;
        })
        builder.addCase(takeBook.fulfilled, (state) => {
            state.loading = false;
            state.error = undefined;
        });
        builder.addCase(takeBook.rejected, (state, action) => {
            state.loading = false;
            state.error = action.payload as string;
        })

        // Return Book
        builder.addCase(returnBook.pending, (state) => {
            state.loading = true;
            state.error = undefined;
        })
        builder.addCase(returnBook.fulfilled, (state, action) => {
            state.loading = false;
            state.error = undefined;
            state.items = state.items.filter(item => item.id !== action.payload);
        });
        builder.addCase(returnBook.rejected, (state, action) => {
            state.loading = false;
            state.error = action.payload as string;
        })
    }
})

export default myBooksSlice.reducer