import React, { createContext, useState, useContext } from 'react';

export const AuthContext = createContext();

export const useAuth = () => {
    return useContext(AuthContext);
};

const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);

    const signIn = (username, password) => {
        // Implement your sign-in logic here
        setUser({ username, role: 'admin' }); // or 'user'
    };

    const signOut = () => {
        setUser(null);
    };

    const register = (username, password) => {
        // Implement your registration logic here
        // For example, you can make an API call to register the user
        console.log('Registered user:', username);
        setUser({ username, role: 'user' }); // or 'admin'
    };

    return (
        <AuthContext.Provider value={{ user, signIn, signOut, register }}>
            {children}
        </AuthContext.Provider>
    );
};

export default AuthProvider;