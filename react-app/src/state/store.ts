import { configureStore } from "@reduxjs/toolkit";
import authorListReducer from "./author/authorListSlice"
import bookReducer from "./book/bookSlice"
import bookListReducer from "./book/bookListSlice"
import filterReducer from "./book/filterSlice"
import signInReducer from "./user/signInSlice"

export const store = configureStore({
    reducer: {
        book: bookReducer,
        bookList: bookListReducer,
        authorList: authorListReducer,
        filter: filterReducer,
        signIn: signInReducer
    }
})

export type RootState = ReturnType<typeof store.getState>;
export type AppDispath = typeof store.dispatch;