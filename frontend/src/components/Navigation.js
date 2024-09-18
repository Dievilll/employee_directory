import React, { useContext } from 'react';
import { Link } from 'react-router-dom';
import { AuthContext } from './AuthContext';

const Navigation = () => {
    const { isAuthenticated } = useContext(AuthContext);
    return (
        <nav style={{ display: 'flex', justifyContent: 'center' }}>
            <ul style={{ display: 'flex', listStyle: 'none', padding: 0 }}>
            {!isAuthenticated ? (
                <>
                    <li style={{ margin: '0 10px', padding: '10px 20px', border: '1px solid #ccc', borderRadius: '5px' }}>
                        <Link to="/auth" style={{ textDecoration: 'none', color: 'inherit' }}>Авторизация</Link>
                    </li>
                </>
            ) : (
                <li style={{ margin: '0 10px', padding: '10px 20px', border: '1px solid #ccc', borderRadius: '5px' }} ><Link to="/auth">Выйти</Link></li>
            )}
                <li style={{ margin: '0 10px', padding: '10px 20px', border: '1px solid #ccc', borderRadius: '5px' }}>
                    <Link to="/employees" style={{ textDecoration: 'none', color: 'rgb(82, 160, 0)', fontWeight: 'bold' }}>Страница сотрудников</Link>
                </li>
                <li style={{ margin: '0 10px', padding: '10px 20px', border: '1px solid #ccc', borderRadius: '5px' }}>
                    <Link to="/departments" style={{ textDecoration: 'none', color: 'rgb(82, 160, 0)', fontWeight: 'bold' }}>Страница отделов</Link>
                </li>
            </ul>
        </nav>
    );
};

export default Navigation;