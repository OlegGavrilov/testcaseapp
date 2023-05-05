import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import authService from './authService';

const AuthGuard = () => {
    const authUser = authService.getAuthUser();
    console.log(authUser);
    return authUser ? <Outlet /> : <Navigate to={'/'} replace />
}

export default AuthGuard