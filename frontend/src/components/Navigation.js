import React from 'react';
import { Link } from 'react-router-dom';

const Navigation = () => {
    return (
        <nav style={{ display: 'flex', justifyContent: 'center' }}>
            <ul style={{ display: 'flex', listStyle: 'none', padding: 0 }}>
                <li style={{ margin: '0 10px', padding: '10px 20px', border: '1px solid #ccc', borderRadius: '5px' }}>
                    <Link to="/auth" style={{ textDecoration: 'none', color: 'inherit' }}>Sign In</Link>
                </li>
                <li style={{ margin: '0 10px', padding: '10px 20px', border: '1px solid #ccc', borderRadius: '5px' }}>
                    <Link to="/register" style={{ textDecoration: 'none', color: 'rgb(82, 160, 0)', fontFamily: 'Arial', fontWeight: 'bold' }}>Register</Link>
                </li>
                <li style={{ margin: '0 10px', padding: '10px 20px', border: '1px solid #ccc', borderRadius: '5px' }}>
                    <Link to="/employees" style={{ textDecoration: 'none', color: 'inherit' }}>Employees</Link>
                </li>
                <li style={{ margin: '0 10px', padding: '10px 20px', border: '1px solid #ccc', borderRadius: '5px' }}>
                    <Link to="/departments" style={{ textDecoration: 'none', color: 'inherit' }}>Departments</Link>
                </li>
            </ul>
        </nav>
    );
};

export default Navigation;