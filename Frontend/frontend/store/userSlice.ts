import { createSlice } from "@reduxjs/toolkit";
import {jwtDecode} from "jwt-decode";
import type { PayloadAction } from '@reduxjs/toolkit';
import "core-js/stable/atob";

interface JwtPayload 
{
    nameid:string,
    nbf:number,
    exp:number,
    iat:number,
}

export interface UserState 
{
    email: string | null,
    token: string | null,
}

const initialState: UserState = {
    email: null,
    token: null,
};

export const userSlice = createSlice({
    name: 'account',
    initialState,
    reducers: {
        login(state,action:PayloadAction<string>)
        {
            const payload = jwtDecode<JwtPayload>(action.payload);
            state.email = payload.nameid;
            state.token = action.payload;
        },
        logout(state)
        {
            state.email = null;
            state.token = null;
        },
    },
});

export const selectEmail = (state: any) => state.user.email;
export const selectToken = (state:any) => state.user.token;

export const { login, logout } = userSlice.actions;