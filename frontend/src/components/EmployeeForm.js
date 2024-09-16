import React, { useState } from 'react';
import axios from 'axios';
import { TextField, Button } from '@mui/material';

const EmployeeForm = ({ employee, onSubmit, onCancel }) => {
    const [formData, setFormData] = useState({
        fullName: employee?.fullName || '',
        phoneNumber: employee?.phoneNumber || '',
        departmentId: employee?.departmentId || '',
        positionId: employee?.positionId || '',
        photo: employee?.photo || null
    });

    const handleChange = (event) => {
        setFormData({ ...formData, [event.target.name]: event.target.value });
    };

    const handleFileChange = (event) => {
        if (event.target.files.length > 0) {
            setFormData({ ...formData, photo: event.target.files[0] });
        }
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        const form = new FormData();
        form.append('fullName', formData.fullName);
        form.append('phoneNumber', formData.phoneNumber);
        form.append('departmentId', formData.departmentId);
        form.append('positionId', formData.positionId);
        if(formData.photo) {
            form.append('photo', formData.photo);
        }
        
        if (employee) {
            await axios.put(`http://localhost:5085/api/employee/${employee.employeeId}`, form);
        } else {
            await axios.post('http://localhost:5085/api/employee', form);
        }
        onSubmit();
    };

    return (
        <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: '10px', marginTop: '2%' }}>
            <TextField name="fullName" label="Full Name" value={formData.fullName} onChange={handleChange} />
            <TextField name="phoneNumber" label="Phone Number" value={formData.phoneNumber} onChange={handleChange} />
            <TextField name="departmentId" label="Department ID" value={formData.departmentId} onChange={handleChange} />
            <TextField name="positionId" label="Position ID" value={formData.positionId} onChange={handleChange} />
            <input type="file" onChange={handleFileChange} />
            <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                <Button type="submit" variant="outlined" size='large' color="primary">
                    {employee ? 'Обновить' : 'Добавить'} сотрудника
                </Button>
                <Button variant="outlined" size='medium' color="error" onClick={onCancel}>
                    Отмена
                </Button>
            </div>
        </form>
    );
};

export default EmployeeForm;