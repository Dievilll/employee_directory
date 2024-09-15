using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Department;
using api.DTOs.Position;
using api.Models;

namespace api.Mappers
{
    public static class DepartmentMappers
    {
        public static DepartmentDto ToDepartmentDto(this Department department)
        {
            return new DepartmentDto
            {
                DepartmentId = department.DepartmentId,
                Name = department.Name,
                ParentDepartmentId = department.ParentDepartmentId,
                ParentDepartmentName = department.ParentDepartment?.Name,
                SubDepartments = department.SubDepartments?.Select(d => d.ToDepartmentDto()).ToList() ?? new List<DepartmentDto>(),
                Positions = department.Positions?.Select(p => p.ToPositionDto()).ToList() ?? new List<PositionDto>()
            };
        }

        public static Department ToDepartmentCreateDto(this DepartmentCreateDto departmentDto)
        {
            return new Department
            {
                Name = departmentDto.Name,
                ParentDepartmentId = departmentDto.ParentDepartmentId
                
            };

        }

        public static Department ToDepartmentUpdateDto(this DepartmentUpdateDto departmentDto, int id)
        {
            return new Department
            {
                DepartmentId = id,
                Name = departmentDto.Name,
                ParentDepartmentId = departmentDto.ParentDepartmentId
            };
        }
    }
}