using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Employee;
using api.Models;

namespace api.Mappers
{
    public static class EmployeeMappers
    {
        public static EmployeeDto ToEmployeeDto(this Employee employee)
        {
            return new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                FullName = employee.FullName,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department?.Name,
                PositionId = employee.PositionId,
                PositionTitle = employee.Position?.Title,
                PhoneNumber = employee.PhoneNumber,
                Photo = employee.Photo
            };
        }
    }
}