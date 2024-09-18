import React, { useState, useContext } from 'react';
import { AuthContext } from './AuthContext';
import { useNavigate } from 'react-router-dom';
import { Button } from '@mui/material';

const Auth = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const { login } = useContext(AuthContext);
    const navigate = useNavigate();

    const handleLogin = async () => {
        try {
            await login(username, password);
            navigate('/employees');
        } catch (err) {
            setError('Неверное имя пользователя или пароль');
        }
    };

    return (
        
        <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', justifyContent: 'center', height: '80vh' }}>
            <h1 style={{ marginBottom: '5%' }}>Авторизация</h1>
            <input style={{ border: '1px solid #ccc', borderRadius: '5px', padding: '10px', marginBottom: '10px', width: '10%', height: '4%', fontSize: '24px' }} type="text" placeholder="Имя пользователя" value={username} onChange={(e) => setUsername(e.target.value)} />
            <input style={{ border: '1px solid #ccc', borderRadius: '5px', padding: '10px', marginBottom: '10px', width: '10%', height: '4%', fontSize: '24px' }} type="password" placeholder="Пароль" value={password} onChange={(e) => setPassword(e.target.value)} />
            <Button style={{ border: '1px solid #ccc', borderRadius: '5px', padding: '10px', marginTop: '1%', background: '#1976d2', color:'#ffffff', width: '15%', height: '7%', fontSize: '24px' }} onClick={handleLogin}>ВОЙТИ</Button>
            <h4 style={{ color:'#aaa' }}>В целях демонстрации работы авторизации, введите имя пользователя "root" и пароль "root".<br/>Также попробуйте любые другие данные.</h4>
            {error && <p style={{ color: 'red', fontSize: '24px' }}>{error}</p>}
        </div>
    );
};

export default Auth;