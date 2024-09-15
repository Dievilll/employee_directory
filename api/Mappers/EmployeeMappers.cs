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

        public static Employee ToEmployeeCreateDto(this EmployeeCreateDto employeeDto)
        {
            byte[] photoBytes = null;

            if (employeeDto.Photo != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    employeeDto.Photo.CopyTo(memoryStream);
                    photoBytes = memoryStream.ToArray();
                }
            }
            return new Employee
            {
                FullName = employeeDto.FullName,
                DepartmentId = employeeDto.DepartmentId,
                PositionId = employeeDto.PositionId,
                PhoneNumber = employeeDto.PhoneNumber,
                Photo = photoBytes
            };
        }

        public static Employee ToEmployeeUpdateDto(this EmployeeUpdateDto employeeDto, int id)
        {
            byte[] photoBytes = null;

            if (employeeDto.Photo != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    employeeDto.Photo.CopyTo(memoryStream);
                    photoBytes = memoryStream.ToArray();
                }
            }
            return new Employee
            {
                EmployeeId = id,
                FullName = employeeDto.FullName,
                DepartmentId = employeeDto.DepartmentId,
                PositionId = employeeDto.PositionId,
                PhoneNumber = employeeDto.PhoneNumber,
                Photo = photoBytes
            };
        }
    }
}