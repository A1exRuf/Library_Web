import { createSlice } from "@reduxjs/toolkit"
import book from "./book"

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
    error: ''
}

const bookSlice = createSlice({
    name: 'book',
    initialState,
    reducers: {}
})

export default bookSlice.reducer