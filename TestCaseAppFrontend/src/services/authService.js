import http from "../utils/httpClient";

const AUTH_USER = "auth_user"

const login = (data) => {
    return http.post('/auth/login', data, {
        transformResponse: [(result) => {
            const parsed = JSON.parse(result);
            localStorage.setItem(AUTH_USER, JSON.stringify(parsed));
            return parsed;
        }]
    });
}

const register = (data) => {
    return http.post('/auth/register', data, {
        transformResponse: [(result) => {
            const parsed = JSON.parse(result);
            localStorage.setItem(AUTH_USER, JSON.stringify(parsed));
            return parsed;
        }]
    });
}

const profile = () => {
    return http.get('/usersprofile');
}

const updateProfile = (data) => {
    return http.post('/usersprofile', data);
}

const logout = () => {
    localStorage.removeItem(AUTH_USER);
}

const getAuthUser = () => {
    return JSON.parse(localStorage.getItem(AUTH_USER));
}  

const methods = { 
    login,
    register,
    profile,
    logout,
    getAuthUser,
    updateProfile
}

export default methods;