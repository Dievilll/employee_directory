import React, { createContext, useState, useEffect } from 'react';
import axios from 'axios';

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [user, setUser] = useState(null);

    useEffect(() => {
        checkAuth();
    }, []);

    const checkAuth = async () => {
        try {
                const response = await axios.get('http://localhost:5085/api/auth/check');
                setIsAuthenticated(true);
                setUser(response.data.user);
            }
        catch (err) {
            setIsAuthenticated(false);
            setUser(null);
        }
    };

    const login = async (username, password) => {
        try {
            const response = await axios.post('http://localhost:5085/api/auth/login', { username, password });
            setIsAuthenticated(true);
            setUser(response.data.user);
        } catch (err) {
            setIsAuthenticated(false);
            setUser(null);
            throw err;
        }
    };

    const logout = async () => {
        try {
            await axios.post('http://localhost:5085/api/auth/logout');
            setIsAuthenticated(false);
            setUser(null);
        } catch (err) {
            console.error('Logout failed', err);
        }
    };

    return (
        <AuthContext.Provider value={{ isAuthenticated, user, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};