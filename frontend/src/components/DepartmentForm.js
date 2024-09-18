import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { TextField, Button, List, ListItem, ListItemText, Input, Card, CardContent, Typography, ListItemSecondaryAction, Checkbox, Select, MenuItem, FormControl, InputLabel } from '@mui/material';

const DepartmentForm = ({ department, onSubmit, onCancel }) => {
    const [formData, setFormData] = useState({
        name: department?.name || '',
        parentDepartmentId: department?.parentDepartmentId || '',
        positionsToUpdate: department?.positions || [],
        positionsToCreate: []
    });

    const [newPositionTitle, setNewPositionTitle] = useState('');
    const [newPositionSalary, setNewPositionSalary] = useState('');
    const [positionsToDelete, setPositionsToDelete] = useState([]);
    const [editingPosition, setEditingPosition] = useState(null);
    const [departments, setDepartments] = useState([]);

    useEffect(() => {
        fetchDepartments();
    }, []);

    const fetchDepartments = async () => {
        try {
            const response = await axios.get('http://localhost:5085/api/department');
            setDepartments(response.data);
        } catch (error) {
            console.error('Error fetching departments:', error);
        }
    };

    const handleChange = (event) => {
        setFormData({ ...formData, [event.target.name]: event.target.value });
    };

    const handlePositionChange = (index, field, value) => {
        const updatedPositions = [...formData.positionsToUpdate];
        updatedPositions[index][field] = value;
        setFormData({ ...formData, positionsToUpdate: updatedPositions });
    };

    const handleAddPosition = () => {
        const newPosition = {
            title: newPositionTitle,
            salary: newPositionSalary
        };
        setFormData({ ...formData, positionsToUpdate: [...formData.positionsToUpdate, newPosition] });
        setNewPositionTitle('');
        setNewPositionSalary('');
    };

    const handleDeletePosition = (positionId) => {
        const position = formData.positionsToUpdate.find(p => p.positionId === positionId);
        if (position && position.employees && position.employees.length > 0) {
            const confirmDelete = window.confirm("Удаление этой должности приведет к удалению всех связанных сотрудников. Вы уверены?");
            if (!confirmDelete) {
                return;
            }
        }

        if (positionsToDelete.includes(positionId)) {
            setPositionsToDelete(positionsToDelete.filter(id => id !== positionId));
        } else {
            setPositionsToDelete([...positionsToDelete, positionId]);
        }
    };

    const handleEditPosition = (positionId) => {
        const position = formData.positionsToUpdate.find(p => p.positionId === positionId);
        setEditingPosition(position);
    };

    const handleSavePosition = () => {
        const updatedPositions = formData.positionsToUpdate.map(p => {
            if (p.positionId === editingPosition.positionId) {
                return editingPosition;
            }
            return p;
        });
        setFormData({ ...formData, positionsToUpdate: updatedPositions });
        setEditingPosition(null);
    };

    const handleSubmit = async (event) => {
        event.preventDefault();

        const positionsToCreate = formData.positionsToUpdate.filter(p => !p.positionId);
        const positionsToUpdate = formData.positionsToUpdate.filter(p => p.positionId);

        const data = {
            name: formData.name,
            parentDepartmentId: formData.parentDepartmentId,
            positionsToUpdate: positionsToUpdate,
            positionsToCreate: positionsToCreate,
            positionsToDelete: positionsToDelete
        };

        try {
            if (department) {
                await axios.put(`http://localhost:5085/api/department/${department.departmentId}`, data);
            } else {
                await axios.post('http://localhost:5085/api/department', data);
            }
            window.location.reload();
            onSubmit();
        } catch (error) {
            console.error('Error saving department:', error);
        }
    };

    const getDescendants = (departmentId, departments) => {
        const descendants = [];
        const stack = [departmentId];

        while (stack.length > 0) {
            const currentId = stack.pop();
            const children = departments.filter(dep => dep.parentDepartmentId === currentId);
            descendants.push(...children.map(child => child.departmentId));
            stack.push(...children.map(child => child.departmentId));
        }

        return descendants;
    };

    const filteredDepartments = departments.filter(dep => {
        const descendants = getDescendants(department?.departmentId, departments);
        return dep.departmentId !== department?.departmentId && !descendants.includes(dep.departmentId);
    });

    return (
        <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: '10px', marginTop: '2%' }}>
            <TextField name="name" label="Название отдела" value={formData.name} onChange={handleChange} />
            <FormControl>
                <InputLabel>Родительский отдел</InputLabel>
                <Select
                    name="parentDepartmentId"
                    value={formData.parentDepartmentId}
                    onChange={handleChange}
                >
                    {filteredDepartments.map(dep => (
                        <MenuItem key={dep.departmentId} value={dep.departmentId}>
                            {dep.name}
                        </MenuItem>
                    ))}
                </Select>
            </FormControl>

            <Typography variant="h6" component="h2">
                Должности
            </Typography>

            {formData.positionsToUpdate.map((position, index) => (
                <ListItem key={index}>
                    {editingPosition && editingPosition.positionId === position.positionId ? (
                        <>
                            <TextField
                                label="Название должности"
                                value={editingPosition.title}
                                onChange={(e) => setEditingPosition({ ...editingPosition, title: e.target.value })}
                            />
                            <TextField
                                label="Зарплата"
                                value={editingPosition.salary}
                                onChange={(e) => setEditingPosition({ ...editingPosition, salary: e.target.value.replace(/\D/g, '') })}
                            />
                            <Button variant="contained" color="primary" onClick={handleSavePosition}>
                                Сохранить
                            </Button>
                        </>
                    ) : (
                        <>
                            <ListItemText primary={position.title} secondary={`Зарплата: ${position.salary} рублей`} />
                            <Button style={{ marginRight: '1%' }} variant="outlined" color="primary" onClick={() => handleEditPosition(position.positionId)}>
                                Редактировать
                            </Button>
                            <a>Отметьте, чтобы удалить</a>
                            <Checkbox
                                edge="end"
                                onChange={() => handleDeletePosition(position.positionId)}
                                checked={positionsToDelete.includes(position.positionId)}
                            />
                        </>
                    )}
                </ListItem>
            ))}

            <ListItem>
                <TextField
                    label="Название должности"
                    value={newPositionTitle}
                    onChange={(e) => setNewPositionTitle(e.target.value)}
                />
                <TextField
                    label="Зарплата"
                    value={newPositionSalary}
                    onChange={(e) => setNewPositionSalary(e.target.value.replace(/\D/g, ''))}
                    //inputProps={{ inputMode: 'numeric', pattern: '[0-9]*' }}
                />
                <Button style={{ marginLeft: '1%' }} variant="contained" color="primary" onClick={handleAddPosition}>
                    Добавить должность
                </Button>
            </ListItem>

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