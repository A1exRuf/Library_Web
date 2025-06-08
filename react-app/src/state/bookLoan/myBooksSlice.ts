import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { bookLoan } from "./bookLoan";
import apiClient from "../../api/apiClient";

interface myBooksState {
    items: bookLoan[]
    loading: boolean,
    error: string | undefined,
}

const initialState: myBooksState = {
    items: [],
    loading: false,
    error: undefined
}

// Fetch MyBooks
export const fetchMyBooks = createAsyncThunk('myBooks/fetchMyBooks',
    async (_, { rejectWithValue }) => {
        try {
            const response = await apiClient
                .get('BookLoans')

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
            state.items = action.payload;
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