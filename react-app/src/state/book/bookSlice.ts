import { createSlice } from "@reduxjs/toolkit"
import book from "./book"

const initialState: book = {
    id: 'id',
    isbn: 'isbn',
    title: 'title',
    genree: 'genre',
    description: 'description',
    authorId: 'author id',
    isAvailable: false,
    imageId: 'image id',
    loading: false,
    error: ''
}

const bookSlice = createSlice({
    name: 'book',
    initialState,
    reducers: {}
})

export default bookSlice.reducer