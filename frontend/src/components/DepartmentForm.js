import React, { useState } from 'react';
import axios from 'axios';
import { TextField, Button } from '@mui/material';

const DepartmentForm = ({ department, onSubmit }) => {
    const [formData, setFormData] = useState({
        name: department?.name || '',
        parentDepartmentId: department?.parentDepartmentId || ''
    });

    const handleChange = (event) => {
        setFormData({ ...formData, [event.target.name]: event.target.value });
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        if (department) {
            await axios.put(`/api/department/${department.departmentId}`, formData);
        } else {
            await axios.post('/api/department', formData);
        }
        onSubmit();
    };

    return (
        <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: '10px' }}>
            <TextField name="name" label="Department Name" value={formData.name} onChange={handleChange} />
            <TextField name="parentDepartmentId" label="Parent Department ID" value={formData.parentDepartmentId} onChange={handleChange} />
            <Button type="submit" variant="contained" color="primary">
                {department ? 'Update' : 'Add'} Department
            </Button>
        </form>
    );
};

export default DepartmentForm;