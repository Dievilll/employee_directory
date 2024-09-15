import React, { useState } from 'react';
import { useAuth } from '../context/AuthContext';
import { TextField, Button } from '@mui/material';

const Register = () => {
    const [formData, setFormData] = useState({
        username: '',
        password: ''
    });

    const { register } = useAuth();

    const handleChange = (event) => {
        setFormData({ ...formData, [event.target.name]: event.target.value });
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        register(formData.username, formData.password);
    };

    return (
        <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: '10px' }}>
            <TextField name="username" label="Username" value={formData.username} onChange={handleChange} />
            <TextField name="password" label="Password" type="password" value={formData.password} onChange={handleChange} />
            <Button type="submit" variant="contained" color="primary">
                Register
            </Button>
        </form>
    );
};

export default Register;