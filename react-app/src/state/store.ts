import { configureStore } from "@reduxjs/toolkit";
import authorListReducer from "./author/authorListSlice"
import bookReducer from "./book/bookSlice"
import bookListReducer from "./book/bookListSlice"
import filterReducer from "./book/filterSlice"
import signInReducer from "./user/signInSlice"
import signUpReducer from "./user/signUpSlice"
import myBooksReducer from "./bookLoan/myBooksSlice"
import addBookReducer from "./book/addBookSlice"

export const store = configureStore({
    reducer: {
        book: bookReducer,
        bookList: bookListReducer,
        authorList: authorListReducer,
        filter: filterReducer,
        signIn: signInReducer,
        signUp: signUpReducer,
        myBooks: myBooksReducer,
        addBook: addBookReducer
    }
})

export type RootState = ReturnType<typeof store.getState>;
export type AppDispath = typeof store.dispatch;