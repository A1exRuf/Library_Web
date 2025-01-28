import { createSlice, PayloadAction } from "@reduxjs/toolkit";

interface filterState {
    genres: string[];
    authorIds: string[];
}

const initialState: filterState = {
    genres: [],
    authorIds: [],
};

const filterSlice = createSlice({
    name: "filter",
    initialState,
    reducers: {
        setGenre: (state, action: PayloadAction<string>) => {
            const genre = action.payload;
            if (state.genres.includes(genre)) {
                state.genres = state.genres.filter((g) => g !== genre);
            } else {
                state.genres.push(genre);
            }
        },

        setAuthorId: (state, action: PayloadAction<string>) => {
            const authorId = action.payload;
            if (state.authorIds.includes(authorId)) {
                state.authorIds = state.authorIds.filter((id) => id !== authorId);
            } else {
                state.authorIds.push(authorId);
            }
        },
    },
});

export const { setGenre, setAuthorId } = filterSlice.actions

export default filterSlice.reducer
