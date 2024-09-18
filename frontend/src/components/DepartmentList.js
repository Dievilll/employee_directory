import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { TextField, Button, List, ListItem, ListItemText, Card, CardContent, Typography } from '@mui/material';
import { ExpandMore, ExpandLess } from '@mui/icons-material';
import DepartmentForm from './DepartmentForm';

const DepartmentList = () => {
    const [departments, setDepartments] = useState([]);
    const [searchTerm, setSearchTerm] = useState('');
    const [showForm, setShowForm] = useState(false);
    const [selectedDepartment, setSelectedDepartment] = useState(null);
    const [departmentNames, setDepartmentNames] = useState({});
    const [positionNames, setPositionNames] = useState({});
    const [expandedDepartments, setExpandedDepartments] = useState({});
    const [filteredDepartments, setFilteredDepartments] = useState([]);
    const [isEditing, setIsEditing] = useState(false);

    useEffect(() => {
        fetchDepartments();
    }, []);

    useEffect(() => {
        if (searchTerm === '') {
            setFilteredDepartments(departments);
        } else {
            setFilteredDepartments(filterDepartments(departments, searchTerm));
        }
    }, [searchTerm, departments]);

    const buildDepartmentTree = (departments) => {
        const departmentMap = {};
        const rootDepartments = [];
    
        departments.forEach(department => {
            department.subDepartments = [];
            departmentMap[department.departmentId] = department;
        });
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
            setFilteredDepartments(departmentTree);
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
        setIsEditing(false);
    };

    const handleEditDepartment = (department) => {
        setSelectedDepartment(department);
        setShowForm(true);
        setIsEditing(true);
    };

    const handleFormSubmit = () => {
        setShowForm(false);
        fetchDepartments();
        setIsEditing(false);
    };

    const handleFormCancel = () => {
        setShowForm(false);
        setIsEditing(false);
    };

    const handleDepartmentClick = (departmentId) => {
        setExpandedDepartments(prev => ({
            ...prev,
            [departmentId]: !prev[departmentId]
        }));
    };

    const handleViewDepartment = (department) => {
        setSelectedDepartment(department);
    };

    const handleDeleteDepartment = async (departmentId) => {
        const department = departments.find(d => d.departmentId === departmentId);
        if (department && department.subDepartments && department.subDepartments.length > 0) {
            const confirmDelete = window.confirm("Удаление этого отдела приведет к удалению всех дочерних отделов и связанных сотрудников. Вы уверены?");
            if (!confirmDelete) {
                return;
            }
        }

        try {
            await axios.delete(`http://localhost:5085/api/department/${departmentId}`);
            fetchDepartments();
            setSelectedDepartment(null);
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
                    <Button variant="outlined" color="primary" onClick={(e) => { e.stopPropagation(); handleViewDepartment(department); }}>
                        Просмотреть
                    </Button>
                </ListItem>
                {expandedDepartments[department.departmentId] && (
                    <List style={{ paddingLeft: '20px', marginLeft: '1%' }}>
                        {renderDepartments(department.subDepartments)}
                    </List>
                )}
            </div>
        ));
    };

    const filterDepartments = (departments, searchTerm) => {
        return departments.filter(department => {
            const matchesSearch = department.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
                department.positions.some(position => positionNames[position.positionId].toLowerCase().includes(searchTerm.toLowerCase()));

            if (matchesSearch) {
                return true;
            }

            if (department.subDepartments.length > 0) {
                department.subDepartments = filterDepartments(department.subDepartments, searchTerm);
                return true;
            }

            return false;
        });
    };

    return (
        <div style={{ display: 'flex', height: '100vh' }}>
            <div style={{ width: '30%', borderRight: '1px solid #ccc', padding: '20px' }}>
                <TextField label="Search" onChange={handleSearch} fullWidth />
                <Button 
                    variant="contained" 
                    color="primary" 
                    onClick={handleAddDepartment} 
                    fullWidth 
                    disabled={isEditing}
                    style={{ marginTop: '1%', marginBottom: '2%' }}
                >
                    Добавить отдел
                </Button>
                {showForm ? (
                    <DepartmentForm department={selectedDepartment} onSubmit={handleFormSubmit} onCancel={handleFormCancel} />
                ) : (
                    <List>
                        {renderDepartments(filteredDepartments)}
                    </List>
                )}
            </div>
            <div style={{ width: '70%', padding: '20px' }}>
                {selectedDepartment ? (
                    <Card>
                        <CardContent>
                            <Typography variant="h5" component="h2">
                                {selectedDepartment.name}
                            </Typography>
                            <Typography color="textSecondary">
                                ID: {selectedDepartment.departmentId}
                            </Typography>
                            <Typography variant="body1" component="p" style={{ marginBottom: '1%' }}>
                                Должности:
                                {selectedDepartment.positions.length > 0 ? (
                                    <List>
                                        {selectedDepartment.positions.map(position => (
                                            <ListItem style={{borderBottom: '1px solid #ccc'}} key={position.positionId}>
                                                <ListItemText primary={positionNames[position.positionId]} secondary={`Зарплата: ${position.salary} рублей`} />
                                                <Typography variant="body1" component="p">
                                                    Работники:
                                                    <ul>
                                                        {position.employees && position.employees.length > 0 ? (
                                                            position.employees.map(employee => (
                                                                <li key={employee.employeeId}>
                                                                    {employee.fullName}
                                                                </li>
                                                            ))
                                                        ) : (
                                                            <li>отсутствуют</li>
                                                        )}
                                                    </ul>
                                                </Typography>
                                            </ListItem>
                                        ))}
                                    </List>
                                ) : (
                                    <Typography variant="body2" component="p">
                                        отсутствуют
                                    </Typography>
                                )}
                            </Typography>
                            <Button variant="contained" color="primary" onClick={() => handleEditDepartment(selectedDepartment)}>
                                Редактировать
                            </Button>
                            <br />
                            <Button style={{ marginTop: '1%' }} variant="contained" color="error" onClick={() => handleDeleteDepartment(selectedDepartment.departmentId)}>
                                Удалить
                            </Button>
                            {selectedDepartment.subDepartments && selectedDepartment.subDepartments.length > 0 && (
                                <Typography variant="body2" color="error" style={{ marginTop: '1%' }}>
                                    Удаление этого отдела приведет к удалению всех дочерних отделов и связанных сотрудников.
                                </Typography>
                            )}
                        </CardContent>
                    </Card>
                ) : (
                    <Typography variant="body1">
                        Выберите отдел для просмотра информации.
                    </Typography>
                )}
            </div>
        </div>
    );
};

export default DepartmentList;