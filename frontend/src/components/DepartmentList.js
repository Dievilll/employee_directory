import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { TextField, Button, List, ListItem, ListItemText } from '@mui/material';
import { ExpandMore, ExpandLess } from '@mui/icons-material';
import DepartmentForm from './DepartmentForm';
import DepartmentTree from './DepartmentTree';

const DepartmentList = () => {
    const [departments, setDepartments] = useState([]);
    const [searchTerm, setSearchTerm] = useState('');
    const [showForm, setShowForm] = useState(false);
    const [selectedDepartment, setSelectedDepartment] = useState(null);
    const [departmentNames, setDepartmentNames] = useState({});
    const [positionNames, setPositionNames] = useState({});
    const [expandedDepartments, setExpandedDepartments] = useState({});

    useEffect(() => {
        fetchDepartments();
    }, []);

    const buildDepartmentTree = (departments) => {
        const departmentMap = {};
        const rootDepartments = [];
    
        departments.forEach(department => {
            department.subDepartments = [];
            departmentMap[department.departmentId] = department;
        });
    
        // Строим дерево
        departments.forEach(department => {
            if (department.parentDepartmentId) {
                const parentDepartment = departmentMap[department.parentDepartmentId];
                if (parentDepartment) {
                    parentDepartment.subDepartments.push(department);
                }
            } else {
                rootDepartments.push(department);
            }
        });
    
        return rootDepartments;
    };

    const fetchDepartments = async () => {
        try {
            const response = await axios.get('http://localhost:5085/api/department');
            const departmentTree = buildDepartmentTree(response.data);
            setDepartments(departmentTree);
            fetchDepartmentNames(response.data);
            fetchPositionNames(response.data);
        } catch (error) {
            console.error('Error fetching departments:', error);
        }
    };

    const fetchDepartmentNames = async (departments) => {
        const departmentIds = new Set();
        departments.forEach(department => {
            if (department.parentDepartment) departmentIds.add(department.parentDepartment.departmentId);
            department.subDepartments.forEach(subDepartment => departmentIds.add(subDepartment.departmentId));
        });

        const names = {};
        for (const id of departmentIds) {
            const response = await axios.get(`http://localhost:5085/api/department/${id}`);
            names[id] = response.data.name;
        }
        setDepartmentNames(names);
    };

    const fetchPositionNames = async (departments) => {
        const positionIds = new Set();
        departments.forEach(department => {
            department.positions.forEach(position => positionIds.add(position.positionId));
        });

        const names = {};
        for (const id of positionIds) {
            const response = await axios.get(`http://localhost:5085/api/position/${id}`);
            names[id] = response.data.title;
        }
        setPositionNames(names);
    };

    const handleSearch = (event) => {
        setSearchTerm(event.target.value);
    };

    const handleAddDepartment = () => {
        setSelectedDepartment(null);
        setShowForm(true);
    };

    const handleEditDepartment = (department) => {
        setSelectedDepartment(department);
        setShowForm(true);
    };

    const handleFormSubmit = () => {
        setShowForm(false);
        fetchDepartments();
    };

    const handleFormCancel = () => {
        setShowForm(false);
    };

    const handleDepartmentClick = (departmentId) => {
            setExpandedDepartments(prev => ({
                ...prev,
                [departmentId]: !prev[departmentId]
            }));
    };

    const handleDeleteDepartment = async (departmentId) => {
        try {
            await axios.delete(`http://localhost:5085/api/department/${departmentId}`);
            fetchDepartments();
        } catch (error) {
            console.error('Error deleting department:', error);
        }
    };

    const renderDepartments = (departments) => {
        return departments.map(department => (
            <div key={department.departmentId}>
                <ListItem button onClick={() => handleDepartmentClick(department.departmentId)}>
                    <ListItemText primary={department.name} secondary={department.departmentId} />
                    {department.subDepartments.length > 0 && (expandedDepartments[department.departmentId] ? <ExpandLess /> : <ExpandMore />)}
                </ListItem>
                {expandedDepartments[department.departmentId] && (
                    <List style={{ paddingLeft: '20px' }}>
                        {renderDepartments(department.subDepartments)}
                    </List>
                )}
            </div>
        ));
    };

    const filteredDepartments = departments.filter(department =>
        department.name.toLowerCase().includes(searchTerm.toLowerCase())
    );

    return (
        <div style={{ display: 'flex', height: '100vh' }}>
            <div style={{ width: '30%', borderRight: '1px solid #ccc', padding: '20px' }}>
                <TextField label="Search" onChange={handleSearch} fullWidth />
                <Button variant="contained" color="primary" onClick={handleAddDepartment} fullWidth>Добавить отдел</Button>
                {showForm ? (
                    <DepartmentForm department={selectedDepartment} onSubmit={handleFormSubmit} onCancel={handleFormCancel} />
                ) : (
                    <List>
                        {renderDepartments(filteredDepartments)}
                    </List>
                )}
            </div>
            <div style={{ width: '70%', padding: '20px' }}>
                <DepartmentTree
                    departments={filteredDepartments}
                    departmentNames={departmentNames}
                    positionNames={positionNames}
                    onEdit={handleEditDepartment}
                    onDelete={handleDeleteDepartment}
                />
            </div>
        </div>
    );
};

export default DepartmentList;