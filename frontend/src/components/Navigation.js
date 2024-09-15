import React from 'react';
import { Link } from 'react-router-dom';

const Navigation = () => {
    return (
        <nav>
            <ul style={{ display: 'flex', gap: '10px' }}>
                <li><Link to="/auth">Sign In</Link></li>
                <li><Link to="/register">Register</Link></li>
                <li><Link to="/employees">Employees</Link></li>
                <li><Link to="/departments">Departments</Link></li>
            </ul>
        </nav>
    );
};

export default Navigation;