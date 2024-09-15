import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Card, CardContent, CardMedia, Typography, TextField, Button } from '@mui/material';
import { FixedSizeList as List } from 'react-window';
import EmployeeForm from './EmployeeForm';

const EmployeeList = () => {
    const [employees, setEmployees] = useState([]);
    const [searchTerm, setSearchTerm] = useState('');
    const [showForm, setShowForm] = useState(false);
    const [selectedEmployee, setSelectedEmployee] = useState(null);

    useEffect(() => {
        fetchEmployees();
    }, []);

    const fetchEmployees = async () => {
        const response = await axios.get('/api/employee');
        setEmployees(response.data);
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

    const filteredEmployees = employees.filter(employee =>
        employee.fullName.toLowerCase().includes(searchTerm.toLowerCase()) ||
        employee.phoneNumber.includes(searchTerm) ||
        employee.departmentName.toLowerCase().includes(searchTerm.toLowerCase())
    );

    const Row = ({ index, style }) => (
        <div style={style} key={filteredEmployees[index].employeeId}>
            <Card style={{ width: '300px', margin: '10px' }}>
                <CardMedia
                    component="img"
                    height="140"
                    image={`data:image/jpeg;base64,${filteredEmployees[index].photo}`}
                    alt={filteredEmployees[index].fullName}
                />
                <CardContent>
                    <Typography variant="h5">{filteredEmployees[index].fullName}</Typography>
                    <Typography variant="body2">Department: {filteredEmployees[index].departmentName}</Typography>
                    <Typography variant="body2">Position: {filteredEmployees[index].positionTitle}</Typography>
                    <Typography variant="body2">Phone: {filteredEmployees[index].phoneNumber}</Typography>
                    <Button variant="contained" color="primary" onClick={() => handleEditEmployee(filteredEmployees[index])}>
                        Edit
                    </Button>
                </CardContent>
            </Card>
        </div>
    );

    return (
        <div>
            <TextField label="Search" onChange={handleSearch} />
            <Button variant="contained" color="primary" onClick={handleAddEmployee}>Add Employee</Button>
            {showForm && <EmployeeForm employee={selectedEmployee} onSubmit={handleFormSubmit} />}
            <div style={{ display: 'flex', flexWrap: 'wrap' }}>
                <List
                    height={400}
                    itemCount={filteredEmployees.length}
                    itemSize={150}
                    width={1200}
                >
                    {Row}
                </List>
            </div>
        </div>
    );
};

export default EmployeeList;