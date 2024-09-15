using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Employee;
using api.DTOs.Position;
using api.Models;

namespace api.Mappers
{
    public static class PositionMappers
    {
        public static PositionDto ToPositionDto(this Position position)
        {
            return new PositionDto
            {
                PositionId = position.PositionId, 
                Title = position.Title, 
                Salary = position.Salary,
                DepartmentId = position.DepartmentId,
                DepartmentName = position.Department?.Name,
                Employees = position.Employees?.Select(e => e.ToEmployeeDto()).ToList() ?? new List<EmployeeDto>()
            };
        }

        public static Position ToPositionCreateDto(this PositionCreateDto positionDto)
        {
            return new Position
            {
                Title = positionDto.Title,
                Salary = positionDto.Salary,
                DepartmentId = positionDto.DepartmentId
            };
        }

        public static Position ToPositionUpdateDto(this PositionUpdateDto positionDto, int id)
        {
            return new Position
            {
                PositionId = id,
                Title = positionDto.Title,
                Salary = positionDto.Salary,
                DepartmentId = positionDto.DepartmentId
            };
        }
    }
}