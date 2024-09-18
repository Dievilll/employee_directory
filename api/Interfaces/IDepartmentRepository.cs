using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Department;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllAsync(QueryObjectDepartment queryObjectDepartment);

        Task<Department?> GetByIdAsync(int departmentId);

        Task<Department> CreateAsync(DepartmentCreateDto departmentCreateDto);

        Task<Department?> UpdateAsync(int id, DepartmentUpdateDto departmentUpdateDto);

        Task<Department?> DeleteAsync(int departmentId);

        Task<bool> IsExistsAsync(int departmentId);

        Task<bool> AnyExistAsync();
    }
}