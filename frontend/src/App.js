import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import AuthProvider from './context/AuthContext';
import EmployeeList from './components/EmployeeList';
import DepartmentList from './components/DepartmentList';
import Auth from './components/Auth';
import Register from './components/Register';
import Navigation from './components/Navigation';

const App = () => {
    return (
        <AuthProvider>
            <Router>
                <Navigation />
                <Routes>
                    <Route path="/auth" element={<Auth />} />
                    <Route path="/register" element={<Register />} />
                    <Route path="/employees" element={<EmployeeList />} />
                    <Route path="/departments" element={<DepartmentList />} />
                </Routes>
            </Router>
        </AuthProvider>
    );
};

export default App;