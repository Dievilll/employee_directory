using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Employee;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync(QueryObjectEmployee queryObjectEmployee);

        Task<Employee?> GetByIdAsync(int employeeId);

        Task<Employee> CreateAsync(Employee employeeModel);

        Task<Employee?> UpdateAsync(int id, EmployeeUpdateDto employeeUpdate);

        Task<Employee?> DeleteAsync(int employeeId);

        Task<bool> IsExistsAsync(int employeeId);

        Task<bool> AnyExistAsync();
    }
}