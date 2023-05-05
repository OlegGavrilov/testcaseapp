import React from 'react';
import { Route, Routes } from 'react-router-dom';
import AuthGuard from './services/authGuard';
import LoginForm from './components/loginForm';
import RegisterForm from './components/registerForm';
import NotFound from './components/notFound';
import Profile from './components/profile';

const Router = () => {
    return (
        <Routes>
            <Route exact path='/' element={<LoginForm />} />
            <Route path='/register' element={<RegisterForm />} />
            <Route element={<AuthGuard />}>
                <Route path='/profile' element={<Profile />} />
            </Route>
            <Route path='*' element={<NotFound />} />
        </Routes>
    )
}

export default Router;