import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";
import { LoginModel } from "@/app/models/loginModel";
import { loginUserOnServer } from "@/app/services/accountService";
import type { AppDispatch } from "@/app/store";

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
        setLoginState: (state, action: PayloadAction<boolean>) => {
            state.isAuthenticated = action.payload;
        }
    }
})


export const loginActions = loginSlice.actions;

export const loginUser = (loginModel: LoginModel) =>{
    return async (dispatch: AppDispatch) =>{ 
        try{
            const response = await loginUserOnServer(loginModel);
            localStorage.setItem('token', response.data.access_token);
            dispatch(loginActions.setLoginState(true))
        } catch(error){
            dispatch(loginActions.setLoginState(false))
            console.log(error);
        }
    }
}

export default loginSlice.reducer;