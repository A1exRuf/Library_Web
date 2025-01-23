import { createSlice } from "@reduxjs/toolkit"

interface BookState {
    id: string,
    isbn: string,
    title: string,
    genree: string,
    description: string,
    authorId: string,
    isAvailable: boolean,
    imageId: string | null
}

const initialState:  BookState = {
    id: 'id',
    isbn: 'isbn',
    title: 'title',
    genree: 'genre',
    description: 'description',
    authorId: 'author id',
    isAvailable: false,
    imageId:  'image id'
}

const bookSlice = createSlice({
    name: 'book',
    initialState,
    reducers: {}
})

export default bookSlice.reducer