'use client'
import { loginUser } from '../../features/account/loginSlice'
import { useDispatch } from "react-redux";
// import type { RootState } from '../../store'
import { LoginModel } from '@/app/models/loginModel';
import type { AppDispatch } from '@/app/store';

export default function Login() {
    // const isAuthenticated = useSelector((state : RootState) => state.login.isAuthenticated);
     const dispatch = useDispatch<AppDispatch>();

    const loginData : LoginModel = {
      userName: 'manprit',
      password: 'Admin@123'
    };

    return (
       <>
        <button type='button' onClick={() => dispatch(loginUser(loginData))}>Login User</button>
       </>
  );
}
