import { configureStore } from "@reduxjs/toolkit";
import bookReducer from "./book/bookSlice"

export const store = configureStore({
    reducer: {
        book: bookReducer
    }
})

export type RootState = ReturnType<typeof store.getState>;
export type AppDispath = typeof store.dispatch;