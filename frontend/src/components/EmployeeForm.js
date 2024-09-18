import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { TextField, Button, FormControl, InputLabel, Select, MenuItem } from '@mui/material';

const EmployeeForm = ({ employee, onSubmit, onCancel }) => {
    const [formData, setFormData] = useState({
        fullName: employee?.fullName || '',
        phoneNumber: employee?.phoneNumber || '',
        departmentId: employee?.departmentId || '',
        positionId: employee?.positionId || '',
        photo: employee?.photo || null
    });

    const [departments, setDepartments] = useState([]);
    const [positions, setPositions] = useState([]);

    useEffect(() => {
        axios.get('http://localhost:5085/api/department')
            .then(response => {
                setDepartments(response.data);
            })
            .catch(error => {
                console.error('Error loading departments:', error);
            });
    }, []);

    useEffect(() => {
        if (formData.departmentId) {
            axios.get(`http://localhost:5085/api/department/${formData.departmentId}/positions`)
                .then(response => {
                    setPositions(response.data);
                    if (!response.data.some(p => p.positionId === formData.positionId)) {
                        setFormData(prev => ({ ...prev, positionId: '' }));
                    }
                })
                .catch(error => {
                    console.error('Error loading positions:', error);
                });
        } else {
            setPositions([]);
            setFormData(prev => ({ ...prev, positionId: '' }));
        }
    }, [formData.departmentId]);

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
        if (formData.photo) {
            form.append('photo', formData.photo);
        }

        try {
            if (employee) {
                await axios.put(`http://localhost:5085/api/employee/${employee.employeeId}`, form);
            } else {
                await axios.post('http://localhost:5085/api/employee', form);
            }
            onSubmit();
        } catch (error) {
            console.error('Error saving employee:', error);
        }
    };

    return (
        <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: '10px', marginTop: '2%', marginBottom: '2%' }}>
            <TextField name="fullName" label="ФИО" value={formData.fullName} onChange={handleChange} />
            <TextField name="phoneNumber" label="Номер телефона" value={formData.phoneNumber} onChange={handleChange} />

            <FormControl>
                <InputLabel>Отдел</InputLabel>
                <Select
                    name="departmentId"
                    value={formData.departmentId}
                    onChange={handleChange}
                >
                    {departments.map(department => (
                        <MenuItem key={department.departmentId} value={department.departmentId}>
                            {department.name}
                        </MenuItem>
                    ))}
                </Select>
            </FormControl>

            <FormControl>
                <InputLabel>Должность</InputLabel>
                <Select
                    name="positionId"
                    value={formData.positionId}
                    onChange={handleChange}
                >
                    {positions.map(position => (
                        <MenuItem key={position.positionId} value={position.positionId}>
                            {position.title}
                        </MenuItem>
                    ))}
                </Select>
            </FormControl>

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