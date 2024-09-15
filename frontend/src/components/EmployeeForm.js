import React, { useState } from 'react';
import axios from 'axios';
import { TextField, Button } from '@mui/material';

const EmployeeForm = ({ employee, onSubmit }) => {
    const [formData, setFormData] = useState({
        fullName: employee?.fullName || '',
        phoneNumber: employee?.phoneNumber || '',
        departmentId: employee?.departmentId || '',
        positionId: employee?.positionId || '',
        photo: null
    });

    const handleChange = (event) => {
        setFormData({ ...formData, [event.target.name]: event.target.value });
    };

    const handleFileChange = (event) => {
        setFormData({ ...formData, photo: event.target.files[0] });
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        const form = new FormData();
        form.append('fullName', formData.fullName);
        form.append('phoneNumber', formData.phoneNumber);
        form.append('departmentId', formData.departmentId);
        form.append('positionId', formData.positionId);
        form.append('photo', formData.photo);

        if (employee) {
            await axios.put(`/api/employee/${employee.employeeId}`, form);
        } else {
            await axios.post('/api/employee', form);
        }
        onSubmit();
    };

    return (
        <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: '10px' }}>
            <TextField name="fullName" label="Full Name" value={formData.fullName} onChange={handleChange} />
            <TextField name="phoneNumber" label="Phone Number" value={formData.phoneNumber} onChange={handleChange} />
            <TextField name="departmentId" label="Department ID" value={formData.departmentId} onChange={handleChange} />
            <TextField name="positionId" label="Position ID" value={formData.positionId} onChange={handleChange} />
            <input type="file" onChange={handleFileChange} />
            <Button type="submit" variant="contained" color="primary">
                {employee ? 'Update' : 'Add'} Employee
            </Button>
        </form>
    );
};

export default EmployeeForm;