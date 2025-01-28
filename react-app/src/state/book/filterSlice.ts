import { createSlice, PayloadAction } from "@reduxjs/toolkit";

interface filterState {
    genres: string[];
    authorIds: string[];
    showUnavailable: boolean;
}

const initialState: filterState = {
    genres: [],
    authorIds: [],
    showUnavailable: false
};

const filterSlice = createSlice({
    name: "filter",
    initialState,
    reducers: {
        setAuthorId: (state, action: PayloadAction<string>) => {
            const authorId = action.payload;
            if (state.authorIds.includes(authorId)) {
                state.authorIds = state.authorIds.filter((id) => id !== authorId);
            } else {
                state.authorIds.push(authorId);
            }
        },

        setGenre: (state, action: PayloadAction<string>) => {
            const genre = action.payload;
            if (state.genres.includes(genre)) {
                state.genres = state.genres.filter((g) => g !== genre);
            } else {
                state.genres.push(genre);
            }
        },

        setAvailability: (state) => {
            state.showUnavailable = !state.showUnavailable
        }
    },
});

export const { setAuthorId, setGenre, setAvailability } = filterSlice.actions

export default filterSlice.reducer
