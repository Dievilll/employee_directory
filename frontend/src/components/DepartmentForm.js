import React, { useState } from 'react';
import axios from 'axios';
import { TextField, Button } from '@mui/material';

const DepartmentForm = ({ department, onSubmit, onCancel }) => {
    const [formData, setFormData] = useState({
        name: department?.name || '',
        parentDepartmentId: department?.parentDepartmentId || ''
    });

    const handleChange = (event) => {
        setFormData({ ...formData, [event.target.name]: event.target.value });
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        const form = new FormData();
        form.append('name', formData.name);
        form.append('parentDepartmentId', formData.parentDepartmentId);

        if (department) {
            await axios.put(`http://localhost:5085/api/department/${department.departmentId}`, form);
        } else {
            await axios.post('http://localhost:5085/api/department', form);
        }
        onSubmit();
    };

    return (
        <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: '10px', marginTop: '2%' }}>
            <TextField name="name" label="Department Name" value={formData.name} onChange={handleChange} />
            <TextField name="parentDepartmentId" label="Parent Department ID" value={formData.parentDepartmentId} onChange={handleChange} />
            <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                <Button type="submit" variant="contained" color="primary">
                    {department ? 'Обновить' : 'Добавить'} отдел
                </Button>
                <Button variant="contained" color="secondary" onClick={onCancel}>
                    Отмена
                </Button>
            </div>
        </form>
    );
};

export default DepartmentForm;