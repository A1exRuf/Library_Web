import { configureStore } from "@reduxjs/toolkit";
import bookReducer from "./book/bookSlice"
import bookListReducer from "./book/bookListSlice"
import bookImageReducer from "./book/bookImageSlice"

export const store = configureStore({
    reducer: {
        book: bookReducer,
        bookList: bookListReducer,
        bookImage: bookImageReducer
    }
})

export type RootState = ReturnType<typeof store.getState>;
export type AppDispath = typeof store.dispatch;