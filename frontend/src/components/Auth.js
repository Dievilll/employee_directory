import React, { useState } from 'react';
import { useAuth } from '../context/AuthContext';
import { useNavigate } from 'react-router-dom';
import { TextField, Button } from '@mui/material';

const Auth = () => {
    const [formData, setFormData] = useState({
        username: '',
        password: ''
    });

    const { signIn } = useAuth();
    const navigate = useNavigate();

    const handleChange = (event) => {
        setFormData({ ...formData, [event.target.name]: event.target.value });
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        signIn(formData.username, formData.password);
        navigate('/employees'); // Перенаправление после успешной авторизации
    };

    return (
        <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: '10px' }}>
            <TextField name="username" label="Username" value={formData.username} onChange={handleChange} />
            <TextField name="password" label="Password" type="password" value={formData.password} onChange={handleChange} />
            <Button type="submit" variant="contained" color="primary">
                Sign In
            </Button>
        </form>
    );
};

export default Auth;