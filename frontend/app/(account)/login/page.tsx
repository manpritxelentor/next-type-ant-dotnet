'use client'
import { loginUser } from '../../features/account/loginSlice'
import { useSelector, useDispatch } from "react-redux";
import type { RootState } from '../../store'

export default function Login() {
    const isAuthenticated = useSelector((state : RootState) => state.login.isAuthenticated);
    const dispatch = useDispatch();

    return (
        <>
      <button type='button' onClick={() => dispatch(loginUser(!isAuthenticated))}>
        Change Login State
        </button>
      User is {isAuthenticated ? "Logged In" : "Not Logged In"}
        </>
  );
}
