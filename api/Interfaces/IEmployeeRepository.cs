using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync(QueryObjectEmployee queryObjectEmployee);

        Task<Employee> GetByIdAsync(int employeeId);

        Task<Employee> CreateAsync(Employee employee);

        Task<Employee> UpdateAsync(Employee employee);

        Task<Employee> DeleteAsync(int employeeId);
    }
}