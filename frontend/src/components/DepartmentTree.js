import React from 'react';
import { Card, CardContent, Typography, Button } from '@mui/material';

const DepartmentTree = ({ departments, departmentNames, positionNames, onEdit, onDelete }) => {
    const renderDepartment = (department, level, index) => (
        <div key={department.departmentId} style={{ position: 'relative', marginLeft: `${level * 20 * index}px` }}>
            <Card style={{ width: '300px', margin: '10px' }}>
                <CardContent>
                    <Typography variant="h5">ID отдела:{department.departmentId}</Typography>
                    <Typography variant="body2">Название: {department.name}</Typography>
                    <Typography variant="body2">Родительский отдел: {department.parentDepartment ? departmentNames[department.parentDepartment.departmentId] : 'N/A'}</Typography>
                    <Typography variant="body2">Дочерние отделы: {department.subDepartments ? department.subDepartments.map(subDepartment => departmentNames[subDepartment.departmentId]).join(', ') : 'N/A'}</Typography>
                    <Typography variant="body2">Должности: {department.positions ? department.positions.map(position => positionNames[position.positionId]).join(', ') : 'N/A'}</Typography>
                    <Button variant="contained" color="primary" onClick={() => onEdit(department)}>
                        Редактировать
                    </Button>
                    <Button variant="contained" color="secondary" onClick={() => onDelete(department.departmentId)}>
                        Удалить
                    </Button>
                </CardContent>
            </Card>
            <div style={{ display: 'flex', justifyContent: 'center', marginTop: '20px' }}>
                {department.subDepartments && department.subDepartments.map((subDepartment, subIndex) => (
                    <div
                        key={subDepartment.departmentId}
                        style={{
                            position: 'absolute',
                            top: '100%',
                            left: `${subIndex * 320}px`,
                            transform: 'translateX(-100%)',
                        }}
                    >
                        {renderDepartment(subDepartment, level + 1, subIndex + 1)}
                    </div>
                ))}
            </div>
        </div>
    );

    return (
        <div style={{ display: 'flex', justifyContent: 'center' }}>
            {departments.map((department, index) => renderDepartment(department, 0, index))}
        </div>
    );
};

export default DepartmentTree;