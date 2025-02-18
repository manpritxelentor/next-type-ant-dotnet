import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";

export interface UserState {
    isAuthenticated: boolean
}

const initialState: UserState = {
    isAuthenticated: false
}

export const loginSlice = createSlice({
    name: 'login',
    initialState,
    reducers: {
        loginUser: (state, action: PayloadAction<boolean>) => {
            state.isAuthenticated = action.payload;
        }
    }
})

export const { loginUser } = loginSlice.actions;

export default loginSlice.reducer;