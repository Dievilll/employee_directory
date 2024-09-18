import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { TextField, Button, List, ListItem, ListItemText, Card, CardContent, CardMedia, Typography } from '@mui/material';
import { ExpandMore, ExpandLess } from '@mui/icons-material';
import EmployeeForm from './EmployeeForm';

const EmployeeList = () => {
    const [employees, setEmployees] = useState([]);
    const [searchTerm, setSearchTerm] = useState('');
    const [showForm, setShowForm] = useState(false);
    const [selectedEmployee, setSelectedEmployee] = useState(null);
    const [expandedDepartments, setExpandedDepartments] = useState({});

    useEffect(() => {
        fetchEmployees();
    }, []);

    const fetchEmployees = async () => {
        try {
            const response = await axios.get('http://localhost:5085/api/employee');
            setEmployees(response.data);
        } catch (error) {
            console.error('Error fetching employees:', error);
        }
    };

    const handleSearch = (event) => {
        setSearchTerm(event.target.value);
    };

    const handleAddEmployee = () => {
        setSelectedEmployee(null);
        setShowForm(true);
    };

    const handleEditEmployee = (employee) => {
        setSelectedEmployee(employee);
        setShowForm(true);
    };

    const handleFormSubmit = () => {
        setShowForm(false);
        fetchEmployees();
    };

    const handleFormCancel = () => {
        setShowForm(false);
    };

    const handleDepartmentClick = (departmentName) => {
        setExpandedDepartments(prev => ({
            ...prev,
            [departmentName]: !prev[departmentName]
        }));
    };

    const handleEmployeeClick = (employee) => {
        setSelectedEmployee(employee);
    };

    const handleDeleteEmployee = async (employeeId) => {
        try {
            await axios.delete(`http://localhost:5085/api/employee/${employeeId}`);
            fetchEmployees();
            setSelectedEmployee(null);
        } catch (error) {
            console.error('Error deleting employee:', error);
        }
    };

    const filteredEmployees = employees.filter(employee =>
        employee.fullName.toLowerCase().includes(searchTerm.toLowerCase()) ||
        employee.phoneNumber.includes(searchTerm) ||
        employee.departmentName.toLowerCase().includes(searchTerm.toLowerCase())
    );

    const groupedEmployees = filteredEmployees.reduce((acc, employee) => {
        if (!acc[employee.departmentName]) {
            acc[employee.departmentName] = [];
        }
        acc[employee.departmentName].push(employee);
        return acc;
    }, {});

    return (
        <div style={{ display: 'flex', height: 'auto' }}>
            <div style={{ width: '30%', borderRight: '1px solid #ccc', padding: '20px' }}>
                <TextField label="Search" onChange={handleSearch} fullWidth />
                <Button variant="contained" color="primary" onClick={handleAddEmployee} fullWidth>Add Employee</Button>
                {showForm && <EmployeeForm employee={selectedEmployee} onSubmit={handleFormSubmit} onCancel={handleFormCancel} />}
                <List>
                    {Object.keys(groupedEmployees).map(departmentName => (
                        <div key={departmentName}>
                            <ListItem button onClick={() => handleDepartmentClick(departmentName)}>
                                <ListItemText primary={departmentName} />
                                {expandedDepartments[departmentName] ? <ExpandLess /> : <ExpandMore />}
                            </ListItem>
                            {expandedDepartments[departmentName] && groupedEmployees[departmentName].map(employee => (
                                <ListItem button key={employee.employeeId} onClick={() => handleEmployeeClick(employee)}>
                                    <ListItemText primary={employee.fullName} secondary={employee.positionTitle} />
                                </ListItem>
                            ))}
                        </div>
                    ))}
                </List>
            </div>
            <div style={{ width: '70%', padding: '20px', display: 'flex', justifyContent: 'center' }}>
                {selectedEmployee && (
                    <Card style={{ width: '60%', maxWidth: '80%' }}>
                        <CardMedia
                            component="img"
                            style={{ height: 'auto', maxWidth: '100%' }}
                            image={`data:image/jpeg;base64,${selectedEmployee.photo}`}
                            alt={selectedEmployee.fullName}
                        />
                        <CardContent>
                            <Typography style={{ borderBottom: '1px solid #ccc', marginBottom: '1%' }} variant="h4">{selectedEmployee.fullName}</Typography>
                            <Typography style={{ borderBottom: '1px solid #ccc', marginBottom: '1%' }} variant="h6">Отдел: {selectedEmployee.departmentName}</Typography>
                            <Typography style={{ borderBottom: '1px solid #ccc', marginBottom: '1%' }} variant="h6">Должность: {selectedEmployee.positionTitle}</Typography>
                            <Typography style={{ borderBottom: '1px solid #ccc', marginBottom: '1%' }} variant="h6">Номер телефона: {selectedEmployee.phoneNumber}</Typography>
                            <Button style={{ marginTop: '1%' }} variant="contained" color="primary" onClick={() => handleEditEmployee(selectedEmployee)}>
                                Редактировать
                            </Button>
                            <br/>
                            <Button style={{ marginTop: '1%' }} variant="contained" color="error" onClick={() => handleDeleteEmployee(selectedEmployee.employeeId)}>
                                Удалить
                            </Button>
                        </CardContent>
                    </Card>
                )}
            </div>
        </div>
    );
};

export default EmployeeList;