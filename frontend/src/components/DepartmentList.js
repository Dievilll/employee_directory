import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { TreeView, TreeItem } from '@mui/lab';
import { TextField, Button } from '@mui/material';
import DepartmentForm from './DepartmentForm';

const DepartmentList = () => {
    const [departments, setDepartments] = useState([]);
    const [searchTerm, setSearchTerm] = useState('');
    const [showForm, setShowForm] = useState(false);

    useEffect(() => {
        fetchDepartments();
    }, []);

    const fetchDepartments = async () => {
        try {
            const response = await axios.get('http://localhost:5085/api/department');
            setDepartments(response.data);
            console.log('Departments:', response.data); // Добавьте это для отладки
        } catch (error) {
            console.error('Error fetching departments:', error);
        }
    };

    const handleSearch = (event) => {
        setSearchTerm(event.target.value);
    };

    const handleAddDepartment = () => {
        setShowForm(true);
    };

    const handleFormSubmit = () => {
        setShowForm(false);
        fetchDepartments();
    };

    const filteredDepartments = departments.filter(department =>
        department.name.toLowerCase().includes(searchTerm.toLowerCase())
    );

    return (
        <div>
            <TextField label="Search" onChange={handleSearch} />
            <Button variant="contained" color="primary" onClick={handleAddDepartment}>Add Department</Button>
            {showForm && <DepartmentForm onSubmit={handleFormSubmit} />}
            <TreeView>
                {filteredDepartments.map(department => (
                    <TreeItem key={department.departmentId} nodeId={department.departmentId.toString()} label={department.name}>
                        {department.positions.map(position => (
                            <TreeItem key={position.positionId} nodeId={position.positionId.toString()} label={`${position.title} - ${position.salary}`} />
                        ))}
                    </TreeItem>
                ))}
            </TreeView>
        </div>
    );
};

export default DepartmentList;